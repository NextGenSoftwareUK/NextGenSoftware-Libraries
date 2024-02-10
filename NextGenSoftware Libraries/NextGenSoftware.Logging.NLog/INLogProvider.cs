
namespace NextGenSoftware.Logging.NLogger
{
    public interface INLogProvider
    {
        void Log(string message, LogType type, bool showWorkingAnimation = false);
        void Log(string message, LogType type, ConsoleColor consoleColour, bool showWorkingAnimation = false);
        void Shutdown();
    }
}