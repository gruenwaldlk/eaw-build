using Serilog;

namespace eaw.build.app.util
{
    internal static class LogUtility
    {
        internal static ILogger GetLogger(bool verbose = false)
        {
#if DEBUG
            if (verbose)
            {
                return new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .WriteTo.Console()
                    .CreateLogger();
            }

            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
#else
            if (verbose)
            {
                return new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .WriteTo.Console()
                    .CreateLogger();
            }
            return new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.Console()
                .CreateLogger();
#endif
        }
    }
}
