using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Data;
using System.Text.RegularExpressions;
using TestTask.Application.Contracts;

namespace TestTask.Application;

public class ExcelService : IExcelService
{
    public async Task<ExcelFileDto> ReadExcelFileAsync(IFormFile file)
    {
        var result = new ExcelFileDto();
        var dt = new DataTable();

        if (file == null || file.Length == 0)
            return result;

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(stream))
            {

                var worksheet = package.Workbook.Worksheets.First();
                var rowCount = worksheet.Dimension.Rows;
                var colCount = worksheet.Dimension.Columns;

                // get headers
                for (int col = 1; col <= colCount; col++)
                {
                    string header = Regex.Replace(Regex.Replace(worksheet.Cells[1, col].Text.Trim(), @"(\r\n|\r|\n)", " "), @"\s+", " "); 
                    dt.Columns.Add(header);
                    result.Headers.Add(header);
                }

                // Add rows to DataTable
                for (int row = 2; row <= rowCount; row++)
                {
                    var dataRow = dt.NewRow();
                    for (int col = 1; col <= colCount; col++)
                    {
                        dataRow[col - 1] = worksheet.Cells[row, col].Text;
                    }
                    dt.Rows.Add(dataRow);
                }
            }
        }

        result.Data = dt;
        return result;
    }
}
