using ClosedXML.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVC_Homework1.ViewModels
{
    public static class ControllerExtension
    {
        /// <summary>
        /// 允許輸出型別
        /// </summary>
        private static readonly Type[] allowTypes = new Type[]
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

        public static ActionResult ExcelFile<T>(this Controller controller, IEnumerable<T> sources,string fileName = "report.xlsx")
        {
            var models = sources.ToList();
            var modelType = typeof(T);
            var properties = GetOuputProperties<T>(modelType);

            // Excel Init
            var wb = new XLWorkbook();
            var worksheet = wb.Worksheets.Add(modelType.Name);

            // Header
            foreach (var property in properties)
            {
                var columnIndex = properties.IndexOf(property) + 1;

                worksheet.Row(1)
                    .Cell(columnIndex)
                    .Value = property.Name;
            }

            // Body
            foreach (var model in models)
            {
                int rowIndex = models.IndexOf(model) + 2;

                if (rowIndex < 2)
                    continue;

                foreach (var property in properties)
                {
                    int columnIndex = properties.IndexOf(property) + 1;

                    worksheet.Row(rowIndex)
                        .Cell(columnIndex)
                        .Value = property.GetValue(model);
                }
            }

            // Save to Stream.
            MemoryStream ms = new MemoryStream();
            wb.SaveAs(ms);
            ms.Position = 0;
            // Debug file
            //wb.SaveAs(controller.Server.MapPath($"~/App_Data/{modelType.Name}.xlsx"));

            //return new FilePathResult(controller.Server.MapPath($"~/App_Data/{modelType.Name}.xlsx"), "application/octet-stream");
            return new FileStreamResult(ms, "application/vnd.ms-excel") {FileDownloadName = $"{modelType.Name}.xlsx"};
        }

        /// <summary>
        /// 取得輸出的屬性清單
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelType"></param>
        /// <returns></returns>
        private static List<PropertyInfo> GetOuputProperties<T>(Type modelType)
        {
            var metadataType = modelType.GetCustomAttribute<MetadataTypeAttribute>()?
                .MetadataClassType;

            var properties = modelType.GetProperties()
                .Where(property => !IsExcelIgnore(property, metadataType) &&
                                   IsAllowType(property))
                .ToList();
            return properties;
        }

        /// <summary>
        /// 是否忽略此屬性
        /// </summary>
        /// <param name="property"></param>
        /// <param name="metadataType"></param>
        /// <returns></returns>
        private static bool IsExcelIgnore(PropertyInfo property, Type metadataType)
        {
            return property.GetCustomAttribute(typeof(ExcelIgnoreAttribute)) != null ||
                   metadataType?.GetProperty(property.Name)?.GetCustomAttribute<ExcelIgnoreAttribute>() != null;
        }

        /// <summary>
        /// 此屬性是否可以輸出
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private static bool IsAllowType(PropertyInfo property)
        {
            return (allowTypes.Contains(property.PropertyType) || property.PropertyType.IsEnum);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ExcelIgnoreAttribute : Attribute
    {

    }
}