using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace eaw.build.app.util
{
    internal static class FilePathUtility
    {
        internal static string GetDirectoryPathFromFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return string.Empty;
            }

            string path = ValidatePath(filePath);
            if (Environment.OSVersion.Platform != PlatformID.Unix &&
                Environment.OSVersion.Platform != PlatformID.MacOSX)
            {
                return Path.GetDirectoryName(path);
            }

            if (path.LastOrDefault().Equals(Path.DirectorySeparatorChar))
            {
                return path;
            }

            string[] pathSplit = path.Split(Path.DirectorySeparatorChar);
            string currPath = "";
            for (int i = 0; i < pathSplit.Length - 1; i++)
            {
                currPath = Combine(currPath, pathSplit[i]);
            }

            return currPath;
        }

        internal static string ValidatePath(string path, bool resolveRelativePaths = false)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return string.Empty;
            }

            if (Environment.OSVersion.Platform != PlatformID.Unix &&
                Environment.OSVersion.Platform != PlatformID.MacOSX)
            {
                return Path.GetFullPath(CleanPath(path));
            }

            //[Kad] -- FIXME: Path.GetFullPath(...) does not work correctly on Unix systems. Relative path resolving is currently disabled.
            return resolveRelativePaths ? Path.GetFullPath(CleanPath(path)) : CleanPath(path);
        }

        internal static string Combine(string path, params string[] strings)
        {
            if (string.IsNullOrWhiteSpace(path) && (strings == null || strings.Length < 1))
            {
                return string.Empty;
            }

            string basePath = CleanPath(path);
            return strings.Where(s => !string.IsNullOrWhiteSpace(s))
                .Aggregate(basePath, (current, s) => Path.Combine(current, CleanPath(s)));
        }

        internal static string CleanPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return string.Empty;
            }

            string cleanedPath = path;

            if (path.Contains('\\'))
            {
                string[] splitPath = path.Split('\\');
                cleanedPath = Path.Combine(splitPath);
            }

            if (cleanedPath.Contains('/'))
            {
                string[] splitPath = cleanedPath.Split('/');
                cleanedPath = Path.Combine(splitPath);
            }

            return cleanedPath;
        }

        internal static string GetTempPath()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix ||
                Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }

            return Path.GetTempPath();
        }

        internal static string GetTempFile(string tempFileName = "eawbld.tmp")
        {
            return Combine(GetTempPath(), tempFileName);
        }
    }
}
