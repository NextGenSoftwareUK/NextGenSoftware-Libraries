
using NextGenSoftware.ErrorHandling;

namespace NextGenSoftware.Logging
{
    public static class LogConfig
    {
        public static LoggingMode LoggingMode = LoggingMode.WarningsErrorsAndInfo;
        // public static ErrorHandlingBehaviour ErrorHandlingBehaviour { get; set; } = ErrorHandlingBehaviour.OnlyThrowExceptionIfNoErrorHandlerSubscribedToOnErrorEvent;
        
        public static ErrorHandlingBehaviour ErrorHandlingBehaviour
        {
            get
            {
                return ErrorHandling.ErrorHandling.ErrorHandlingBehaviour;
            }
            set
            {
                ErrorHandling.ErrorHandling.ErrorHandlingBehaviour = value;
            }
        }
    }
}