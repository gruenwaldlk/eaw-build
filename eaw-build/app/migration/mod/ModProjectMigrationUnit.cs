using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using eaw.build.app.util;
using eaw.build.app.util.mod;
using eaw.build.app.util.xml;
using eaw.build.app.version.mod;
using eaw.build.data.config.mod.v1;
using eaw.build.data.config.mod.v2;
using Serilog;

namespace eaw.build.app.migration.mod
{
    internal class ModProjectMigrationUnit : IMigrationUnit<ModProjectVersion>
    {
        private const ModProjectVersion CURRENT_VERSION = ModProjectVersion.V2;
        private const string MISSING = "MISSING";
        private const string MISSING_VERSION = "0.1.0";

        public ModProjectVersion GetCurrentVersion()
        {
            return CURRENT_VERSION;
        }

        internal string OldFilePath { get; }

        internal string NewFilePath { get; private set; }

        internal ModProjectMigrationUnit(string oldFilePath)
        {
            OldFilePath = oldFilePath;
        }

        internal ModProjectMigrationUnit(string oldFilePath, string newFilePath)
        {
            OldFilePath = oldFilePath;
            NewFilePath = newFilePath;
        }

        public ExitCode Migrate()
        {
            ModProjectVersion oldProjectVersion = ModProjectUtility.GetProjectVersionFromConfigFile(OldFilePath);
            switch (oldProjectVersion)
            {
                case ModProjectVersion.V1:
                    return MigrateV1();
                case ModProjectVersion.V2:
                    return MigrateV2();
                case ModProjectVersion.Invalid:
                    return ExitCode.Error;
                default:
                    return ExitCode.Error;
            }
        }

        private ExitCode MigrateV1()
        {
            Log.Information($"Migrating mod project from {ModProjectVersion.V1} to {CURRENT_VERSION} ...");
            try
            {
                ModBuildConfigType config = XmlUtility.ReadAndValidateXmlFile<ModBuildConfigType>(V1.XSD, OldFilePath);
                ModProjectType modProjectType = new ModProjectType
                {
                    ModInfo = V1.ExtractModInfoType(config), BuildSettings = V1.ExtractBuildSettingsType(config)
                };
                if (string.IsNullOrWhiteSpace(NewFilePath))
                {
                    NewFilePath = PathUtility.Combine(PathUtility.GetDirectoryPathFromFilePath(OldFilePath),
                        V2.DEFAULT_CONFIGURATION_FILE_NAME);
                    Log.Debug("Migrating to {NewFilePath}", NewFilePath);
                }
                XmlUtility.WriteXmlFile(NewFilePath, modProjectType);
                Log.Information("Migration finished successfully.");
                return ExitCode.Success;
            }
            catch (Exception e)
            {
                Log.Error(e, "");
            }
            return ExitCode.Error;
        }

        internal static class V1
        {
            internal const string XSD = "v1.yvaw-build.xsd";
            internal const string DEFAULT_TRANSLATION_MANIFEST = "TranslationManifest.xml";
            internal const string DEFAULT_CONFIGURATION_FILE_NAME = "build.cfg";

            internal static ModInfoType ExtractModInfoType(ModBuildConfigType config)
            {
                return new ModInfoType
                {
                    Name = ExtractModNameType(config),
                    Version = ExtractModVersionType(config),
                    Description = ExtractModDescriptionType(config),
                    IconPath = MISSING
                };
            }

            private static ModNameType ExtractModNameType(ModBuildConfigType config)
            {
                string modLongName = config.ModSettings.Name ?? MISSING;
                string modShortName = config.ModSettings.ShortName ??
                                      modLongName.Replace(" ", string.Empty);
                return new ModNameType {LongName = modLongName, ShortName = modShortName};
            }

            private static ModVersionType ExtractModVersionType(ModBuildConfigType config)
            {
                string modVersion = config.ModSettings.Version ?? MISSING_VERSION;
                string[] modVersionSplit = modVersion.Split('.');
                int majorVersion = int.Parse(modVersionSplit[0] ?? "0");
                int minorVersion = int.Parse(modVersionSplit[1] ?? "1");
                int patchVersion = int.Parse(modVersionSplit[2] ?? "0");
                return
                    new ModVersionType {Major = majorVersion, Minor = minorVersion, Patch = patchVersion, Build = 0};
            }

            private static ModDescriptionType ExtractModDescriptionType(ModBuildConfigType config)
            {
                return new ModDescriptionType {Description = MISSING, Summary = MISSING};
            }

            internal static eaw.build.data.config.mod.v2.BuildSettingsType ExtractBuildSettingsType(
                ModBuildConfigType config)
            {
                eaw.build.data.config.mod.v2.BuildSettingsType buildSettingsType =
                    new eaw.build.data.config.mod.v2.BuildSettingsType
                    {
                        CookSettings = ExtractCookSettingsType(config),
                        LocalisationSettings = new LocalisationSettingsType
                        {
                            LocalisationProjectFile = Path.Combine(
                                V2.TRANSLATION_PROJECT_BASE_PATH,
                                config.BuildSettings.Localisation.LocalisationFile ?? V1.DEFAULT_TRANSLATION_MANIFEST),
                            LocalisationProjectVersion = LocalisationProjectVersionType.v1
                        }
                    };
                return buildSettingsType;
            }

            private static eaw.build.data.config.mod.v2.CookSettingsType ExtractCookSettingsType(
                ModBuildConfigType config)
            {
                CookSettingsType cookSettingsType = new CookSettingsType
                {
                    OutputDirectory = V2.DEFAULT_COOK_DIRECTORY,
                    MoveDefinitions = new MoveDefinitionsType()
                    {
                        Directories = new eaw.build.data.config.mod.v2.DirectoryType[0], Files = new string[0]
                    }
                };
                List<data.config.mod.v2.BundleType> bundleTypes = new List<data.config.mod.v2.BundleType>();

                foreach (eaw.build.data.config.mod.v1.BundleType bundleType in config.BuildSettings.Bundle)
                {
                    string bundleName = ModProjectUtility.CheckAndUpdateBundleName(bundleType.Name);
                    data.config.mod.v2.BundleType bundleV2 = new data.config.mod.v2.BundleType
                    {
                        Name = bundleName, Content = new BundleContentType()
                    };

                    bundleV2.Content.Directories = (from directoryType in bundleType.Directory
                        let recursive = bool.Parse(directoryType.Recurse ?? "false")
                        let filePattern = directoryType.FilePattern ?? string.Empty
                        let directoryPath = directoryType.Value
                        select new data.config.mod.v2.DirectoryType
                        {
                            Recursive = recursive, DirectoryPath = directoryPath, FileSearchPattern = filePattern
                        }).ToArray();
                    bundleV2.Content.Files = bundleType.File.ToArray();
                    bundleTypes.Add(bundleV2);
                }

                cookSettingsType.BundleDefinitions = bundleTypes.ToArray();
                return cookSettingsType;
            }
        }

        internal static class V2
        {
            internal const string XSD = "v2.eaw-build.xsd";
            internal static readonly string TRANSLATION_PROJECT_BASE_PATH = Path.Combine("data", "text");
            internal const string DEFAULT_COOK_DIRECTORY = "cooked";
            internal const string DEFAULT_CONFIGURATION_FILE_NAME = "configuration.xml";
        }

        private ExitCode MigrateV2()
        {
            Log.Warning(
                $"No migration necessary. The mod project is already a {CURRENT_VERSION.ToString()} project.");
            return ExitCode.Success;
        }
    }
}
