
namespace NextGenSoftware.Logging.NLogger
{
    public interface INLogProvider
    {
        void Log(string message, LogType type, bool showWorkingAnimation = false, bool logOnNewLine = true, bool insertExtraNewLineAfterLogMessage = false, int? indentLogMessagesBy = 1);
        void Log(string message, LogType type, ConsoleColor consoleColour, bool showWorkingAnimation = false, bool logOnNewLine = true, bool insertExtraNewLineAfterLogMessage = false, int? indentLogMessagesBy = 1);
        void Shutdown();
    }
}