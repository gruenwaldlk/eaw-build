using System;
using System.Collections.Generic;
using eaw.build.app.migration.mod;
using eaw.build.app.util.game;
using eaw.build.app.util.xml;
using eaw.build.data.config.mod.v1;
using eaw.build.data.config.mod.v2;

namespace eaw.build.app.util.mod
{
    internal static class ModProjectUtility
    {
        private static readonly HashSet<string> USED_BUNDLE_NAMES = new HashSet<string>();

        internal static void Reset()
        {
            USED_BUNDLE_NAMES.Clear();
        }

        internal static ModProjectVersion GetProjectVersionFromConfigFile(string filePath)
        {
            try
            {
                XmlUtility.ReadAndValidateXmlFile<ModBuildConfigType>(ModProjectMigrationUnit.V1.XSD, filePath);
                return ModProjectVersion.V1;
            }
#if DEBUG
            catch (Exception e)
            {
                Console.Write(e);
#else
            catch (Exception)
            {
                // Expected: Ignore.
#endif
            }

            try
            {
                XmlUtility.ReadAndValidateXmlFile<ModProjectType>(ModProjectMigrationUnit.V2.XSD, filePath);
                return ModProjectVersion.V2;
            }
#if DEBUG
            catch (Exception e)
            {
                Console.Write(e);
#else
            catch (Exception)
            {
                // Expected: Ignore.
#endif
            }

            return ModProjectVersion.Invalid;
        }

        internal static string CheckAndUpdateBundleName(string bundleName)
        {
            if (StarWarsGame.EmpireAtWar.RESERVED_BUNDLE_NAMES.Contains(bundleName))
            {
                string newName = bundleName + "_" + DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                USED_BUNDLE_NAMES.Add(newName);
                Console.WriteLine(
                    $"The bundle name \"{bundleName}\" is colliding with the base game bundle of the same name. The bundle will be renamed to \"{newName}\" to avoid conflicts.");
                return newName;
            }

            if (StarWarsGame.ForcesOfCorruption.RESERVED_BUNDLE_NAMES.Contains(bundleName))
            {
                string newName = bundleName + "_" + DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                USED_BUNDLE_NAMES.Add(newName);
                Console.WriteLine(
                    $"The bundle name \"{bundleName}\" is colliding with the game expansion bundle of the same name. The bundle will be renamed to \"{newName}\" to avoid conflicts.");
                return newName;
            }

            if (USED_BUNDLE_NAMES.Contains(bundleName))
            {
                string newName = bundleName + "_" + DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                USED_BUNDLE_NAMES.Add(newName);
                Console.WriteLine(
                    $"The bundle name \"{bundleName}\" is colliding with a previously defined bundle of the same name. The bundle will be renamed to \"{newName}\" to avoid conflicts.");
                return newName;
            }

            USED_BUNDLE_NAMES.Add(bundleName);
            return bundleName;
        }
    }
}