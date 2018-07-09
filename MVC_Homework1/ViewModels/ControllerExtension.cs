using ClosedXML.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVC_Homework1.ViewModels
{
    public static class ControllerExtension
    {
        public static ActionResult ExcelFile<T>(this Controller controller, IEnumerable<T> sources,string fileName = "report.xlsx")
        {
            // 允許輸出型別
            var allowTypes = new Type[]
            {
                typeof(string),
                typeof(int),
                typeof(short),
                typeof(float),
                typeof(long),
                typeof(bool),
                typeof(decimal),
                typeof(double),
                typeof(uint),
                typeof(ulong),
                typeof(ushort),
            };
            var models = sources.ToArray();
            var modelType = typeof(T);
            var propertyies = modelType.GetProperties()
                .Where(p => p.GetCustomAttribute(typeof(ExcelIgnoreAttribute)) == null &&
                            (allowTypes.Contains(p.PropertyType) ||
                             p.PropertyType.IsEnum))
                .ToArray();


            var wb = new XLWorkbook();
            var worksheet = wb.Worksheets.Add(modelType.Name);

            // Header
            for (int columnNumber = 0; columnNumber < propertyies.Length; columnNumber++)
            {
                worksheet.Row(1)
                    .Cell(columnNumber + 1)
                    .Value = propertyies[columnNumber].Name;
            }

            // Body
            for (var rowIndex = 0; rowIndex < models.Length; rowIndex ++)
            {
                var model = models[rowIndex];
                for (int columnIndex = 0; columnIndex < propertyies.Length; columnIndex++)
                {
                    worksheet.Row(rowIndex + 2)
                        .Cell(columnIndex + 1)
                        .Value = propertyies[columnIndex].GetValue(model);
                }
            }

            MemoryStream ms = new MemoryStream();
            // Debug file
            wb.SaveAs(controller.Server.MapPath($"~/App_Data/{modelType.Name}.xlsx"));
            wb.SaveAs(ms);
            ms.Position = 0;

            //return new FilePathResult(controller.Server.MapPath($"~/App_Data/{modelType.Name}.xlsx"), "application/octet-stream");
            return new FileStreamResult(ms, "application/vnd.ms-excel") {FileDownloadName = $"{modelType.Name}.xlsx"};
            //return new EmptyResult(); 
        }

        
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ExcelIgnoreAttribute : Attribute
    {

    }
}