using System.Linq;
using CodeFileSplitter.Services;
using Xunit;

namespace CodeFileSplitter.Tests
{
    public class CodeParserTests
    {
        private readonly CodeParser _codeParser;

        public CodeParserTests()
        {
            _codeParser = new CodeParser();
        }

        [Fact]
        public void ParseCodeIntoFiles_EmptyInput_ReturnsEmptyList()
        {
            // Arrange
            string emptyCode = "";

            // Act
            var result = _codeParser.ParseCodeIntoFiles(emptyCode);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void ParseCodeIntoFiles_NullInput_ReturnsEmptyList()
        {
            // Arrange
            string? nullCode = null;

            // Act
            var result = _codeParser.ParseCodeIntoFiles(nullCode);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void ParseCodeIntoFiles_NoFileHeaders_ReturnsEmptyList()
        {
            // Arrange
            string codeWithoutHeaders = @"
                using System;
                
                class Program 
                {
                    static void Main() 
                    {
                        Console.WriteLine(""Hello, World!"");
                    }
                }";

            // Act
            var result = _codeParser.ParseCodeIntoFiles(codeWithoutHeaders);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void ParseCodeIntoFiles_SingleFile_ReturnsSingleFile()
        {
            // Arrange
            string singleFileCode = @"// test.cs
                using System;
                
                class Program 
                {
                    static void Main() 
                    {
                        Console.WriteLine(""Hello, World!"");
                    }
                }";

            // Act
            var result = _codeParser.ParseCodeIntoFiles(singleFileCode);

            // Assert
            Assert.Single(result);
            Assert.Equal("test.cs", result[0].Filename);
            Assert.Contains("class Program", result[0].Content);
        }

        [Fact]
        public void ParseCodeIntoFiles_MultipleFiles_ReturnsAllFiles()
        {
            // Arrange
            string multipleFilesCode = @"// file1.cs
                using System;
                
                namespace Test1 
                {
                    public class Class1 {}
                }

                // file2.cs
                using System;
                
                namespace Test2 
                {
                    public class Class2 {}
                }

                // subfolder/file3.cs
                using System;
                
                namespace Test3 
                {
                    public class Class3 {}
                }";

            // Act
            var result = _codeParser.ParseCodeIntoFiles(multipleFilesCode);

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Contains(result, f => f.Filename == "file1.cs");
            Assert.Contains(result, f => f.Filename == "file2.cs");
            Assert.Contains(result, f => f.Filename == "subfolder/file3.cs");
            
            var file1 = result.First(f => f.Filename == "file1.cs");
            Assert.Contains("namespace Test1", file1.Content);
            
            var file2 = result.First(f => f.Filename == "file2.cs");
            Assert.Contains("namespace Test2", file2.Content);
            
            var file3 = result.First(f => f.Filename == "subfolder/file3.cs");
            Assert.Contains("namespace Test3", file3.Content);
        }

        [Fact]
        public void ParseCodeIntoFiles_SpecialCharactersInFilename_ParsesCorrectly()
        {
            // Arrange
            string codeWithSpecialChars = @"// @special-file_name.with.dots.js
                function test() {
                    return 'test';
                }";

            // Act
            var result = _codeParser.ParseCodeIntoFiles(codeWithSpecialChars);

            // Assert
            Assert.Single(result);
            Assert.Equal("@special-file_name.with.dots.js", result[0].Filename);
        }
    }
}