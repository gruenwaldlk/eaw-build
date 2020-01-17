using System;
using System.Collections.Generic;
using eaw.build.app.migration.mod;
using eaw.build.app.util.game;
using eaw.build.app.util.xml;
using eaw.build.data.config.mod.v1;
using eaw.build.data.config.mod.v2;
using Serilog;

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
            catch (Exception exception)
            {
                Log.Error("An exception occurred {@Exception}", exception);
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
            catch (Exception exception)
            {
                Log.Error("An exception occurred {@Exception}", exception);
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
                Log.Warning(
                    "The bundle name \"{BundleName}\" is colliding with the base game bundle of the same name. The bundle will be renamed to \"{NewName}\" to avoid conflicts.", bundleName, newName);
                return newName;
            }

            if (StarWarsGame.ForcesOfCorruption.RESERVED_BUNDLE_NAMES.Contains(bundleName))
            {
                string newName = bundleName + "_" + DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                USED_BUNDLE_NAMES.Add(newName);
                Log.Warning(
                    "The bundle name \"{BundleName}\" is colliding with the game expansion bundle of the same name. The bundle will be renamed to \"{NewName}\" to avoid conflicts.", bundleName, newName);
                return newName;
            }

            if (USED_BUNDLE_NAMES.Contains(bundleName))
            {
                string newName = bundleName + "_" + DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                USED_BUNDLE_NAMES.Add(newName);
                Log.Warning(
                    "The bundle name \"{BundleName}\" is colliding with a previously defined bundle of the same name. The bundle will be renamed to \"{NewName}\" to avoid conflicts.", bundleName, newName);
                return newName;
            }

            USED_BUNDLE_NAMES.Add(bundleName);
            return bundleName;
        }
    }
}
