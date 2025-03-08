# CodeSplitter Project Guide

## Project Structure

The solution consists of multiple projects:

- **CodeSplitter** - Main WinUI desktop application
- **CodeSplitter.Services** - Shared services and models (.NET Standard 2.0)
- **CodeSplitter.Tests** - xUnit tests for the services

## Build Commands

- `dotnet build CodeSplitter.sln` - Build the entire solution
- `dotnet build CodeSplitter.csproj` - Build the main application
- `dotnet build CodeSplitter.Services/CodeSplitter.Services.csproj` - Build the services library
- `dotnet build CodeSplitter.Tests/CodeSplitter.Tests.csproj` - Build the test project
- `dotnet build CodeSplitter.sln -c Release` - Build in Release configuration
- `dotnet publish CodeSplitter.sln -c Release -r win-x64 --self-contained` - Publish for x64
- `dotnet publish CodeSplitter.sln -c Release -r win-arm64 --self-contained` - Publish for ARM64

## Testing

- `dotnet test CodeSplitter.Tests/CodeSplitter.Tests.csproj` - Run all tests
- `dotnet test CodeSplitter.Tests/CodeSplitter.Tests.csproj --filter "FullyQualifiedName~CodeParser"` - Run tests by name pattern
- `dotnet test CodeSplitter.Tests/CodeSplitter.Tests.csproj --filter "DisplayName~FileHeaders"` - Run a specific test by name

## Code Style Guidelines

### Formatting & Structure
- Use 4-space indentation in C# code
- Use standard C# brace style with braces on new lines
- Place using statements at the top of files, organized alphabetically
- Prefer `var` for local variables when type is obvious

### Naming Conventions
- PascalCase for class names, public methods, and properties (e.g., `ParsedFile`, `ParseButton_Click`)
- camelCase for local variables and private fields
- Use descriptive, intention-revealing names

### Type Safety
- Enable nullable reference types (`Nullable` is enabled in project)
- Use explicit null checks before operations on potentially null values
- Prefer strongly-typed collections over generic collections when applicable

### Error Handling
- Use try/catch blocks when calling external APIs (file I/O, UI interactions)
- Display user-friendly error messages via ContentDialog
- Return appropriate error context when operations fail

### UI Components
- Follow WinUI 3/Fluent Design patterns and styling
- Group related UI elements in logical Grid or StackPanel containers
- Use XAML resource styles for consistent appearance