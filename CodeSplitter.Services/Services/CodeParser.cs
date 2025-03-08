using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CodeFileSplitter.Models;

namespace CodeFileSplitter.Services
{
    /// <summary>
    /// Service to parse code into separate files based on file headers.
    /// </summary>
    public class CodeParser
    {
        /// <summary>
        /// Parses a string of code into multiple files based on file headers (// filename.ext).
        /// </summary>
        /// <param name="code">The full code content to parse.</param>
        /// <returns>A list of ParsedFile objects.</returns>
        public List<ParsedFile> ParseCodeIntoFiles(string? code)
        {
            var files = new List<ParsedFile>();
            
            if (string.IsNullOrEmpty(code))
            {
                return files;
            }

            // Simple regex to match file headers like "// filename.ext"
            var regex = new Regex(@"//\s+([^\s]+\.[^\s]+)");
            var matches = regex.Matches(code);

            if (matches.Count == 0)
            {
                return files;
            }

            for (int i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                // Ensure we have the required groups before accessing
                if (match.Groups.Count < 2)
                {
                    continue;
                }
                
                string filename = match.Groups[1].Value;

                int startPos = match.Index;
                // Since we've already checked that code is not null above
                int endPos = (i < matches.Count - 1) ? matches[i + 1].Index : code!.Length;

                // We've already checked that code is not null above
                string fileContent = code!.Substring(startPos, endPos - startPos).Trim();
                files.Add(new ParsedFile { Filename = filename, Content = fileContent });
            }

            return files;
        }
    }
}