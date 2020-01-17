using System;
using System.IO;
using eaw.build.app.migration.mod;
using eaw.build.app.util.mod;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eaw.build.test.app.migration.mod
{
    [TestClass]
    public class ModProjectMigrationUnitTest
    {
        private const string MIGRATE_TO_FILE = "ModProjectMigrationUnitTest_V1_Migration.xml";

        [TestInitialize]
        public void TestInitialise()
        {
            CleanupMigrationFiles();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            CleanupMigrationFiles();
        }

        private static void CleanupMigrationFiles()
        {
            if (File.Exists(Path.Combine(TestUtility.GetBasePath(), MIGRATE_TO_FILE)))
            {
                File.Delete(Path.Combine(TestUtility.GetBasePath(), MIGRATE_TO_FILE));
            }

            if (File.Exists(Path.Combine(TestUtility.GetBasePath(),
                ModProjectMigrationUnit.V2.DEFAULT_CONFIGURATION_FILE_NAME)))
            {
                File.Delete(Path.Combine(TestUtility.GetBasePath(),
                    ModProjectMigrationUnit.V2.DEFAULT_CONFIGURATION_FILE_NAME));
            }
        }

        [TestMethod]
        public void MigrateV1_Test_MigrationTargetProvided()
        {
            string migrateFromFile = TestUtility.Mod.Config.V1.GetTestConfigFilePath();
            string migrateToFile = Path.Combine(TestUtility.GetBasePath(), MIGRATE_TO_FILE);
            ModProjectMigrationUnit migrationUnit = new ModProjectMigrationUnit(migrateFromFile, migrateToFile);
            migrationUnit.Migrate();
            Assert.IsTrue(File.Exists(Path.Combine(TestUtility.GetBasePath(), MIGRATE_TO_FILE)));
            ModProjectVersion actual = ModProjectUtility.GetProjectVersionFromConfigFile(Path.Combine(
                TestUtility.GetBasePath(),
                MIGRATE_TO_FILE));
            Assert.AreEqual(ModProjectVersion.V2, actual);
        }

        [TestMethod]
        public void MigrateV1_Test_MigrationTargetNotProvided()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix ||
                Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                Assert.Inconclusive("Not a Windows platform. Test Skipped.");
            }
            string migrateFromFile = TestUtility.Mod.Config.V1.GetTestConfigFilePath();
            ModProjectMigrationUnit migrationUnit = new ModProjectMigrationUnit(migrateFromFile);
            migrationUnit.Migrate();
            Assert.IsTrue(File.Exists(Path.Combine(TestUtility.GetBasePath(),
                ModProjectMigrationUnit.V2.DEFAULT_CONFIGURATION_FILE_NAME)));
            ModProjectVersion actual = ModProjectUtility.GetProjectVersionFromConfigFile(Path.Combine(
                TestUtility.GetBasePath(),
                ModProjectMigrationUnit.V2.DEFAULT_CONFIGURATION_FILE_NAME));
            Assert.AreEqual(ModProjectVersion.V2, actual);
        }
    }
}
