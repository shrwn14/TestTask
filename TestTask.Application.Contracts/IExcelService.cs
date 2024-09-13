using Microsoft.AspNetCore.Http;

namespace TestTask.Application.Contracts;

public interface IExcelService
{
    Task<ExcelFileDto> ReadExcelFileAsync(IFormFile file);
}
