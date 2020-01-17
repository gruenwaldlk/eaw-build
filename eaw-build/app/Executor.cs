using System;
using eaw.build.app.migration;
using eaw.build.app.migration.mod;
using eaw.build.app.util;
using pg.util;
using Serilog;

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
            Log.Error("The tool was provided with an invalid options set {@Options}", options);
            return ExitCode.Error;
        }

        private static ExitCode Migrate(Builder.MigrationOptions migrationOptions)
        {
            if (migrationOptions == null)
            {
                Log.Error("The tool was provided with an invalid options set {@MigrationOptions}", migrationOptions);
                return ExitCode.MigrationOptionsArgumentError;
            }

            if (migrationOptions.TranslationMigration && migrationOptions.ModProjectMigration)
            {
                Log.Error("The tool was provided with an invalid options set {@MigrationOptions}", migrationOptions);
                return ExitCode.MigrationOptionsArgumentError;
            }

            if (migrationOptions.TranslationMigration)
            {
                Log.Debug("Starting translation project migration {@MigrationOptions}", migrationOptions);
                return MigrateTranslation(migrationOptions.ConfigurationFilePath);
            }

            if (migrationOptions.ModProjectMigration)
            {
                Log.Debug("Starting mod project migration {@MigrationOptions}", migrationOptions);
                return MigrateModProject(migrationOptions.ConfigurationFilePath);
            }

            return ExitCode.Error;
        }

        private static ExitCode MigrateModProject(string configurationFilePath)
        {
            IMigrationUnit migrationUnit = new ModProjectMigrationUnit(configurationFilePath);
            return migrationUnit.Migrate();
        }

        private static ExitCode MigrateTranslation(string configurationFilePath)
        {
            throw new NotImplementedException();
        }
    }
}
