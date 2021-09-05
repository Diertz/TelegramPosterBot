using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TelegramPosterBot.Helpers
{
    class HttpRequestSubmitter
    {
        private readonly static HttpClient httpClient = new HttpClient();

        public async static Task<bool> PostAsync(string url, HttpContent httpContent) => await ExecuteHttpRequestAsync(HttpMethod.Post, url, httpContent);

        private async static Task<bool> ExecuteHttpRequestAsync(HttpMethod httpMethod, string url, HttpContent httpContent)
        {
            bool isSuccess = false;

            try
            {
                using (var httpRequestMessage = new HttpRequestMessage(httpMethod, url) { Content = httpContent })
                {
                    var response = await httpClient.SendAsync(httpRequestMessage);

                    if (response.IsSuccessStatusCode)
                        isSuccess = true;
                    else
                        throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"An error occured while executing http request for '{url}'.\nDetails: {ex.Message}");
            }

            return isSuccess;
        }
    }
}
