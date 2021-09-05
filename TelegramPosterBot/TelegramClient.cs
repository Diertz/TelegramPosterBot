using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramPosterBot.Helpers;

namespace TelegramPosterBot
{
    class TelegramClient
    {
        private readonly string _token;
        private readonly string _channelId;

        public TelegramClient(string token, string channelId)
        {
            _token = token;
            _channelId = channelId;
        }

        public async Task SendPhotoAsync(Stream fileStream, string fileName)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(_channelId), "chat_id" },
                { new StreamContent(fileStream), "photo", fileName }
            };

            bool isSuccess = await HttpRequestSubmitter.PostAsync($"https://api.telegram.org/bot{_token}/sendPhoto", content);

            if (isSuccess)
                Logger.LogInfo($"Photo '{fileName}' was successfully sent.");
        }

        public async Task SendVideoAsync(Stream fileStream, string fileName)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(_channelId), "chat_id" },
                { new StreamContent(fileStream), "video", fileName },
            };

            bool isSuccess = await HttpRequestSubmitter.PostAsync($"https://api.telegram.org/bot{_token}/sendVideo", content);

            if (isSuccess)
                Logger.LogInfo($"Video '{fileName}' was successfully sent.");
        }
    }
}
