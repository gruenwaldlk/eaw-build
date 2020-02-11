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
        [DataRow("en-UK", FileType.SortedGameStringFile, GameType.EaW, OverrideType.Core, TranslationResourceType.Nls, "texts.core.eaw.en-uk.nls")]
        [DataRow("de-DE", FileType.UnsortedCreditsStringFile, GameType.EaW, OverrideType.Core, TranslationResourceType.Nls, "credits.core.eaw.de-de.nls")]
        [DataRow("en-UK", FileType.SortedGameStringFile, GameType.EaW, OverrideType.Expansion, TranslationResourceType.Nls, "texts.expansion.eaw.en-uk.nls")]
        [DataRow("en-UK", FileType.SortedGameStringFile, GameType.FoC, OverrideType.Core, TranslationResourceType.Nls, "texts.core.foc.en-uk.nls")]
        public void GetTranslationResourceFileName_Test(string key, FileType fileType,
            GameType gameType, OverrideType overrideType, TranslationResourceType translationResourceType, string expected)
        {
            Assert.AreEqual(expected,
                TextProjectUtility.GetTranslationResourceFileName(key, fileType, gameType, overrideType, translationResourceType));
        }
    }
}
