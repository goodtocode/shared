﻿using GoodToCode.Shared.Blob.Excel;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;

namespace GoodToCode.Shared.Blob.Tests
{
    [Binding]
    public class ExcelBlobReaderTests
    {
        private readonly ExcelBlobReader reader;
        private readonly string executingPath;
        private string AssetsFolder { get { return @$"{executingPath}/Assets"; } }

        private string SutXlsFile { get { return @$"{AssetsFolder}/TestFile.xls"; } }
        private string SutXlsxFile { get { return @$"{AssetsFolder}/TestFile.xlsx"; } }        

        public IWorkbook SutCsv { get; private set; }
        public IWorkbook SutXls { get; private set; }
        public IWorkbook SutXlsx { get; private set; }
        public Dictionary<string, StringValues> SutReturn { get; private set; }

        public ExcelBlobReaderTests()
        {
            reader = new ExcelBlobReader();
            // Visual Studio vs. dotnet test execute different folders
            executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);                        
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : Directory.GetCurrentDirectory();
            executingPath = Directory.Exists(AssetsFolder) ? executingPath : $"{Directory.GetParent(executingPath)}/bin/Debug/net5.0";
        }

        [Given(@"I have an XLSX file")]
        public void GivenIHaveAnXLSXFile()
        {
            Assert.IsTrue(File.Exists(SutXlsxFile), $"{SutXlsxFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
        }

        [When(@"read XLSX in via ExcelBlobReader")]
        public void WhenReadXLSXInViaExcelBlobReader()
        {
            SutXlsx = reader.ReadFile(SutXlsxFile);
            Assert.IsTrue(SutXlsx.GetSheetAt(0) != null);
        }

        [Then(@"all readable XLSX data is available to systems")]
        public void ThenAllReadableXLSXDataIsAvailableToSystems()
        {
            Assert.IsTrue(SutXlsx.NumberOfSheets > 0, $"SutXlsx.NumberOfSheets={SutXlsx.NumberOfSheets} > 0");
        }

        [Given(@"I have an XLS file")]
        public void GivenIHaveAnXLSFile()
        {
            Assert.IsTrue(File.Exists(SutXlsFile), $"{SutXlsFile} does not exist. Executing: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}");
        }

        [When(@"read XLS in via ExcelBlobReader")]
        public void WhenReadXLSInViaExcelBlobReader()
        {
            SutXls = reader.ReadFile(SutXlsFile);
            Assert.IsTrue(SutXls.GetSheetAt(0) != null);
        }

        [Then(@"all readable XLS data is available to systems")]
        public void ThenAllReadableXLSDataIsAvailableToSystems()
        {
            Assert.IsTrue(SutXls.NumberOfSheets > 0, $"SutXls.NumberOfSheets={SutXls.NumberOfSheets} > 0");
        }
    }
}

