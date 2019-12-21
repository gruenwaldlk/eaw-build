using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Xml.Serialization;
using eaw.build.app.util;
using eaw.build.data.config.v1;
using eaw.build.data.config.v2;
using BuildSettingsType = eaw.build.data.config.v1.BuildSettingsType;
using BundleType = eaw.build.data.config.v2.BundleType;

namespace eaw.build.app.migration.mod
{
    internal class ModProjectMigrator : IMigrator
    {
        private const ModProjectVersion CURRENT_VERSION = ModProjectVersion.V2;
        private const string MISSING = "MISSING";
        private const string MISSING_VERSION = "0.1.0";

        internal string OldFilePath { get; }
        internal string NewFilePath { get; }

        internal ModProjectMigrator(string oldFilePath, string newFilePath)
        {
            OldFilePath = oldFilePath;
            NewFilePath = newFilePath;
        }

        public ExitCode Migrate()
        {
            ModProjectVersion oldProjectVersion = GetProjectVersion();
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
            try
            {
                using (FileStream stream = File.OpenRead(OldFilePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ModBuildConfigType));
                    ModBuildConfigType config = (ModBuildConfigType) serializer.Deserialize(stream);
                    if (config == null)
                    {
                        return ExitCode.Error;
                    }

                    data.config.v2.ModInfoType modInfo = V1.ExtractModInfoType(config);
                    data.config.v2.BuildSettingsType buildSettingsType = new data.config.v2.BuildSettingsType();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }

            return ExitCode.Error;
        }

        private static class V1
        {
            internal static data.config.v2.ModInfoType ExtractModInfoType(ModBuildConfigType config)
            {
                return new data.config.v2.ModInfoType
                {
                    Name = ExtractModNameType(config),
                    Version = ExtractModVersionType(config),
                    Description = ExtractModDescriptionType(config),
                    IconPath = MISSING
                };
            }

            private static data.config.v2.ModNameType ExtractModNameType(ModBuildConfigType config)
            {
                string modLongName = config.ModSettings.Name ?? MISSING;
                string modShortName = config.ModSettings.ShortName ??
                                      modLongName.Replace(" ", string.Empty);
                return new data.config.v2.ModNameType {LongName = modLongName, ShortName = modShortName};
            }

            private static data.config.v2.ModVersionType ExtractModVersionType(ModBuildConfigType config)
            {
                string modVersion = config.ModSettings.Version ?? MISSING_VERSION;
                string[] modVersionSplit = modVersion.Split('.');
                int majorVersion = int.Parse(modVersionSplit[0] ?? "0");
                int minorVersion = int.Parse(modVersionSplit[1] ?? "1");
                int patchVersion = int.Parse(modVersionSplit[2] ?? "0");
                return
                    new data.config.v2.ModVersionType
                    {
                        Major = majorVersion, Minor = minorVersion, Patch = patchVersion, Build = 0
                    };
            }

            private static data.config.v2.ModDescriptionType ExtractModDescriptionType(ModBuildConfigType config)
            {
                return new data.config.v2.ModDescriptionType {Description = MISSING, Summary = MISSING};
            }

            private static data.config.v2.BuildSettingsType ExtractBuildSettingsType(ModBuildConfigType config)
            {
                foreach (data.config.v1.BundleType bundleType in config.BuildSettings.Bundle)
                {
                    data.config.v1.DirectoryType[] directories = bundleType.Directory;
                    data.config.v2.BundleType bundle = new data.config.v2.BundleType();
                    bundle.Name = bundleType.Name;
                    bundle.Content = new BundleContentType();
                    List<data.config.v2.DirectoryType> directoriesV2 = new List<data.config.v2.DirectoryType>();
                    foreach (data.config.v1.DirectoryType directoryType in bundleType.Directory)
                    {
                        bool recursive = bool.Parse(directoryType.Recurse ?? "false");
                        string filePattern = directoryType.FilePattern ?? string.Empty;
                        string directoryPath = directoryType.Value;
                        data.config.v2.DirectoryType directory =
                            new data.config.v2.DirectoryType
                            {
                                Recursive = recursive,
                                DirectoryPath = directoryPath,
                                FileSearchPattern = filePattern
                            };
                        directoriesV2.Add(directory);
                    }
                    bundle.Content.Directories = directoriesV2.ToArray();
                }
                data.config.v2.BuildSettingsType buildSettingsType = new data.config.v2.BuildSettingsType();
                buildSettingsType.CookSettings = new CookSettingsType();
            }
        }

        private ExitCode MigrateV2()
        {
            Console.Error.WriteLine(
                $"No migration necessary. The mod project is already a {CURRENT_VERSION.ToString()} project.");
            return ExitCode.Success;
        }

        private ModProjectVersion GetProjectVersion()
        {
            try
            {
                using (FileStream stream = File.OpenRead(OldFilePath))
                {
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ModBuildConfigType));
                        ModBuildConfigType config = (ModBuildConfigType) serializer.Deserialize(stream);
                        if (config != null)
                        {
                            return ModProjectVersion.V1;
                        }
                    }
                    catch (Exception)
                    {
                        // No exception necessary. It's expected.
                    }

                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ModProjectType));
                        ModProjectType config = (ModProjectType) serializer.Deserialize(stream);
                        if (config != null)
                        {
                            return ModProjectVersion.V2;
                        }
                    }
                    catch (Exception)
                    {
                        // No exception necessary. It's expected.
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }

            return ModProjectVersion.Invalid;
        }
    }
}
