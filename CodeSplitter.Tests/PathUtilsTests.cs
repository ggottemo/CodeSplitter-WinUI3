using System.IO;
using CodeFileSplitter.Services;
using Xunit;

namespace CodeFileSplitter.Tests
{
    public class PathUtilsTests
    {
        [Fact]
        public void NormalizePath_WithForwardSlashes_ReturnsBackslashes()
        {
            // Arrange
            string path = "folder/subfolder/file.txt";
            
            // Act
            string result = PathUtils.NormalizePath(path);
            
            // Assert
            Assert.Equal(@"folder\subfolder\file.txt", result);
        }
        
        [Fact]
        public void NormalizePath_WithBackslashes_ReturnsSame()
        {
            // Arrange
            string path = @"folder\subfolder\file.txt";
            
            // Act
            string result = PathUtils.NormalizePath(path);
            
            // Assert
            Assert.Equal(@"folder\subfolder\file.txt", result);
        }
        
        [Fact]
        public void GetDirectoryPath_WithPath_ReturnsDirectoryOnly()
        {
            // Arrange
            string path = @"folder\subfolder\file.txt";
            
            // Act
            string result = PathUtils.GetDirectoryPath(path);
            
            // Assert
            Assert.Equal(@"folder\subfolder", result);
        }
        
        [Fact]
        public void GetDirectoryPath_WithNoDirectory_ReturnsEmpty()
        {
            // Arrange
            string path = "file.txt";
            
            // Act
            string result = PathUtils.GetDirectoryPath(path);
            
            // Assert
            Assert.Equal("", result);
        }
        
        [Fact]
        public void GetFileName_WithPath_ReturnsFileNameOnly()
        {
            // Arrange
            string path = @"folder\subfolder\file.txt";
            
            // Act
            string result = PathUtils.GetFileName(path);
            
            // Assert
            Assert.Equal("file.txt", result);
        }
        
        [Fact]
        public void GetFileName_WithNoPath_ReturnsFileName()
        {
            // Arrange
            string path = "file.txt";
            
            // Act
            string result = PathUtils.GetFileName(path);
            
            // Assert
            Assert.Equal("file.txt", result);
        }
    }
}