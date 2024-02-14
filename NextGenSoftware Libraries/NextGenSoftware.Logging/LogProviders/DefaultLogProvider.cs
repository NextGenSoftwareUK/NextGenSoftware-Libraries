using System;
using System.IO;
using System.Threading;
using NextGenSoftware.CLI.Engine;
using NextGenSoftware.ErrorHandling;
using NextGenSoftware.Logging.Interfaces;
using NextGenSoftware.WebSocket;

namespace NextGenSoftware.Logging
{
    public class DefaultLogProvider : LogProviderBase, ILogProvider, IDefaultLogProvider
    {
        public DefaultLogProvider(bool logToConsole = true, bool logToFile = true, string pathToLogFile = "Logs", string logFileName = "Log.txt", int maxLogFileSize = 1000000, bool addAdditionalSpaceAfterEachLogEntry = false, bool showColouredLogs = true, ConsoleColor debugColour = ConsoleColor.White, ConsoleColor infoColour = ConsoleColor.Green, ConsoleColor warningColour = ConsoleColor.Yellow, ConsoleColor errorColour = ConsoleColor.Red, int numberOfRetriesToLogToFile = 3, int retryLoggingToFileEverySeconds = 1)
        {
            MaxLogFileSize = maxLogFileSize;
            LogDirectory = pathToLogFile;
            LogFileName = logFileName;
            LogToConsole = logToConsole;
            LogToFile = logToFile;
            AddAdditionalSpaceAfterEachLogEntry = addAdditionalSpaceAfterEachLogEntry;
            ShowColouredLogs = showColouredLogs;
            DebugColour = debugColour;
            InfoColour = infoColour;
            ErrorColour = errorColour;
            WarningColour = warningColour;
            NumberOfRetriesToLogToFile = numberOfRetriesToLogToFile;
            RetryLoggingToFileEverySeconds = retryLoggingToFileEverySeconds;
        }

        public delegate void Error(object sender, LoggingErrorEventArgs e);
        public event Error OnError;

        public int MaxLogFileSize = 1000000;
        public int NumberOfRetriesToLogToFile { get; set; } = 3;
        public int RetryLoggingToFileEverySeconds { get; set; } = 1;
        public string LogDirectory { get; set; }
        public string LogFileName { get; set; }
        public bool LogToConsole { get; set; }
        public bool LogToFile { get; set; }
        public bool AddAdditionalSpaceAfterEachLogEntry { get; set; } = false;
        public static bool ShowColouredLogs { get; set; } = true;
        public static ConsoleColor DebugColour { get; set; } = ConsoleColor.White;
        public static ConsoleColor InfoColour { get; set; } = ConsoleColor.Green;
        public static ConsoleColor WarningColour { get; set; } = ConsoleColor.Yellow;
        public static ConsoleColor ErrorColour { get; set; } = ConsoleColor.Red;

        public void Log(string message, LogType type, bool showWorkingAnimation = false)
        {
            if (ShowColouredLogs)
            {
                switch (type)
                {
                    case LogType.Debug:
                        Log(message, type, DebugColour, showWorkingAnimation);
                        break;

                    case LogType.Info:
                        Log(message, type, InfoColour, showWorkingAnimation);
                        break;

                    case LogType.Warning:
                        Log(message, type, WarningColour, showWorkingAnimation);
                        break;

                    case LogType.Error:
                        Log(message, type, ErrorColour, showWorkingAnimation);
                        break;
                }
            }
            else
                Log(message, type, ConsoleColor.White, showWorkingAnimation);
        }

        public void Log(string message, LogType type, ConsoleColor consoleColour, bool showWorkingAnimation = false)
        {
            try
            {
                string logMessage = $"{DateTime.Now} {type}: {message}";

                if (ContinueConsoleLogging(type) && LogToConsole)
                {
                    if (AddAdditionalSpaceAfterEachLogEntry)
                        logMessage = String.Concat(logMessage, "\n");

                    try
                    {
                        //TODO: Need to check if running on non windows enviroment here and find different logging for each platform if possible...
                        if (showWorkingAnimation)
                            CLIEngine.ShowWorkingMessage(message, consoleColour, false, 0);
                        else

                            CLIEngine.ShowMessage(message, consoleColour, false, false, 0);
                    }
                    catch (Exception e) { }
                }

                if (ContinueFileLogging(type) && LogToFile)
                {
                    for (int i = 1; i <= NumberOfRetriesToLogToFile; ++i)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(LogDirectory) && !Directory.Exists(LogDirectory))
                                Directory.CreateDirectory(LogDirectory);

                            if (!AddAdditionalSpaceAfterEachLogEntry)
                                logMessage = String.Concat(logMessage, "\n");

                            string fullFileName = LogFileName;
                            FileInfo fileInfo = new FileInfo(fullFileName);

                            if (File.Exists(String.Concat(LogDirectory, "\\", LogFileName)))
                            {
                                //Need to find the latest file.
                                DirectoryInfo dirInfo = new DirectoryInfo(LogDirectory);

                                FileInfo[] files = dirInfo.GetFiles();
                                DateTime latestWriteTime = DateTime.MinValue;

                                foreach (FileInfo file in files)
                                {
                                    if (file.LastWriteTime > latestWriteTime)
                                    {
                                        fullFileName = file.Name;
                                        latestWriteTime = file.LastWriteTime;
                                    }
                                }

                                fileInfo = new FileInfo(String.Concat(LogDirectory, "\\", LogFileName));
                                string ext = fileInfo.Extension;
                                string[] parts = LogFileName.Split('.');
                                string fileName = parts[0];

                                fileInfo = new FileInfo(String.Concat(LogDirectory, "\\", fullFileName));

                                //If the logfile is over its max size then find the next free filename.
                                if (fileInfo != null && fileInfo.Length > MaxLogFileSize)
                                {
                                    int fileNumber = 2;
                                    bool foundFreeFile = false;

                                    while (!foundFreeFile)
                                    {
                                        fullFileName = $"{fileName}{fileNumber}{ext}";

                                        if (!File.Exists(String.Concat(LogDirectory, "\\", fullFileName)))
                                            foundFreeFile = true;
                                        else
                                            fileNumber++;
                                    }
                                }
                            }

                            using (var stream = File.Open(String.Concat(LogDirectory, "\\", fullFileName), FileMode.Append, FileAccess.Write, FileShare.Write))
                            {
                                using (var writer = new StreamWriter(stream))
                                    writer.WriteLine(logMessage);
                            }
                            break;
                        }
                        catch (IOException e) when (i <= NumberOfRetriesToLogToFile)
                        {
                            Thread.Sleep(RetryLoggingToFileEverySeconds * 1000);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError("Error occurred in DefaultLogger.Log method.", ex);
            }
        }

        public void Shutdown()
        {
            //Not needed for DefaultLogger.
        }

        private void HandleError(string message, Exception exception)
        {
            message = string.Concat(message, exception != null ? $". Error Details: {exception}" : "");
            //Logging.Logging.Log(message, LogType.Error);

            OnError.Invoke(this, new LoggingErrorEventArgs { Reason = message, ErrorDetails = exception });

            switch (LogConfig.ErrorHandlingBehaviour)
            {
                case ErrorHandlingBehaviour.AlwaysThrowExceptionOnError:
                    throw new LoggingException(message, exception);

                case ErrorHandlingBehaviour.OnlyThrowExceptionIfNoErrorHandlerSubscribedToOnErrorEvent:
                    {
                        if (OnError == null)
                            throw new LoggingException(message, exception);
                    }
                    break;
            }
        }
    }
}