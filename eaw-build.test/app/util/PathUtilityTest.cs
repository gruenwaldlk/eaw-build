using System;
using System.IO;
using eaw.build.app.util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eaw.build.test.app.util
{
    [TestClass]
    [TestCategory(TestUtility.TEST_TYPE_UTILITY)]
    public class PathUtilityTest
    {

        [TestMethod]
        [DataRow(null, "")]
        [DataRow("", "")]
        [DataRow("C:\\a\\test\\file.exe", "C:\\a\\test")]
        public void GetDirectoryPathFromFilePath_TestWindows(string filePath, string expectedPath)
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix ||
                Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                Assert.Inconclusive("Not a Windows system. Test Skipped.");
            }

            string actual = PathUtility.GetDirectoryPathFromFilePath(filePath);
            Assert.AreEqual(expectedPath, actual);
        }

        [TestMethod]
        [DataRow(null, "")]
        [DataRow("", "")]
        [DataRow("/a/test/file.exe", "/a/test")]
        public void GetDirectoryPathFromFilePath_TestLinux(string filePath, string expectedPath)
        {
            if (Environment.OSVersion.Platform != PlatformID.Unix)
            {
                Assert.Inconclusive("Not a Unix system. Test Skipped.");
            }

            string actual = PathUtility.GetDirectoryPathFromFilePath(filePath);
            Assert.AreEqual(expectedPath, actual);
        }

        [TestMethod]
        [DataRow("", "", new string[0])]
        [DataRow("C:\\some\\file\\path.exe", "C:/some/file\\path.exe", new string[0])]
        [DataRow("C:\\some\\file\\path.exe", "C:/some/", new[] {"file", "\\path.exe"})]
        [DataRow("C:\\some\\file\\path.exe", "C:", new[] {"some/file", "\\path.exe"})]
        [DataRow("C:\\some\\file\\path.exe", "C:\\some/", new[] {"file", "\\path.exe"})]
        [DataRow("C:\\some\\other\\file\\path.exe", "C:\\some/", new[] {"other\\", "file", "\\path.exe"})]
        [DataRow("C:\\some\\other\\file\\path.exe", "C:\\some/", new[] {"other\\", "", "file", "/", "\\path.exe"})]
        public void Combine_TestSuccessWindows(string expected, string requiredBase, params string[] strings)
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix ||
                Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                Assert.Inconclusive("Not a Windows system. Test Skipped.");
            }

            string actual = PathUtility.Combine(requiredBase, strings);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("", "", new string[0])]
        [DataRow("C:/some/file/path.exe", "C:/some/file\\path.exe", new string[0])]
        [DataRow("C:/some/file/path.exe", "C:/some/", new[] {"file", "\\path.exe"})]
        [DataRow("C:/some/file/path.exe", "C:", new[] {"some/file", "\\path.exe"})]
        [DataRow("C:/some/file/path.exe", "C:\\some/", new[] {"file", "\\path.exe"})]
        [DataRow("C:/some/other/file/path.exe", "C:\\some/", new[] {"other\\", "file", "\\path.exe"})]
        [DataRow("C:/some/other/file/path.exe", "C:\\some/", new[] {"other\\", "", "file", "/", "\\path.exe"})]
        public void Combine_TestSuccessLinux(string expected, string requiredBase, params string[] strings)
        {
            if (Environment.OSVersion.Platform != PlatformID.Unix)
            {
                Assert.Inconclusive("Not a Unix system. Test Skipped.");
            }

            string actual = PathUtility.Combine(requiredBase, strings);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("", "")]
        [DataRow("C:\\some\\file\\path.exe", "C:/some/file\\path.exe")]
        [DataRow("C:\\some\\file\\path.exe", "C:/some/other/../file\\path.exe")]
        [DataRow("C:\\some\\file\\path.exe", "C:/some/file\\other\\../path.exe")]
        public void ValidatePath_TestSuccessWindows(string expected, string path)
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix ||
                Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                Assert.Inconclusive("Not a Windows system. Test Skipped.");
            }

            string actual = PathUtility.ValidatePath(path);
            Assert.AreEqual(expected, actual);
        }


        // [Kad] -- TODO: Reenable as soon as a workaround for Linux was implemented.
        [TestMethod]
        [Ignore]
        [DataRow("", "")]
        [DataRow("C:/some/file/path.exe", "C:/some/file\\path.exe")]
        [DataRow("C:/some/file/path.exe", "C:/some/other/../file\\path.exe")]
        [DataRow("C:/some/file/path.exe", "C:/some/file\\other\\../path.exe")]
        public void ValidatePath_TestSuccessLinux(string expected, string path)
        {
            if (Environment.OSVersion.Platform != PlatformID.Unix)
            {
                Assert.Inconclusive("Not a Unix system. Test Skipped.");
            }

            string actual = PathUtility.ValidatePath(path);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTempPath_Test()
        {
            string tempPath = PathUtility.GetTempPath();
            Assert.IsTrue(PathUtility.DirectoryExists(tempPath));
        }

        [TestMethod]
        public void GetTempFile_Test()
        {
            string tempPath = PathUtility.GetTempFile();
            PathUtility.FileCreate(tempPath);
            Assert.IsTrue(PathUtility.FileExists(tempPath));
        }
    }
}
