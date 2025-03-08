using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using Windows.Storage;
using Windows.Storage.Pickers;
using CodeFileSplitter.Models;
using CodeFileSplitter.Services;

namespace CodeFileSplitter
{
    /// <summary>
    /// Main window for the Code File Splitter application.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private ObservableCollection<ParsedFile> ParsedFiles { get; } = new();
        private readonly CodeParser _codeParser = new();

        public MainWindow()
        {
            this.InitializeComponent();
         
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);

            // Set window appearance
            var titleBar = AppWindow.TitleBar;
                titleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;
                titleBar.ButtonInactiveBackgroundColor = Microsoft.UI.Colors.Transparent;
           
            // Fix for file pickers in WinUI 3 packaged apps
            InitializeWithWindow.Initialize(this);
        }

        private async void ParseButton_Click(object sender, RoutedEventArgs e)
        {
            string codeContent = CodeTextBox.Text;
            if (string.IsNullOrWhiteSpace(codeContent))
            {
                ContentDialog dialog = new()
                {
                    Title = "No Code",
                    Content = "Please paste some code first.",
                    CloseButtonText = "OK",
                    XamlRoot = Content.XamlRoot
                };

                await dialog.ShowAsync();
                return;
            }

            ParsedFiles.Clear();

            try
            {
                var files = _codeParser.ParseCodeIntoFiles(codeContent);
                if (files.Count == 0)
                {
                    ContentDialog dialog = new()
                    {
                        Title = "No Files Found",
                        Content = "No file headers found. Please ensure your code contains headers in the format '// filename.ext'",
                        CloseButtonText = "OK",
                        XamlRoot = Content.XamlRoot
                    };
                    await dialog.ShowAsync();
                    return;
                }

                foreach (var file in files)
                {
                    ParsedFiles.Add(file);
                }

                StatusTextBlock.Text = $"Parsed Files ({ParsedFiles.Count})";
            }
            catch (Exception ex)
            {
                ContentDialog dialog = new()
                {
                    Title = "Error",
                    Content = $"Error parsing code: {ex.Message}",
                    CloseButtonText = "OK",
                    XamlRoot = Content.XamlRoot
                };

                await dialog.ShowAsync();
            }
        }

        private async void DownloadAllButton_Click(object sender, RoutedEventArgs e)
        {
            if (ParsedFiles.Count == 0)
            {
                ContentDialog dialog = new()
                {
                    Title = "No Files",
                    Content = "No files to download. Please parse code first.",
                    CloseButtonText = "OK",
                    XamlRoot = Content.XamlRoot
                };

                await dialog.ShowAsync();
                return;
            }

            FolderPicker folderPicker = new()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                FileTypeFilter = { "*" }
            };
            IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);
            StorageFolder folder = await folderPicker.PickSingleFolderAsync();

            if (folder != null)
            {
                try
                {
                    foreach (var file in ParsedFiles)
                    {
                        // Handle path separators in filenames by creating directories
                        string filename = file.Filename;

                        if (filename.Contains('/') || filename.Contains('\\'))
                        {
                            // Extract the directory path and filename
                            string dirPath = PathUtils.GetDirectoryPath(filename);
                            string baseFilename = PathUtils.GetFileName(filename);

                            // Create directory structure if needed
                            StorageFolder targetFolder = folder;

                            if (!string.IsNullOrEmpty(dirPath))
                            {
                                // Create each directory in the path
                                string[] dirs = dirPath.Split('\\', StringSplitOptions.RemoveEmptyEntries);
                                foreach (string dir in dirs)
                                {
                                    targetFolder = await targetFolder.CreateFolderAsync(
                                        dir, CreationCollisionOption.OpenIfExists);
                                }
                            }

                            // Create file in the final directory
                            StorageFile storageFile = await targetFolder.CreateFileAsync(
                                baseFilename, CreationCollisionOption.ReplaceExisting);
                            await FileIO.WriteTextAsync(storageFile, file.Content);
                        }
                        else
                        {
                            // Simple filename without path separators
                            StorageFile storageFile = await folder.CreateFileAsync(
                                filename, CreationCollisionOption.ReplaceExisting);
                            await FileIO.WriteTextAsync(storageFile, file.Content);
                        }
                    }

                    ContentDialog dialog = new()
                    {
                        Title = "Files Saved",
                        Content = $"Successfully saved {ParsedFiles.Count} files to {folder.Path}",
                        CloseButtonText = "OK",
                        XamlRoot = Content.XamlRoot
                    };

                    await dialog.ShowAsync();
                }
                catch (Exception ex)
                {
                    ContentDialog dialog = new()
                    {
                        Title = "Error",
                        Content = $"Error saving files: {ex.Message}",
                        CloseButtonText = "OK",
                        XamlRoot = Content.XamlRoot
                    };

                    await dialog.ShowAsync();
                }
            }
        }

        private async void LoadFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                FileTypeFilter = { ".txt", ".cs", ".js", ".html", "*" }
            };

            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                try
                {
                    string content = await FileIO.ReadTextAsync(file);
                    CodeTextBox.Text = content;
                }
                catch (Exception ex)
                {
                    ContentDialog dialog = new()
                    {
                        Title = "Error",
                        Content = $"Error loading file: {ex.Message}",
                        CloseButtonText = "OK",
                        XamlRoot = Content.XamlRoot
                    };

                    await dialog.ShowAsync();
                }
            }
        }

        private async void ViewFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (ParsedFilesListView.SelectedItem is ParsedFile selectedFile)
            {
                ContentDialog fileViewDialog = new()
                {
                    Title = selectedFile.Filename,
                    Content = new ScrollViewer
                    {
                        Content = new TextBlock
                        {
                            Text = selectedFile.Content,
                            TextWrapping = TextWrapping.Wrap,
                            IsTextSelectionEnabled = true
                        },
                        VerticalScrollMode = ScrollMode.Auto,
                        HorizontalScrollMode = ScrollMode.Auto,
                        HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                        VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                        MinHeight = 400,
                        MinWidth = 600
                    },
                    CloseButtonText = "Close",
                    XamlRoot = Content.XamlRoot
                };

                await fileViewDialog.ShowAsync();
            }
        }
    }

    // Helper class to initialize file pickers in WinUI 3 desktop apps
    public static class InitializeWithWindow
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();

        public static void Initialize(Window window)
        {
            IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            WinRT.Interop.InitializeWithWindow.Initialize(new FileOpenPicker(), hwnd);
            WinRT.Interop.InitializeWithWindow.Initialize(new FileSavePicker(), hwnd);
            WinRT.Interop.InitializeWithWindow.Initialize(new FolderPicker(), hwnd);
        }
    }
}