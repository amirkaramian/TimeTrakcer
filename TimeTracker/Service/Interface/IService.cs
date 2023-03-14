
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Model;

namespace TimeTracker.Service.Interface
{
    public interface IService
    {
        Task<UserInfo> LoginAsync(string userName, string password);
        Task<bool> RegisterAsync(UserModel model);
        Task<bool> ChangePassAsync(ChangePasswordModel model);
        Task<bool> LogoutAsync(UserInfo userInfo);
        Task<List<ProjectModel>> GetProjectsAsync(UserInfo userInfo);
        Task<List<ApplicationList>> GetAppsAsync(UserInfo userInfo);
        Task<int> GetConfigTimeAsync();
        Task<int> SendProjectHistory(ProjectHistory project, UserInfo userInfo);
        bool UploadPictures(UserInfo userInfo, List<string> files, int projectWorkerActivityId);
    }
}
