using eaw.build.app.util.game;
using eaw.build.app.util.mod;
using eaw.build.app.version.mod;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eaw.build.test.app.util.mod
{
    [TestClass]
    [TestCategory(TestUtility.TEST_TYPE_UTILITY)]
    public class ModProjectUtilityTest
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
        }

        [TestMethod]
        public void GetProjectVersionFromConfigFile_Test_Expected_V1()
        {
            ModProjectVersion modProjectVersion =
                ModProjectUtility.GetProjectVersionFromConfigFile(TestUtility.Mod.Config.V1.GetTestConfigFilePath());
            Assert.AreEqual(ModProjectVersion.V1, modProjectVersion);
        }

        [TestMethod]
        public void GetProjectVersionFromConfigFile_Test_Expected_Invalid_V1()
        {
            ModProjectVersion modProjectVersion =
                ModProjectUtility.GetProjectVersionFromConfigFile(TestUtility.Mod.Config.V1.GetTestConfigFilePath(false));
            Assert.AreEqual(ModProjectVersion.Invalid, modProjectVersion);
        }

        [TestMethod]
        [DataRow("test1","test2")]
        [DataRow("test2","test3")]
        [DataRow("test3","test4")]
        public void CheckAndUpdateBundleName_Test_NoConflict(string s1, string s2)
        {
            string expected1 = s1;
            string expected2 = s2;
            string actual1 = ModProjectUtility.CheckAndUpdateBundleName(s1);
            string actual2 = ModProjectUtility.CheckAndUpdateBundleName(s2);
            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
            Assert.AreNotEqual(actual1, actual2);
        }

        [TestMethod]
        [DataRow("test1","test1")]
        [DataRow("test2","test2")]
        [DataRow("test3","test3")]
        public void CheckAndUpdateBundleName_Test_ConflictBetween(string s1, string s2)
        {
            string expected = s1;
            string actual1 = ModProjectUtility.CheckAndUpdateBundleName(s1);
            string actual2 = ModProjectUtility.CheckAndUpdateBundleName(s2);
            Assert.AreEqual(expected, actual1);
            Assert.AreNotEqual(expected, actual2);
            Assert.AreNotEqual(actual1, actual2);
        }

        [TestMethod]
        public void CheckAndUpdateBundleName_Test_ConflictWithBaseGameBundle()
        {
            foreach (string bundleName in StarWarsGame.EmpireAtWar.RESERVED_BUNDLE_NAMES)
            {
                string actual = ModProjectUtility.CheckAndUpdateBundleName(bundleName);
                Assert.AreNotEqual(bundleName, actual);
            }
        }

        [TestMethod]
        public void CheckAndUpdateBundleName_Test_ConflictWithExpansionBundle()
        {
            foreach (string bundleName in StarWarsGame.ForcesOfCorruption.RESERVED_BUNDLE_NAMES)
            {
                string actual = ModProjectUtility.CheckAndUpdateBundleName(bundleName);
                Assert.AreNotEqual(bundleName, actual);
            }
        }
    }
}
