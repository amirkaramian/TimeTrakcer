using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TimeTracker.Model;
using TimeTracker.Modules;
using TimeTracker.Properties;
using TimeTracker.Service.Interface;

namespace TimeTracker.Service
{
    public class ServiceAccess : IService
    {
        private HttpClient httpClient;
        public ServiceAccess()
        {
            httpClient = new HttpClient();
            var baseAddress = Resources.url;
            if (string.IsNullOrEmpty(baseAddress))
                throw new Exception("Invalid url");
            httpClient.BaseAddress = new Uri(baseAddress);
        }

        public async Task<bool> ChangePassAsync(ChangePasswordModel model)
        {
            this.ValidateForgotPass(model);
            var userInfo = await LoginAsync(model.Email, model.OldPassword);

            var client = new RestClient($"{ Resources.url}api/User/changepassword")
            {
                Timeout = -1
            };

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {userInfo.AccessToken}");
            var body = JsonConvert.SerializeObject(model);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("error in cconnection to server");
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<List<ApplicationList>> GetAppsAsync(UserInfo userInfo)
        {
            var client = new RestClient($"{ Resources.url}api/TimeTracking/getapplicationbyoperatingsystem?operatingSystem=microsoft windows")
            {
                Timeout = -1
            };

            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {userInfo.AccessToken}");
            var response = client.ExecuteAsync(request).GetAwaiter().GetResult();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var resp = JsonConvert.DeserializeObject<ApiResponse<ApplicationList>>(response.Content);
                return resp.dataList;
            }
            return new List<ApplicationList>();
        }

        public async Task<int> GetConfigTimeAsync()
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "time");
            return await httpClient.Exec<int>(request);
        }

        public async Task<List<ProjectModel>> GetProjectsAsync(UserInfo userInfo)
        {


            var client = new RestClient($"{ Resources.url}api/project/getdrpprojectListbyuserid")
            {
                Timeout = -1
            };

            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {userInfo.AccessToken}");
            var response = client.ExecuteAsync(request).GetAwaiter().GetResult();
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("error in cconnection to server");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var resp = JsonConvert.DeserializeObject<ApiResponse<ProjectModel>>(response.Content);
                return resp.dataList;
            }
            return new List<ProjectModel>();

        }

        public async Task<UserInfo> LoginAsync(string userName, string password)
        {
            this.ValidateLogin(userName, password);
            var client = new RestClient($"{ Resources.url}api/User/login")
            {
                Timeout = -1
            };

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer <Bearer Token>");
            var body = "{ \"emailId\": \"" + userName + "\",\"password\": \"" + password + "\" }";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("error in cconnection to server");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var resp = JsonConvert.DeserializeObject<ApiResponse<UserInfo>>(response.Content);
                return JsonConvert.DeserializeObject<UserInfo>(resp.data.ToString());
            }
            throw new Exception("invalid user name or password");
        }

        public async Task<bool> LogoutAsync(UserInfo userInfo)
        {
            var client = new RestClient($"{ Resources.url}api/User/logout")
            {
                Timeout = -1
            };

            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", userInfo.AccessToken);
            var response = await client.ExecuteAsync(request);
            return true;// response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> RegisterAsync(UserModel model)
        {
            this.ValidateRegister(model);
            var client = new RestClient($"{Resources.url}api/User/signup")
            {
                Timeout = -1
            };

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer <Bearer Token>");
            var body = JsonConvert.SerializeObject(model);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("error in cconnection to server");
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<int> SendProjectHistory(ProjectHistory model, UserInfo userInfo)
        {
            var client = new RestClient($"{Resources.url}api/TimeTracking/insertactivity")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {userInfo.AccessToken}");
            var body = JsonConvert.SerializeObject(model);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = await client.ExecuteAsync<ApiResponse<object>>(request);
            var res = JsonConvert.DeserializeObject<ApiResponse<ProjectModel>>(response.Content);
            if (res == null)
                throw new Exception("error in sending data,please contact with administrator");
            return res.id;
        }

        protected void ValidateLogin(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                throw new Exception("Invalid userName or password");
        }

        protected void ValidateRegister(UserModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Family) ||
                string.IsNullOrEmpty(model.MobileNumber) || string.IsNullOrEmpty(model.Email))
                throw new Exception("Invalid user info");
            if (!IsValid(model.Email))
                throw new Exception("Invalid email");
            if (!IsPhoneNumber(model.MobileNumber))
                throw new Exception("Invalid mobile number");
            if (!IsValidPaass(model.Password))
                throw new Exception("Invalid password password should has a number,upper char,minimum 8 chars");
            if (!model.Password.Equals(model.ConfirmPassword))
                throw new Exception("password not match");

        }
        protected void ValidateForgotPass(ChangePasswordModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
                throw new Exception("Invalid user info");
            if (!IsValid(model.Email))
                throw new Exception("Invalid email");
            if (string.IsNullOrEmpty(model.OldPassword))
                throw new Exception("Invalid password password");
            if (!IsValidPaass(model.newPassword))
                throw new Exception("Invalid password password should has a number,upper char,minimum 8 chars");

        }
        protected bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"(^[0-9]{10}$)|(^[0-9]{11}$)|(^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$)|(^[0-9]{3}-[0-9]{4}-[0-9]{4}$)").Success;
        }
        protected bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        protected bool IsValidPaass(string input)
        {

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            return hasNumber.IsMatch(input) && hasUpperChar.IsMatch(input) && hasMinimum8Chars.IsMatch(input);
        }

        public bool UploadPictures(UserInfo userInfo, List<string> files, int projectWorkerActivityId)
        {
            var client = new RestClient($"{Resources.url}api/TimeTracking/insertimagelist?projectWorkerActivityId={projectWorkerActivityId}")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {userInfo.AccessToken}");
            request.AddHeader("Content-Type", "multipart/form-data");
            foreach (var item in files)
                request.AddFile("files", item);
            IRestResponse response = client.Execute(request);
            return response.IsSuccessful;
        }


    }
}
