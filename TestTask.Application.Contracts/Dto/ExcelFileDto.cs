using System.Data;

namespace TestTask.Application.Contracts;

public class ExcelFileDto
{
    public List<string> Headers { get; set; } = [];
    public DataTable Data { get; set; } = new DataTable();
    public int DataCount => Data.Rows.Count;
}
