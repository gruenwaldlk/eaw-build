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
        }

        [TestMethod]
        public void MigrateV1_Test()
        {
            string migrateFromFile = TestUtility.Mod.Config.V1.GetTestConfigFilePath();
            string migrateToFile = Path.Combine(TestUtility.GetBasePath(), MIGRATE_TO_FILE);
            ModProjectMigrationUnit migrationUnit = new ModProjectMigrationUnit(migrateFromFile, migrateToFile);
            migrationUnit.Migrate();
            Assert.IsTrue(File.Exists(Path.Combine(TestUtility.GetBasePath(), MIGRATE_TO_FILE)));
            ModProjectVersion actual = ModProjectUtility.GetProjectVersionFromConfigFile(Path.Combine(TestUtility.GetBasePath(),
                MIGRATE_TO_FILE));
            Assert.AreEqual(ModProjectVersion.V2, actual);
        }
    }
}