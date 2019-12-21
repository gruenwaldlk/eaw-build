using eaw.build.app.util;

namespace eaw.build.app.migration
{
    internal interface IMigrator
    {
        ExitCode Migrate();
    }
}
