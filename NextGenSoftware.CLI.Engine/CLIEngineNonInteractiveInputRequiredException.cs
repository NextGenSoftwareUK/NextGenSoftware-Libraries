using System;

namespace NextGenSoftware.CLI.Engine
{
    /// <summary>
    /// Thrown when <see cref="CLIEngine.NonInteractive"/> is true and a code path attempts to read from the console.
    /// Callers should supply CLI arguments, environment variables, or run without <c>--non-interactive</c>.
    /// </summary>
    public sealed class CLIEngineNonInteractiveInputRequiredException : InvalidOperationException
    {
        public CLIEngineNonInteractiveInputRequiredException(string message)
            : base(message)
        {
        }

        public CLIEngineNonInteractiveInputRequiredException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
