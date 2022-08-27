
namespace NextGenSoftware.Logging
{
    public static class LogConfig
    {
        public static LoggingMode LoggingMode = LoggingMode.WarningsErrorsAndInfo;
        public static ErrorHandlingBehaviour ErrorHandlingBehaviour { get; set; } = ErrorHandlingBehaviour.OnlyThrowExceptionIfNoErrorHandlerSubscribedToOnErrorEvent;
    }
}