using Microsoft.VisualStudio.TestTools.UnitTesting;
using eaw.build.app.util;

namespace eaw.build.test
{
    [TestClass]
    public class BuilderTest
    {
        [TestMethod]
        [DataRow("migrate", "-t", "I:/path/to/somewhere/translationmanifest.xml")]
        [DataRow("migrate", "--text", "I:/path/to/somewhere/translationmanifest.xml")]
        [DataRow("migrate", "-m", "I:/path/to/somewhere/some.xml")]
        [DataRow("migrate", "--mod", "I:/path/to/somewhere/some.xml")]
        public void Main_Test(string verb, string option, string value1)
        {
            string[] args = {verb, option, value1};
            int exitCode = Builder.Main(args);
            Assert.AreEqual((int) ExitCode.Success, exitCode);
        }
    }
}
