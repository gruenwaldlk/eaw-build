using System.IO;
using eaw.build.app.creation.text;
using eaw.build.app.util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eaw.build.test.app.creation.text
{
    [TestClass]
    [TestCategory(TestUtility.TEST_TYPE_HOLY)]
    public class TextProjectCreationUnitTest
    {
        [TestMethod]
        public void CreateNew_Test()
        {
            TextProjectCreationUnit textProjectCreationUnit = new TextProjectCreationUnit(Path.GetTempPath());
            ExitCode exitCode = textProjectCreationUnit.CreateNew();
            Assert.AreEqual(ExitCode.Success, exitCode);
        }
    }
}
