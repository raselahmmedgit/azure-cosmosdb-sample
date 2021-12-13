using CsvHelper;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using lab.LocalCosmosDbApp.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using TinyCsvParser;

namespace lab.LocalCosmosDbApp.Helpers
{
    public static class ImportHelper
    {
        private static string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = string.Empty;
            if (cell.CellValue != null)
            {
                value = cell.CellValue.InnerText;
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
                }
            }
            return value;
        }

        public static IEnumerable<BusinessUnitToolInfoCsvModel> ImportBusinessUnitToolInfoFromCSVFile(IFormFile uploadFile)
        {
            try
            {
                using (var reader = new StreamReader(uploadFile.OpenReadStream()))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var businessUnitToolInfoCsvModelList = csv.GetRecords<BusinessUnitToolInfoCsvModel>();

                        return businessUnitToolInfoCsvModelList.AsEnumerable();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<BusinessUnitToolInfoViewModel> ImportBusinessUnitToolInfoFromCSVFileNew(IFormFile uploadFile)
        {
            List<BusinessUnitToolInfoViewModel> businessUnitToolInfos = new List<BusinessUnitToolInfoViewModel>();
            try
            {
                CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
                var csvParser = new CsvParser<BusinessUnitToolInfoViewModel>(csvParserOptions, new BusinessUnitToolInfoTinyCsvParserMap());
                var records = csvParser.ReadFromStream(uploadFile.OpenReadStream(), Encoding.UTF8);
                businessUnitToolInfos = records.Select(x => x.Result).ToList();
                return businessUnitToolInfos;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
