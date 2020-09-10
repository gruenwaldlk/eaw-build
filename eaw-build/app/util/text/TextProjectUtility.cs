using System;
using System.Collections.Generic;
using eaw.build.data.config.text.v2;
using pg.dat.utility;

namespace eaw.build.app.util.text
{
    internal static class TextProjectUtility
    {
        internal const string RESOURCE_TYPE_SORTED = "texts";
        internal const string RESOURCE_TYPE_UNSORTED = "credits";
        private const string RESOURCE_TYPE_DELIMITER = ".";
        private const string RESOURCE_FILE_TYPE_NLS = ".nls";
        private const string RESOURCE_FILE_TYPE_CSV = ".csv";

        internal static string GetTranslationResourceFileName(string key, FileType fileType,
            GameType gameType, OverrideType overrideType, TranslationResourceType translationResourceType = TranslationResourceType.Nls)
        {
            switch (translationResourceType)
            {
                case TranslationResourceType.Csv:
                    return $"{FileTypeToString(fileType)}{RESOURCE_TYPE_DELIMITER}{overrideType.ToString().ToLower()}{RESOURCE_TYPE_DELIMITER}{gameType.ToString().ToLower()}{RESOURCE_TYPE_DELIMITER}{key.ToLower()}{RESOURCE_FILE_TYPE_CSV}";
                case TranslationResourceType.Nls:
                    return $"{FileTypeToString(fileType)}{RESOURCE_TYPE_DELIMITER}{overrideType.ToString().ToLower()}{RESOURCE_TYPE_DELIMITER}{gameType.ToString().ToLower()}{RESOURCE_TYPE_DELIMITER}{key.ToLower()}{RESOURCE_FILE_TYPE_NLS}";
                default:
                    throw new ArgumentOutOfRangeException(nameof(translationResourceType), translationResourceType, null);
            }
        }

        internal static string FileTypeToString(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.SortedGameStringFile:
                    return RESOURCE_TYPE_SORTED;
                case FileType.UnsortedCreditsStringFile:
                    return RESOURCE_TYPE_UNSORTED;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileType), fileType, null);
            }
        }

        internal static FileType FileTypeFromString(string fileType)
        {
            if (fileType == null)
            {
                throw new ArgumentNullException(nameof(fileType));
            }
            fileType = fileType.Trim().ToLower();
            switch (fileType)
            {
                case RESOURCE_TYPE_SORTED:
                    return FileType.SortedGameStringFile;
                case RESOURCE_TYPE_UNSORTED:
                    return FileType.UnsortedCreditsStringFile;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileType), fileType, null);
            }
        }

        internal static class EaW
        {
            internal const GameType GAME_TYPE = GameType.EaW;
            internal const OverrideType OVERRIDE_TYPE = OverrideType.Core;
            internal static Dictionary<string, string> GetCultureInfoToTranslationMap()
            {
                Dictionary<string, string> map = new Dictionary<string, string>
                {
                    {De.CULTURE_INFO, De.MASTER_TEXT_FILE},
                    {En.CULTURE_INFO, En.MASTER_TEXT_FILE},
                    {Es.CULTURE_INFO, Es.MASTER_TEXT_FILE},
                    {Fr.CULTURE_INFO, Fr.MASTER_TEXT_FILE},
                    {It.CULTURE_INFO, It.MASTER_TEXT_FILE}
                };
                return map;
            }

            internal static Dictionary<string, string> GetCultureInfoToCreditsMap()
            {
                Dictionary<string, string> map = new Dictionary<string, string>
                {
                    {De.CULTURE_INFO, De.CREDITS_FILE},
                    {En.CULTURE_INFO, En.CREDITS_FILE},
                    {Es.CULTURE_INFO, Es.CREDITS_FILE},
                    {Fr.CULTURE_INFO, Fr.CREDITS_FILE},
                    {It.CULTURE_INFO, It.CREDITS_FILE}
                };
                return map;
            }

            internal static class De
            {
                internal const string CULTURE_INFO = "de-DE";
                internal const string MASTER_TEXT_FILE = "dat.eaw.de.mastertextfile_german.dat";
                internal const string CREDITS_FILE = "dat.eaw.de.creditstext_german.dat";
            }

            internal static class En
            {
                internal const string CULTURE_INFO = "en-GB";
                internal const string MASTER_TEXT_FILE = "dat.eaw.en.mastertextfile_english.dat";
                internal const string CREDITS_FILE = "dat.eaw.en.creditstext_english.dat";
            }

            internal static class Es
            {
                internal const string CULTURE_INFO = "es-ES";
                internal const string MASTER_TEXT_FILE = "dat.eaw.es.mastertextfile_spanish.dat";
                internal const string CREDITS_FILE = "dat.eaw.es.creditstext_spanish.dat";
            }

            internal static class Fr
            {
                internal const string CULTURE_INFO = "fr-FR";
                internal const string MASTER_TEXT_FILE = "dat.eaw.fr.mastertextfile_french.dat";
                internal const string CREDITS_FILE = "dat.eaw.fr.creditstext_french.dat";
            }

            internal static class It
            {
                internal const string CULTURE_INFO = "it-IT";
                internal const string MASTER_TEXT_FILE = "dat.eaw.it.mastertextfile_italian.dat";
                internal const string CREDITS_FILE = "dat.eaw.it.creditstext_italian.dat";
            }
        }

        internal static class FoC
        {
            internal const GameType GAME_TYPE = GameType.FoC;
            internal const OverrideType OVERRIDE_TYPE = OverrideType.Expansion;
            internal static Dictionary<string, string> GetCultureInfoToTranslationMap()
            {
                Dictionary<string, string> map = new Dictionary<string, string>
                {
                    {De.CULTURE_INFO, De.MASTER_TEXT_FILE},
                    {En.CULTURE_INFO, En.MASTER_TEXT_FILE},
                    {Es.CULTURE_INFO, Es.MASTER_TEXT_FILE},
                    {Fr.CULTURE_INFO, Fr.MASTER_TEXT_FILE},
                    {It.CULTURE_INFO, It.MASTER_TEXT_FILE}
                };
                return map;
            }

            internal static Dictionary<string, string> GetCultureInfoToCreditsMap()
            {
                Dictionary<string, string> map = new Dictionary<string, string>
                {
                    {De.CULTURE_INFO, De.CREDITS_FILE},
                    {En.CULTURE_INFO, En.CREDITS_FILE},
                    {Es.CULTURE_INFO, Es.CREDITS_FILE},
                    {Fr.CULTURE_INFO, Fr.CREDITS_FILE},
                    {It.CULTURE_INFO, It.CREDITS_FILE}
                };
                return map;
            }

            internal static class De
            {
                internal const string CULTURE_INFO = "de-DE";
                internal const string MASTER_TEXT_FILE = "dat.foc.de.mastertextfile_german.dat";
                internal const string CREDITS_FILE = "dat.foc.de.creditstext_german.dat";
            }

            internal static class En
            {
                internal const string CULTURE_INFO = "en-GB";
                internal const string MASTER_TEXT_FILE = "dat.foc.en.mastertextfile_english.dat";
                internal const string CREDITS_FILE = "dat.foc.en.creditstext_english.dat";
            }

            internal static class Es
            {
                internal const string CULTURE_INFO = "es-ES";
                internal const string MASTER_TEXT_FILE = "dat.foc.es.mastertextfile_spanish.dat";
                internal const string CREDITS_FILE = "dat.foc.es.creditstext_spanish.dat";
            }

            internal static class Fr
            {
                internal const string CULTURE_INFO = "fr-FR";
                internal const string MASTER_TEXT_FILE = "dat.foc.fr.mastertextfile_french.dat";
                internal const string CREDITS_FILE = "dat.foc.fr.creditstext_french.dat";
            }

            internal static class It
            {
                internal const string CULTURE_INFO = "it-IT";
                internal const string MASTER_TEXT_FILE = "dat.foc.it.mastertextfile_italian.dat";
                internal const string CREDITS_FILE = "dat.foc.it.creditstext_italian.dat";
            }
        }
    }
}
