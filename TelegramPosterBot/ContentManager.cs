using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TelegramPosterBot.Helpers;

namespace TelegramPosterBot
{
    class ContentManager
    {
        private readonly List<string> trackedFolders;

        private readonly Random random = new Random();

        private readonly List<string> photoPathes = new List<string>();
        private readonly List<string> videoPathes = new List<string>();

        public ContentManager(List<string> foldersToTrack)
        {
            trackedFolders = foldersToTrack;
            InitData();
        }

        public void InitData()
        {
            foreach (string folder in trackedFolders)
            {
                try
                {
                    photoPathes.AddRange(Directory.GetFiles(folder, "*.jpg"));
                    photoPathes.AddRange(Directory.GetFiles(folder, "*.png"));
                    videoPathes.AddRange(Directory.GetFiles(folder, "*.mp4"));
                }
                catch (Exception ex)
                {
                    Logger.LogError($"An exception occurred while refreshing the data be the path '{folder}'.\nDetails: {ex.Message}");
                }
            }

            Logger.LogInfo("Data was successfully refreshed.");
        }

        public string GetRandomPhoto()
        {
            return GetRandomFile(photoPathes);
        }

        public string GetRandomVideo()
        {
            return GetRandomFile(videoPathes);
        }

        private string GetRandomFile(IList<string> files)
        {
            string file = string.Empty;
            int attempts = 0;

            if (files.Any())
            {
                do
                {
                    file = files[random.Next(0, files.Count)];
                    attempts++;
                } while (!File.Exists(file) && attempts < 3);
            }

            return file;
        }
    }
}
