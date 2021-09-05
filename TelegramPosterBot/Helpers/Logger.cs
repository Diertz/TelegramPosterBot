using System;

namespace TelegramPosterBot.Helpers
{
    static class Logger
    {
        public static void LogInfo(string message) => Log(message, ConsoleColor.Green);

        public static void LogWarn(string message) => Log(message, ConsoleColor.Yellow);

        public static void LogError(string message) => Log(message, ConsoleColor.Red);

        private static void Log(string message, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}
