using System;
using System.IO;
using eaw.build.app.migration.mod;
using eaw.build.app.util;
using eaw.build.app.util.mod;
using eaw.build.app.version.mod;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eaw.build.test.app.migration.mod
{
    [TestClass]
    [TestCategory(TestUtility.TEST_TYPE_HOLY)]
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
            PathUtility.DeleteFile(PathUtility.Combine(TestUtility.GetBasePath(), MIGRATE_TO_FILE));
            PathUtility.DeleteFile(PathUtility.Combine(TestUtility.GetBasePath(),
                ModProjectMigrationUnit.V2.DEFAULT_CONFIGURATION_FILE_NAME));
        }

        [TestMethod]
        public void MigrateV1_Test_MigrationTargetProvided()
        {
            string migrateFromFile = TestUtility.Mod.Config.V1.GetTestConfigFilePath();
            string migrateToFile = PathUtility.Combine(TestUtility.GetBasePath(), MIGRATE_TO_FILE);
            ModProjectMigrationUnit migrationUnit = new ModProjectMigrationUnit(migrateFromFile, migrateToFile);
            migrationUnit.Migrate();
            Assert.IsTrue(PathUtility.FileExists(PathUtility.Combine(TestUtility.GetBasePath(), MIGRATE_TO_FILE)));
            ModProjectVersion actual = ModProjectUtility.GetProjectVersionFromConfigFile(PathUtility.Combine(
                TestUtility.GetBasePath(),
                MIGRATE_TO_FILE));
            Assert.AreEqual(ModProjectVersion.V2, actual);
        }

        [TestMethod]
        public void MigrateV1_Test_MigrationTargetNotProvided()
        {
            string migrateFromFile = TestUtility.Mod.Config.V1.GetTestConfigFilePath();
            ModProjectMigrationUnit migrationUnit = new ModProjectMigrationUnit(migrateFromFile);
            ExitCode exitCode = migrationUnit.Migrate();
            Assert.AreEqual(ExitCode.Success, exitCode);
            Assert.IsTrue(PathUtility.FileExists(PathUtility.Combine(TestUtility.GetBasePath(),
                ModProjectMigrationUnit.V2.DEFAULT_CONFIGURATION_FILE_NAME)));
            ModProjectVersion actual = ModProjectUtility.GetProjectVersionFromConfigFile(PathUtility.Combine(
                TestUtility.GetBasePath(),
                ModProjectMigrationUnit.V2.DEFAULT_CONFIGURATION_FILE_NAME));
            Assert.AreEqual(ModProjectVersion.V2, actual);
        }

        [TestMethod]
        public void GetCurrentVersion_Test()
        {
            ModProjectMigrationUnit modProjectMigrationUnit = new ModProjectMigrationUnit("a", "b");
            Assert.AreNotEqual(modProjectMigrationUnit.GetCurrentVersion(), ModProjectVersion.Invalid);
        }
    }
}
