using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace WebApplication_Filestream.Controllers
{
    [ApiController]
    [Route("api/excel")]
    public class ExcelController : ControllerBase
    {
        [HttpGet("download")]
        public IActionResult DownloadExcel()
        {
            ExcelPackage.License.SetNonCommercialPersonal("Carlos"); 

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Datos");
                worksheet.Cells[1, 1].Value = "ID";  
                worksheet.Cells[1, 2].Value = "Nombre";
                worksheet.Cells[2, 1].Value = 1;
                worksheet.Cells[2, 2].Value = "Ejemplo";

                // We can add more data here if needed 

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                return File(
                    stream,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "ejemplo_streaming.xlsx"
                );
            }
        }
    }
}
