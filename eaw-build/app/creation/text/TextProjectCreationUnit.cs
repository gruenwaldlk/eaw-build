using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using eaw.build.app.util;
using eaw.build.app.util.text;
using eaw.build.app.version.text;
using eaw.build.data.config.text.v2;
using pg.dat;
using pg.dat.utility;
using Serilog;

namespace eaw.build.app.creation.text
{
    internal class TextProjectCreationUnit : ICreationUnit<TextProjectVersion>
    {
        private const TextProjectVersion CURRENT_VERSION = TextProjectVersion.V2;

        internal string TextProjectPath { get; }
        internal TranslationResourceType TranslationResourceType { get; }

        public TextProjectCreationUnit(string textProjectPath, TranslationResourceType translationResourceType = TranslationResourceType.Nls)
        {
            TextProjectPath = textProjectPath;
            TranslationResourceType = translationResourceType;
        }

        public TextProjectVersion GetCurrentVersion()
        {
            return CURRENT_VERSION;
        }

        public ExitCode CreateNew()
        {
            int errors = 0;
            foreach ((string locale, string translationFileName) in TextProjectUtility.EaW.GetCultureInfoToTranslationMap())
            {
                try
                {
                    CreateAllTranslationResources(translationFileName, locale, TranslationResourceType);
                }
                catch (Exception e)
                {
                    errors += 1;
                    Log.Error("An error occured during streaming a file to disc: {}", e);
                }
            }
            return errors > 0 ? ExitCode.TextProjectCreationCompletedWithErrors : ExitCode.Success;
        }

        private void CreateAllTranslationResources(string translationFileName, string locale, TranslationResourceType translationResourceType = TranslationResourceType.Nls)
        {
            StreamTextFileToDisc(translationFileName);
            StreamTextFileToDisc(TextProjectUtility.FoC.GetCultureInfoToTranslationMap()[locale]);
            IEnumerable<Translation> translationsEaW =
                DatFileUtility.Import(FilePathUtility.GetTempFile(translationFileName), new CultureInfo(locale));
            Dictionary<string, string> eawTranslationMap = new Dictionary<string, string>();
            foreach (Translation translation in translationsEaW)
            {
                eawTranslationMap.TryAdd(translation.Key, translation.Value);
            }

            IEnumerable<Translation> translationsFoC =
                DatFileUtility.Import(
                    FilePathUtility.GetTempFile(TextProjectUtility.FoC.GetCultureInfoToTranslationMap()[locale]),
                    new CultureInfo(locale));
            List<Translation> cleanedTranslationsFoC = new List<Translation>();
            foreach (Translation translation in translationsFoC)
            {
                if (!eawTranslationMap.ContainsKey(translation.Key))
                {
                    cleanedTranslationsFoC.Add(translation);
                }
                else
                {
                    string tEaW = eawTranslationMap[translation.Key];
                    if (string.Compare(translation.Value, tEaW, translation.Locale, CompareOptions.IgnoreSymbols) != 0)
                    {
                        cleanedTranslationsFoC.Add(translation);
                    }
                }
            }

            CreateTranslationResource(translationsEaW, locale, FileType.SortedGameStringFile, TextProjectUtility.EaW.GAME_TYPE,
                TextProjectUtility.EaW.OVERRIDE_TYPE, translationResourceType);
            CreateTranslationResource(cleanedTranslationsFoC, locale, FileType.SortedGameStringFile,
                TextProjectUtility.FoC.GAME_TYPE,
                TextProjectUtility.FoC.OVERRIDE_TYPE, translationResourceType);
            if (File.Exists(FilePathUtility.GetTempFile(translationFileName)))
            {
                File.Delete(FilePathUtility.GetTempFile(translationFileName));
            }

            if (File.Exists(FilePathUtility.GetTempFile(TextProjectUtility.FoC.GetCultureInfoToTranslationMap()[locale])))
            {
                File.Delete(FilePathUtility.GetTempFile(TextProjectUtility.FoC.GetCultureInfoToTranslationMap()[locale]));
            }
        }

        private void CreateTranslationResource(IEnumerable<Translation> translations, string key, FileType fileType,
            GameType gameType, OverrideType overrideType, TranslationResourceType translationResourceType = TranslationResourceType.Nls)
        {
            switch (translationResourceType)
            {
                case TranslationResourceType.Csv:
                    CreateTranslationResourceCsv(translations, key, fileType, gameType, overrideType);
                    break;
                case TranslationResourceType.Nls:
                    CreateTranslationResourceNls(translations, key, fileType, gameType, overrideType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(translationResourceType), translationResourceType, null);
            }

        }

        private void CreateTranslationResourceNls(IEnumerable<Translation> translations, string key, FileType fileType, GameType gameType,
            OverrideType overrideType)
        {
            using (StreamWriter writer = new StreamWriter(FilePathUtility.Combine(TextProjectPath,
                TextProjectUtility.GetTranslationResourceFileName(key, fileType, gameType, overrideType))))
            {
                foreach (Translation translation in translations)
                {
                    writer.WriteLine($"{translation.Key}={translation.Value}");
                }
            }
        }

        private void CreateTranslationResourceCsv(IEnumerable<Translation> translations, string key, FileType fileType, GameType gameType,
            OverrideType overrideType)
        {
            using (StreamWriter writer = new StreamWriter(FilePathUtility.Combine(TextProjectPath,
                TextProjectUtility.GetTranslationResourceFileName(key, fileType, gameType, overrideType, TranslationResourceType.Csv))))
            {
                writer.WriteLine($"KEY, VALUE");
                foreach (Translation translation in translations)
                {
                    writer.WriteLine($"{translation.Key}, {translation.Value}");
                }
            }
        }

        private static void StreamTextFileToDisc(string resourceName)
        {
            using (Stream tmpFile =
                ResourceUtility.GetResourceAsStreamByFileName(resourceName))
            {
                using (Stream f = File.Create(FilePathUtility.GetTempFile(resourceName)))
                {
                    tmpFile.CopyTo(f);
                }
            }
        }
    }
}
