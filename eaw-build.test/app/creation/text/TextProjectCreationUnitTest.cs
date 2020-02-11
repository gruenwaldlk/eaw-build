using System.IO;
using eaw.build.app.creation.text;
using eaw.build.app.util;
using eaw.build.data.config.text.v2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eaw.build.test.app.creation.text
{
    [TestClass]
    [TestCategory(TestUtility.TEST_TYPE_HOLY)]
    public class TextProjectCreationUnitTest
    {
        [TestMethod]
        [DataRow(TranslationResourceType.Nls)]
        [DataRow(TranslationResourceType.Csv)]
        public void CreateNew_Test(TranslationResourceType t)
        {
            TextProjectCreationUnit textProjectCreationUnit = new TextProjectCreationUnit(Path.GetTempPath(), t);
            ExitCode exitCode = textProjectCreationUnit.CreateNew();
            Assert.AreEqual(ExitCode.Success, exitCode);
        }
    }
}
