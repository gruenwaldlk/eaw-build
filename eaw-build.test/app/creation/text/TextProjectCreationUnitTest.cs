using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using eaw.build.app.creation.text;
using eaw.build.app.util;
using eaw.build.app.util.text;
using eaw.build.data.config.text.v2;
using eaw.build.test.app.util.text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eaw.build.test.app.creation.text
{
    [TestClass]
    [TestCategory(TestUtility.TEST_TYPE_HOLY)]
    public class TextProjectCreationUnitTest
    {
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
            foreach (KeyValuePair<string, string> keyValuePair in TextProjectUtility.EaW.GetCultureInfoToTranslationMap().Where(keyValuePair => File.Exists(FilePathUtility.GetTempFile(keyValuePair.Value))))
            {
                File.Delete(FilePathUtility.GetTempFile(keyValuePair.Value));
            }

            List<string> files = new List<string>();
            string[] nlsTextFiles = Directory.GetFiles(FilePathUtility.GetTempPath(), "texts.*.nls");
            string[] csvTextFiles = Directory.GetFiles(FilePathUtility.GetTempPath(), "texts.*.csv");
            string[] csvCreditsFiles = Directory.GetFiles(FilePathUtility.GetTempPath(), "credits.*.csv");
            string[] nlsCreditsFiles = Directory.GetFiles(FilePathUtility.GetTempPath(), "credits.*.nls");
            files.AddRange(nlsTextFiles);
            files.AddRange(csvTextFiles);
            files.AddRange(nlsCreditsFiles);
            files.AddRange(csvCreditsFiles);
            foreach (string file in files)
            {
                File.Delete(file);
            }
        }

        [TestMethod]
        [DataRow(TranslationResourceType.Nls)]
        [DataRow(TranslationResourceType.Csv)]
        public void CreateNew_Test(TranslationResourceType t)
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix ||
                Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                Assert.Inconclusive("Not a Windows system. Test Skipped.");
            }
            TextProjectCreationUnit textProjectCreationUnit = new TextProjectCreationUnit(Path.GetTempPath(), t);
            ExitCode exitCode = textProjectCreationUnit.CreateNew();
            Assert.AreEqual(ExitCode.Success, exitCode);
        }
    }
}
