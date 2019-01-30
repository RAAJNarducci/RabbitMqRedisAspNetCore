using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace ApiTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        [Route("Excel")]
        public IActionResult Excel()
        {

            var comlumHeadrs = new string[]
            {
                "Employee Id",
                "Name",
                "Position",
                "Salary",
                "Joined Date"
            };

            byte[] result;

            using (var package = new ExcelPackage())
            {
                // add a new worksheet to the empty workbook

                var worksheet = package.Workbook.Worksheets.Add("Current Employee"); //Worksheet name
                using (var cells = worksheet.Cells[1, 1, 1, 5]) //(1,1) (1,5)
                {
                    cells.Style.Font.Bold = true;
                }

                //First add the headers
                for (var i = 0; i < comlumHeadrs.Count(); i++)
                {
                    worksheet.Cells[1, i + 1].Value = comlumHeadrs[i];
                }

                //Add values
                var j = 2;
                foreach (var employee in DummyData.GetEmployeeData())
                {
                    worksheet.Cells["A" + j].Value = employee.Id;
                    worksheet.Cells["B" + j].Value = employee.Name;
                    worksheet.Cells["C" + j].Value = employee.Position;
                    worksheet.Cells["D" + j].Value = employee.Salary.ToString("$#,0.00;($#,0.00)");
                    worksheet.Cells["E" + j].Value = employee.JoinedDate.ToString("MM/dd/yyyy");

                    j++;
                }
                result = package.GetAsByteArray();
            }

            return File(result, "application/ms-excel", $"Employee.xlsx");
        }

        [Route("CSV")]
        public IActionResult CSV()
        {
            var comlumHeadrs = new string[]
            {
                "Employee Id",
                "Name",
                "Position",
                "Salary",
                "Joined Date"
            };

            var employeeRecords = (from employee in DummyData.GetEmployeeData()
                                   select new object[]
                                   {
                                            employee.Id,
                                            $"{employee.Name}",
                                            $"\"{employee.Position}\"", //Escaping ","
                                            $"\"{employee.Salary.ToString("$#,0.00;($#,0.00)")}\"", //Escaping ","
                                            employee.JoinedDate.ToString("MM/dd/yyyy"),
                                   }).ToList();

            // Build the file content
            var employeecsv = new StringBuilder();
            employeeRecords.ForEach(line =>
            {
                employeecsv.AppendLine(string.Join(",", line));
            });

            byte[] buffer = Encoding.ASCII.GetBytes($"{string.Join(",", comlumHeadrs)}\r\n{employeecsv.ToString()}");
            return File(buffer, "text/csv", $"Employee.csv");

        }
    }
}