using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
#if TEST
using System.IO.Abstractions.TestingHelpers;
#endif
using System.Linq;

namespace eaw.build.app.util
{
    internal static class PathUtility
    {
        internal static readonly IFileSystem FILE_SYSTEM;

        static PathUtility()
        {
#if TEST
            FILE_SYSTEM = new MockFileSystem();
#else
            FILE_SYSTEM = new FileSystem();
#endif
        }

        internal static string GetDirectoryPathFromFilePath(string filePath)
        {
            return string.IsNullOrWhiteSpace(filePath) ? string.Empty : FILE_SYSTEM.Path.GetDirectoryName(filePath);
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
                return FILE_SYSTEM.Path.GetFullPath(CleanPath(path));
            }

            //[Kad] -- FIXME: Path.GetFullPath(...) does not work correctly on Unix systems. Relative path resolving is currently disabled.
            return resolveRelativePaths ? FILE_SYSTEM.Path.GetFullPath(CleanPath(path)) : CleanPath(path);
        }

        internal static string Combine(string path, params string[] strings)
        {
            if (string.IsNullOrWhiteSpace(path) && (strings == null || strings.Length < 1))
            {
                return string.Empty;
            }

            string basePath = CleanPath(path);
            return strings.Where(s => !string.IsNullOrWhiteSpace(s))
                .Aggregate(basePath, (current, s) => FILE_SYSTEM.Path.Combine(current, CleanPath(s)));
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
                cleanedPath = FILE_SYSTEM.Path.Combine(splitPath);
            }

            if (cleanedPath.Contains('/'))
            {
                string[] splitPath = cleanedPath.Split('/');
                cleanedPath = FILE_SYSTEM.Path.Combine(splitPath);
            }

            return cleanedPath;
        }

        internal static string GetTempPath()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix ||
                Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                return Environment.GetEnvironmentVariable("HOME");
            }

            return FILE_SYSTEM.Path.GetTempPath();
        }

        internal static string GetTempFile(string tempFileName = "eawbld.tmp")
        {
            return Combine(GetTempPath(), tempFileName);
        }

        internal static void DeleteFile(string filePath)
        {
            if (FILE_SYSTEM.File.Exists(filePath))
            {
                FILE_SYSTEM.File.Delete(filePath);
            }
        }

        internal static bool FileExists(string filePath)
        {
            return FILE_SYSTEM.File.Exists(filePath);
        }

        internal static bool DirectoryExists(string directoryPath)
        {
            return FILE_SYSTEM.Directory.Exists(directoryPath);
        }

        internal static IEnumerable<string> GetFiles(string directoryPath, string filter = null)
        {
            if (string.IsNullOrEmpty(filter) || string.IsNullOrWhiteSpace(filter))
            {
                return new List<string>(FILE_SYSTEM.Directory.GetFiles(directoryPath));
            }

            return new List<string>(FILE_SYSTEM.Directory.GetFiles(directoryPath, filter));
        }

        internal static Stream FileOpenRead(string filePath)
        {
            return FILE_SYSTEM.File.OpenRead(filePath);
        }

        internal static StreamWriter GetStreamWriter(string filePath)
        {
#if TEST
            ((MockFileSystem) FILE_SYSTEM).AddFile(filePath, new MockFileData("\0"));
#endif
            return FILE_SYSTEM.File.CreateText(filePath);
        }

        internal static Stream FileCreate(string filePath)
        {
#if TEST
            ((MockFileSystem) FILE_SYSTEM).AddFile(filePath, new MockFileData("\0"));
#endif
            return FILE_SYSTEM.File.Create(filePath);
        }
    }
}
