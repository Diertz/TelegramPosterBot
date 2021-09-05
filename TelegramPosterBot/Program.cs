using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TelegramPosterBot.Helpers;

namespace TelegramPosterBot
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSpan sendingInterval = TimeSpan.FromHours(1);
            const string postedFilesFolder = @"";
            var contentManager = new ContentManager(new List<string> { @"" });
            var telegramClient = new TelegramClient("", "");

            Task.Run(async () =>
            {
                do
                {
                    try
                    {
                        string photoPath = contentManager.GetRandomPhoto();
                        if (!string.IsNullOrWhiteSpace(photoPath))
                        {
                            string photoFileName = Path.GetFileName(photoPath);
                            await telegramClient.SendPhotoAsync(File.OpenRead(photoPath), photoFileName);
                            File.Move(photoPath, Path.Combine(postedFilesFolder, photoFileName));
                            await Task.Delay(sendingInterval);
                        }

                        string videoPath = contentManager.GetRandomVideo();
                        if (!string.IsNullOrWhiteSpace(videoPath))
                        {
                            string videoFileName = Path.GetFileName(videoPath);
                            await telegramClient.SendVideoAsync(File.OpenRead(videoPath), videoFileName);
                            File.Move(videoPath, Path.Combine(postedFilesFolder, videoFileName));
                            await Task.Delay(sendingInterval);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Unexpected error. Details: {ex.Message}");
                    }
                } while (true);
            });

            Console.ReadLine();
        }
    }
}
