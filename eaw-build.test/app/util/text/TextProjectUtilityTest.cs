using System;
using eaw.build.app.util.text;
using eaw.build.data.config.text.v2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pg.dat.utility;

namespace eaw.build.test.app.util.text
{
    [TestClass]
    [TestCategory(TestUtility.TEST_TYPE_UTILITY)]
    public class TextProjectUtilityTest
    {
        [TestMethod]
        [DataRow("en-UK", FileType.SortedGameStringFile, GameType.EaW, OverrideType.Core, TranslationResourceType.Nls,
            "texts.core.eaw.en-uk.nls")]
        [DataRow("de-DE", FileType.UnsortedCreditsStringFile, GameType.EaW, OverrideType.Core,
            TranslationResourceType.Nls, "credits.core.eaw.de-de.nls")]
        [DataRow("en-UK", FileType.SortedGameStringFile, GameType.EaW, OverrideType.Expansion,
            TranslationResourceType.Nls, "texts.expansion.eaw.en-uk.nls")]
        [DataRow("en-UK", FileType.SortedGameStringFile, GameType.FoC, OverrideType.Core, TranslationResourceType.Nls,
            "texts.core.foc.en-uk.nls")]
        [DataRow("en-UK", FileType.SortedGameStringFile, GameType.EaW, OverrideType.Core, TranslationResourceType.Csv,
            "texts.core.eaw.en-uk.csv")]
        [DataRow("de-DE", FileType.UnsortedCreditsStringFile, GameType.EaW, OverrideType.Core,
            TranslationResourceType.Csv, "credits.core.eaw.de-de.csv")]
        [DataRow("en-UK", FileType.SortedGameStringFile, GameType.EaW, OverrideType.Expansion,
            TranslationResourceType.Csv, "texts.expansion.eaw.en-uk.csv")]
        [DataRow("en-UK", FileType.SortedGameStringFile, GameType.FoC, OverrideType.Core, TranslationResourceType.Csv,
            "texts.core.foc.en-uk.csv")]
        public void GetTranslationResourceFileName_Test(string key, FileType fileType,
            GameType gameType, OverrideType overrideType, TranslationResourceType translationResourceType,
            string expected)
        {
            Assert.AreEqual(expected,
                TextProjectUtility.GetTranslationResourceFileName(key, fileType, gameType, overrideType,
                    translationResourceType));
        }

        [TestMethod]
        [DataRow(FileType.SortedGameStringFile, TextProjectUtility.RESOURCE_TYPE_SORTED)]
        [DataRow(FileType.UnsortedCreditsStringFile, TextProjectUtility.RESOURCE_TYPE_UNSORTED)]
        public void FileTypeToString_Test(FileType ft, string expected)
        {
            Assert.AreEqual(expected, TextProjectUtility.FileTypeToString(ft));
        }

        [TestMethod]
        [DataRow(TextProjectUtility.RESOURCE_TYPE_SORTED, FileType.SortedGameStringFile)]
        [DataRow(TextProjectUtility.RESOURCE_TYPE_UNSORTED, FileType.UnsortedCreditsStringFile)]
        [DataRow("texts", FileType.SortedGameStringFile)]
        [DataRow("credits", FileType.UnsortedCreditsStringFile)]
        public void FileTypeFromString_TestSuccess(string ft, FileType expected)
        {
            Assert.AreEqual(expected, TextProjectUtility.FileTypeFromString(ft));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileTypeFromString_TestArgumentNullException()
        {
            TextProjectUtility.FileTypeFromString(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow("Test")]
        [DataRow("test")]
        [DataRow("some other stuff")]
        [DataRow("some OTHER OTHER stuff")]
        public void FileTypeFromString_TestArgumentOutOfRangeException(string s)
        {
            TextProjectUtility.FileTypeFromString(s);
        }
    }
}
