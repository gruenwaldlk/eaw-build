using System.IO;
using eaw.build.app.migration.mod;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eaw.build.app.util;
using eaw.build.app.util.mod;

namespace eaw.build.test
{
    [TestClass]
    public class BuilderTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            ModProjectUtility.Reset();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            ModProjectUtility.Reset();
            CleanupMigrationFiles();
        }

        private static void CleanupMigrationFiles()
        {
            if (File.Exists(Path.Combine(TestUtility.GetBasePath(),
                ModProjectMigrationUnit.V1.DEFAULT_CONFIGURATION_FILE_NAME)))
            {
                File.Delete(Path.Combine(TestUtility.GetBasePath(),
                    ModProjectMigrationUnit.V1.DEFAULT_CONFIGURATION_FILE_NAME));
            }

            if (File.Exists(Path.Combine(TestUtility.GetBasePath(),
                ModProjectMigrationUnit.V2.DEFAULT_CONFIGURATION_FILE_NAME)))
            {
                File.Delete(Path.Combine(TestUtility.GetBasePath(),
                    ModProjectMigrationUnit.V2.DEFAULT_CONFIGURATION_FILE_NAME));
            }
        }

        [TestMethod]
        [Ignore]
        [DataRow("migrate", "-m")]
        [DataRow("migrate", "--mod")]
        public void Main_TestMigrationMod(string verb, string option)
        {
            string[] args = {verb, option, TestUtility.Mod.Config.V1.GetTestConfigFilePath()};
            int exitCode = Builder.Main(args);
            Assert.AreEqual((int) ExitCode.Success, exitCode);
        }

        [TestMethod]
        [Ignore]
        [DataRow("migrate", "-t", "I:/path/to/somewhere/translationmanifest.xml")]
        [DataRow("migrate", "--text", "I:/path/to/somewhere/translationmanifest.xml")]
        public void Main_TestMigrationTranslation(string verb, string option, string value1)
        {
            string[] args = {verb, option, value1};
            int exitCode = Builder.Main(args);
            Assert.AreEqual((int) ExitCode.Success, exitCode);
        }
    }
}
