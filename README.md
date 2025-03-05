# Code File Splitter

A modern Windows desktop application that helps split code containing multiple file headers into separate files.

## Features

- **Split Code Files**: Split code with file headers (// filename.ext) into individual files
- **Modern UI**: Built with WinUI 3 and Fluent Design for a native Windows 10/11 experience
- **Directory Structure Support**: Automatically creates appropriate folder structure for files with path separators
- **File Preview**: View individual parsed files before saving them


## Usage

1. Paste code containing file headers (e.g., `// filename.ext`) into the text area
2. Click "Parse Files" to extract individual files
3. Review the parsed files in the list
4. Click "Download All Files" to save all files to a selected folder

### Example Input Format

```csharp
// ViewTransitions.Blazor/BlazorViewTransitionService.cs
using Microsoft.JSInterop;
using System.Text.Json;
using ViewTransitions.Core;

namespace ViewTransitions.Blazor;

/// <summary>
/// Blazor implementation of the view transition service.
/// </summary>
public class BlazorViewTransitionService : IViewTransitionService, IAsyncDisposable
{
    // Class implementation...
}

// ViewTransitions.Blazor/TransitionView.cs
using Microsoft.AspNetCore.Components;

namespace ViewTransitions.Blazor;

/// <summary>
/// Component for managing transition state across components.
/// </summary>
public partial class TransitionView : ComponentBase, IAsyncDisposable
{
    // Component implementation...
}
```

## Installation

### Option 1: Install from GitHub Releases
1. Download the latest MSI installer from the [Releases page](https://github.com/ggottemo/CodeSplitter-WinUI3/releases)
2. Run the installer and follow the prompts

### Option 2: Build from Source
1. Clone this repository
2. Open the solution in Visual Studio 2022
3. Build and run the application

## Requirements

- Windows 10 version 1809 or later (Windows 11 recommended for best experience)
- [Windows App SDK](https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/downloads) 1.4 or later

## Development

### Prerequisites
- Visual Studio 2022 with the following workloads:
  - .NET Desktop Development
  - Universal Windows Platform development
  - Windows App SDK C# Templates
- Windows App SDK 1.4+

### Project Structure
- `MainWindow.xaml/xaml.cs`: Main application UI and logic
- `App.xaml/xaml.cs`: Application entry point

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the Unlicense - see the LICENSE file for details.

## Acknowledgments

- Built with [Windows App SDK](https://github.com/microsoft/WindowsAppSDK)
- Uses WinUI 3 for modern UI components
