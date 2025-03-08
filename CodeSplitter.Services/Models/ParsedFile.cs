namespace CodeFileSplitter.Models
{
    /// <summary>
    /// Represents a file that has been parsed from code with a filename and content.
    /// </summary>
    public class ParsedFile
    {
        /// <summary>
        /// Gets or sets the filename, including any path information.
        /// </summary>
        public string Filename { get; set; } = "";

        /// <summary>
        /// Gets or sets the content of the file.
        /// </summary>
        public string Content { get; set; } = "";
    }
}