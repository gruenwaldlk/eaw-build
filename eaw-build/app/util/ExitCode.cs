namespace eaw.build.app.util
{
    internal enum ExitCode : int
    {
        Success = 0,
        Error = 1,

        MigrationOptionsArgumentError = 10,

        CommandInvokeCannotExecute = 126,
        CommandNotFound = 127,
        InvalidExitArgument = 128,
        UserTerminated = 130,
        ExitStatusOutOfRange = 255,
    }
}
