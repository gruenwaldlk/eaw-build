using System.IO;
using System.Xml.Serialization;
using eaw.build.data.config.mod.v1;
using eaw.build.data.config.mod.v2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eaw.build.test
{
    [TestClass]
    public static class TestUtility
    {
        public const string TEST_TYPE_UTILITY = "Utility";
        public const string TEST_TYPE_HOLY = "Holy";

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            GenerateRequiredFiles();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            DeleteRequiredFiles();
        }

        public static string GetBasePath()
        {
            return Path.GetTempPath();
        }

        private static void DeleteRequiredFiles()
        {
            Mod.Config.V1.DeleteTestFile();
            Mod.Config.V1.DeleteTestFile(false);
        }

        private static void GenerateRequiredFiles()
        {
            Mod.Config.V1.GenerateTestFile();
            Mod.Config.V1.GenerateTestFile(false);
        }

        internal static class Mod
        {
            internal static class Config
            {
                internal static class V1
                {
                    private const string TEST_CONFIG_VALID =
                        "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<ModBuildConfig>\n  <ModSettings Name=\"Test Mod\" ShortName=\"TM\" Version=\"1.2.3\" ReleaseType=\"Full\" />\n  <BuildSettings>\n    <Localisation LocalisationFile=\"TranslationManifest.xml\" />\n    <Bundle Name=\"Bundle01\">\n      <Directory FilePattern=\"*.xml\" Recurse=\"true\">data/xml/test1</Directory>\n      <Directory FilePattern=\"*.xml\" >data/xml/test2</Directory>\n      <File>data/xml/test1.xml</File>\n    </Bundle>\n    <Bundle Name=\"Bundle02\">\n      <Directory FilePattern=\"*.xml\" Recurse=\"true\">data/xml/test3</Directory>\n      <Directory FilePattern=\"*.xml\" >data/xml/test4</Directory>\n      <File>data/xml/test2.xml</File>\n    </Bundle>\n  </BuildSettings>\n</ModBuildConfig>";

                    private const string TEST_CONFIG_INVALID =
                        "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<ModBuildConfig>\n  <ModSettings ShortName=\"TM\" Version=\"1.2.3\" ReleaseType=\"Full\" />\n  <BuildSettings>\n    <Localisation LocalisationFile=\"TranslationManifest.xml\" />\n  </BuildSettings>\n</ModBuildConfig>";

                    private const string TEST_FILE_NAME_VALID = "ModConfigV1TestFile.xml";
                    private const string TEST_FILE_NAME_INVALID = "ModConfigV1TestFileInvalid.xml";

                    internal static string GetTestConfigAsString(bool valid = true)
                    {
                        return valid ? TEST_CONFIG_VALID : TEST_CONFIG_INVALID;
                    }

                    internal static ModBuildConfigType GetTestConfigAsClass()
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ModBuildConfigType));
                        using (StringReader stringReader = new StringReader(GetTestConfigAsString()))
                        {
                            return (ModBuildConfigType) serializer.Deserialize(stringReader);
                        }
                    }

                    internal static string GetTestConfigFilePath(bool valid = true)
                    {
                        return valid
                            ? Path.Combine(GetBasePath(), TEST_FILE_NAME_VALID)
                            : Path.Combine(Path.GetTempPath(), TEST_FILE_NAME_INVALID);
                    }

                    internal static void GenerateTestFile(bool valid = true)
                    {
                        using (StreamWriter writer = new StreamWriter(GetTestConfigFilePath(valid)))
                        {
                            writer.Write(GetTestConfigAsString(valid));
                        }
                    }

                    internal static void DeleteTestFile(bool valid = true)
                    {
                        File.Delete(GetTestConfigFilePath(valid));
                    }
                }

                internal static class V2
                {
                    private const string TEST_CONFIG_VALID = "";
                    private const string TEST_CONFIG_INVALID = "";

                    internal static string GetTestConfigAsString(bool valid = true)
                    {
                        return valid ? TEST_CONFIG_VALID : TEST_CONFIG_INVALID;
                    }

                    internal static ModProjectType GetTestConfigAsClass(bool includeSteamInfo = false)
                    {
                        return new ModProjectType();
                    }
                }
            }
        }
    }
}
