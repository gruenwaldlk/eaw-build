using eaw.build.app.util;

namespace eaw.build.app.migration
{
    internal interface IMigrationUnit<out T>
    {
        T GetCurrentVersion();
        ExitCode Migrate();
    }
}
