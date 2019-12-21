using eaw.build.app.util;

namespace eaw.build.app
{
    internal sealed class Executor
    {
        internal static ExitCode Run(Options options)
        {
            if (options.InternalType == typeof(Builder.MigrationOptions))
            {
                Builder.MigrationOptions o = (Builder.MigrationOptions) options.Option;
                return Migrate(o);
            }

            return ExitCode.Error;
        }

        private static ExitCode Migrate(Builder.MigrationOptions migrationOptions)
        {
            if (migrationOptions == null)
            {
                return ExitCode.MigrationOptionsArgumentError;
            }

            if (migrationOptions.TranslationMigration && migrationOptions.ModProjectMigration)
            {
                return ExitCode.MigrationOptionsArgumentError;
            }

            if (migrationOptions.TranslationMigration)
            {
                return MigrateTranslation(migrationOptions.ConfigurationFilePath);
            }

            if (migrationOptions.ModProjectMigration)
            {
                return MigrateModProject(migrationOptions.ConfigurationFilePath);
            }

            return ExitCode.Error;
        }

        private static ExitCode MigrateModProject(string configurationFilePath)
        {
            return ExitCode.Success;
        }

        private static ExitCode MigrateTranslation(string configurationFilePath)
        {
            return ExitCode.Success;
        }
    }
}
