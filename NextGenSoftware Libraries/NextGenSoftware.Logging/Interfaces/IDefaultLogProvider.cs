using System;

namespace NextGenSoftware.Logging.Interfaces
{
    public interface IDefaultLogProvider
    {
        bool AddAdditionalSpaceAfterEachLogEntry { get; set; }
        string LogDirectory { get; set; }
        string LogFileName { get; set; }
        bool LogToConsole { get; set; }
        bool LogToFile { get; set; }
        int NumberOfRetriesToLogToFile { get; set; }
        int RetryLoggingToFileEverySeconds { get; set; }

        event DefaultLogProvider.Error OnError;

        void Log(string message, LogType type, bool showWorkingAnimation = false);
        void Log(string message, LogType type, ConsoleColor consoleColour, bool showWorkingAnimation = false);
        void Shutdown();
    }
}