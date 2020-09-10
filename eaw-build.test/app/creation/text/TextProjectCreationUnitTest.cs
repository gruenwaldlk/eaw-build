using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using eaw.build.app.creation.text;
using eaw.build.app.util;
using eaw.build.app.util.text;
using eaw.build.data.config.text.v2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eaw.build.test.app.creation.text
{
    [TestClass]
    [Ignore]
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
            foreach (KeyValuePair<string, string> keyValuePair in TextProjectUtility.EaW
                .GetCultureInfoToTranslationMap()
                .Where(keyValuePair => File.Exists(PathUtility.GetTempFile(keyValuePair.Value))))
            {
                PathUtility.DeleteFile(PathUtility.GetTempFile(keyValuePair.Value));
            }

            List<string> files = new List<string>();
            files.AddRange(PathUtility.GetFiles(PathUtility.GetTempPath(), "texts.*.nls"));
            files.AddRange(PathUtility.GetFiles(PathUtility.GetTempPath(), "texts.*.csv"));
            files.AddRange(PathUtility.GetFiles(PathUtility.GetTempPath(), "credits.*.csv"));
            files.AddRange(PathUtility.GetFiles(PathUtility.GetTempPath(), "credits.*.nls"));
            foreach (string file in files)
            {
                PathUtility.DeleteFile(file);
            }
        }

        [TestMethod]
        [Ignore] // TODO - [kad]: These tests won't work, because the underlying library cannot handle the abstracted file system.
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
