using System;
using System.IO;
using System.Reflection;

namespace NextGenSoftware.Utilities
{
    /// <summary>
    /// Shared app/file path helpers for CLI-style apps with install + user-writable fallback locations.
    /// </summary>
    public static class AppPathHelper
    {
        public static string ResolveAppRootDirectory()
        {
            // AppContext.BaseDirectory is the most reliable source for framework-dependent
            // apps — it returns the directory containing the app DLL, not the dotnet runtime.
            // Environment.ProcessPath returns the dotnet runtime binary path on Linux
            // (/usr/share/dotnet/dotnet) for framework-dependent apps, which is wrong.
            try
            {
                string baseDir = AppContext.BaseDirectory?.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                if (!string.IsNullOrEmpty(baseDir))
                    return Path.GetFullPath(baseDir);
            }
            catch { /* non-fatal */ }

            try
            {
                string proc = Environment.ProcessPath;
                string procName = Path.GetFileNameWithoutExtension(proc ?? "");
                // Skip if this is the dotnet runtime binary rather than the app itself
                if (!string.IsNullOrEmpty(proc) &&
                    !string.Equals(procName, "dotnet", StringComparison.OrdinalIgnoreCase))
                {
                    string dir = Path.GetDirectoryName(proc);
                    if (!string.IsNullOrEmpty(dir) && Directory.Exists(dir))
                        return Path.GetFullPath(dir);
                }
            }
            catch { /* non-fatal */ }

            try
            {
                string loc = Assembly.GetExecutingAssembly().Location;
                if (!string.IsNullOrEmpty(loc))
                {
                    string dir = Path.GetDirectoryName(loc);
                    if (!string.IsNullOrEmpty(dir) && Directory.Exists(dir))
                        return Path.GetFullPath(dir);
                }
            }
            catch { /* non-fatal */ }

            return Path.GetFullPath(Environment.CurrentDirectory);
        }

        public static string ResolvePathFromAppRoot(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return path;
            if (Path.IsPathRooted(path))
                return Path.GetFullPath(path);
            return Path.GetFullPath(Path.Combine(ResolveAppRootDirectory(), path.Replace('\\', Path.DirectorySeparatorChar)));
        }

        public static string ResolveBasePath(string appRoot, string configured)
        {
            if (string.IsNullOrWhiteSpace(configured))
                return appRoot;
            string t = configured.Trim();
            if (Path.IsPathRooted(t))
                return Path.GetFullPath(t);
            return Path.GetFullPath(Path.Combine(appRoot, t));
        }

        public static bool IsDirectoryWritable(string path)
        {
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path)) return false;
            try
            {
                string testFile = Path.Combine(path, ".write-test-" + Guid.NewGuid().ToString("N"));
                File.WriteAllText(testFile, "");
                File.Delete(testFile);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetUserDataRoot(string appFolderName = "oasis-star-cli")
        {
            string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (string.IsNullOrEmpty(local))
            {
                string profile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                if (string.IsNullOrEmpty(profile))
                    profile = Environment.GetEnvironmentVariable("HOME") ?? Environment.GetEnvironmentVariable("USERPROFILE") ?? "";

                if (OperatingSystem.IsWindows())
                    local = Path.Combine(profile, "AppData", "Local");
                else if (OperatingSystem.IsMacOS())
                    local = Path.Combine(profile, "Library", "Application Support");
                else
                    local = Path.Combine(profile, ".local", "share");
            }

            string root = Path.Combine(local, appFolderName);
            try
            {
                if (!Directory.Exists(root))
                    Directory.CreateDirectory(root);
            }
            catch { /* non-fatal */ }

            return Path.GetFullPath(root);
        }

        public static string GetUserDataSubDirectory(string appFolderName, string subDirectoryName)
        {
            string dir = Path.Combine(GetUserDataRoot(appFolderName), subDirectoryName);
            try { if (!Directory.Exists(dir)) Directory.CreateDirectory(dir); } catch { /* non-fatal */ }
            return Path.GetFullPath(dir);
        }
    }
}
