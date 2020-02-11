using eaw.build.app.util;
using eaw.build.app.version.text;

namespace eaw.build.app.migration.text
{
    internal class TextProjectMigrationUnit : IMigrationUnit<TextProjectVersion>
    {
        private const TextProjectVersion CURRENT_VERSION = TextProjectVersion.V2;

        public TextProjectVersion GetCurrentVersion()
        {
            return CURRENT_VERSION;
        }

        public ExitCode Migrate()
        {
            throw new System.NotImplementedException();
        }
    }
}
