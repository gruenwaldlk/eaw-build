using eaw.build.app.util;

namespace eaw.build.app.creation
{
    internal interface ICreationUnit<out T>
    {
        T GetCurrentVersion();
        ExitCode CreateNew();
    }
}
