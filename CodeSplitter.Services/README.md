# CodeSplitter.Services

This library contains the core services and models for the CodeSplitter application.

## Purpose

The services are separated into their own library to:

1. Facilitate better testability by separating business logic from UI
2. Support cross-platform development
3. Enable reuse of core functionality across different client applications

## Key Components

### Models

- `ParsedFile` - Represents a parsed file with a filename and content

### Services

- `CodeParser` - Parses code into separate files based on file headers
- `PathUtils` - Utilities for path handling with cross-platform compatibility

## Usage Example

```csharp
// Create code parser instance
var codeParser = new CodeParser();

// Parse code with file headers into individual files
string code = @"// file1.cs
    public class File1 {}

    // file2.cs
    public class File2 {}";

var parsedFiles = codeParser.ParseCodeIntoFiles(code);

// Process extracted files
foreach (var file in parsedFiles)
{
    // file.Filename contains the filename (e.g., "file1.cs")
    // file.Content contains the content with header
}

// Use path utilities for file path handling
string path = "subfolder/file.txt";
string normalizedPath = PathUtils.NormalizePath(path);      // "subfolder\file.txt"
string dirPath = PathUtils.GetDirectoryPath(path);          // "subfolder"
string fileName = PathUtils.GetFileName(path);              // "file.txt"
```