using System;
using System.Collections.Generic;

namespace NextGenSoftware.Logging.Interfaces
{
    public interface ILogger
    {
        List<ILogProvider> LogProviders { get; set; }

        void AddLogProvider(ILogProvider logProvider);
        void AddLogProviders(IEnumerable<ILogProvider> logProviders);
        void Log(string message, LogType type);
        void Log(string message, LogType type, bool showWorkingAnimation = false);
        void Log(string message, LogType type, ConsoleColor consoleColour, bool showWorkingAnimation = false);
    }
}