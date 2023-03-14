using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Modules
{
    public static class HttpTools
    {
        private static async Task<T> DeserializeAsync<T>(HttpContent response)
        {
            var content = await response.ReadAsStringAsync();

            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }       
        public static async Task Exec(this HttpClient client, HttpRequestMessage req)
        {
            var response = await client.SendAsync(req);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }
        public static async Task<T> Exec<T>(this HttpClient client, HttpRequestMessage req)
        {

            try
            {
                var response = await client.SendAsync(req);

                if (response.IsSuccessStatusCode)
                {
                    var result = await DeserializeAsync<T>(response.Content);

                    return result;
                }


                var jresult = await response.Content.ReadAsStringAsync();

                var jObject = JObject.Parse(jresult);
                if (jObject.SelectToken("$.__wrapped") != null || jObject.SelectToken("$.__abp") != null)                
                    jresult = jObject.SelectToken("$.error").SelectToken("$.message").ToString();        
                
                throw new Exception(jresult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        public static async Task<TagOutputDto> Exec<TagOutputDto, TLoggerService>(this HttpClient client, HttpRequestMessage req, ILogger<TLoggerService> logger)
        {
            logger.LogDebug($"Calling backend service. Url: {req.RequestUri}");
            var response = await client.SendAsync(req);

            logger.LogDebug($"Response Code: {response.StatusCode}");
            if (response.IsSuccessStatusCode)
            {
                try
                {

                    var result = await DeserializeAsync<TagOutputDto>(response.Content);

                    return result;
                }
                catch (Exception ex)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    logger.LogError($"Error on deserializing to targetOutput response. Exception: {ex.Message}, Content: {content}");
                }
            }


            var jresult = await response.Content.ReadAsStringAsync();

            logger.LogDebug($"API call response content: {jresult}");

            var jObject = JObject.Parse(jresult);
            if (jObject.SelectToken("$.__wrapped") != null || jObject.SelectToken("$.__abp") != null)
            {
                jresult = jObject.SelectToken("$.error").SelectToken("$.message").ToString();
            }

            throw new Exception(jresult);

        }
    }
}
