using System;
using eaw.build.app.util;

namespace eaw.build.app.migration
{
    internal interface IMigrationUnit
    {
        ExitCode Migrate();
    }
}