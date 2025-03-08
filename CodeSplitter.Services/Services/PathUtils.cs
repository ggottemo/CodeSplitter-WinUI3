using System;
using System.IO;

namespace CodeFileSplitter.Services
{
    /// <summary>
    /// Utility methods for path handling.
    /// </summary>
    public static class PathUtils
    {
        /// <summary>
        /// Normalizes a path by converting forward slashes to backslashes for Windows compatibility.
        /// </summary>
        /// <param name="path">The path to normalize.</param>
        /// <returns>The normalized path.</returns>
        public static string NormalizePath(string path)
        {
            return path.Replace('/', '\\');
        }

        /// <summary>
        /// Gets the directory path from a full file path.
        /// </summary>
        /// <param name="path">The full file path.</param>
        /// <returns>The directory path component, or empty string if no directory.</returns>
        public static string GetDirectoryPath(string path)
        {
            // For testing purposes, we'll just handle the path manually
            string normalizedPath = NormalizePath(path);
            int lastIndex = normalizedPath.LastIndexOf('\\');
            if (lastIndex > 0)
            {
                return normalizedPath.Substring(0, lastIndex);
            }
            return "";
        }

        /// <summary>
        /// Gets the filename from a full file path.
        /// </summary>
        /// <param name="path">The full file path.</param>
        /// <returns>The filename component.</returns>
        public static string GetFileName(string path)
        {
            // For testing purposes, we'll just handle the path manually
            string normalizedPath = NormalizePath(path);
            int lastIndex = normalizedPath.LastIndexOf('\\');
            if (lastIndex >= 0 && lastIndex < normalizedPath.Length - 1)
            {
                return normalizedPath.Substring(lastIndex + 1);
            }
            return normalizedPath;
        }
    }
}