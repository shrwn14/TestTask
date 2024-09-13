using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TestTask.Application.Contracts;

namespace TestTask.Pages;

public class IndexModel : PageModel
{
    private readonly IExcelService _excelService;

    [BindProperty]
    public IFormFile Upload { get; set; }

    public IndexModel(IExcelService excelService)
    {
        _excelService = excelService;
    }

    public void OnGet()
    {

    }
    public async Task<IActionResult> OnPostFormUploadAsync()
    {
        if (Upload == null || Upload.Length == 0)
        {
            ModelState.AddModelError("Upload", "Please select a file to upload.");
            return Page(); 
        }

        var result = await _excelService.ReadExcelFileAsync(Upload);

         return new ContentResult() { Content = JsonConvert.SerializeObject(result), ContentType = "application/json" };

    }


}
