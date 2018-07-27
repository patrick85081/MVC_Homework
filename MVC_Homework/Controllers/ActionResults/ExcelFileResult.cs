using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;

namespace MVC_Homework.Controllers.ActionResults
{
    public class ExcelFileResult<TModel> : FileResult
    {
        public string FileName { get; set; }
        public IEnumerable<TModel> Models { get; set; } = Enumerable.Empty<TModel>();

        public ExcelFileResult() : base("application/vnd.ms-excel")
        {
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            WriteExcelStream(response.OutputStream);
            response.AppendHeader("Content-Disposition",
                $"attachment;filename={FileName ?? $"{nameof(TModel)}.xlsx"}");

        }

        private void WriteExcelStream(Stream stream)
        {
            var models = Models.ToList();
            var modelType = typeof(TModel);
            var metadataType = modelType.GetCustomAttribute<MetadataTypeAttribute>()?
                .MetadataClassType;
            var properties = GetOuputProperties<TModel>(modelType, metadataType);

            // Excel Init
            var wb = new XLWorkbook();
            var worksheet = wb.Worksheets.Add(modelType.Name);

            // Header
            foreach (var property in properties)
            {
                var columnIndex = properties.IndexOf(property) + 1;

                worksheet.Row(1)
                    .Cell(columnIndex)
                    .Value = GetDisplayName(property, metadataType);
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
            wb.SaveAs(stream);
            //MemoryStream ms = new MemoryStream();
            //wb.SaveAs(ms);
            //ms.Position = 0;

            //return ms;
        }

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


        /// <summary>
        /// 取得輸出的屬性清單
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelType"></param>
        /// <returns></returns>
        private static List<PropertyInfo> GetOuputProperties<T>(Type modelType, Type metadataType = null)
        {
            var properties = modelType.GetProperties()
                .Where(property => !IsExcelIgnore(property, metadataType) &&
                                   IsAllowType(property))
                .ToList();
            return properties;
        }

        private static string GetDisplayName(PropertyInfo property, Type metadataType = null)
        {
            var metadataProperty = metadataType?.GetProperty(property.Name);
            return metadataProperty?.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ??
                   metadataProperty?.GetCustomAttribute<DisplayAttribute>()?.Name ??
                   property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ??
                   property.GetCustomAttribute<DisplayAttribute>()?.Name ??
                   property.Name;
        }

        /// <summary>
        /// 是否忽略此屬性
        /// </summary>
        /// <param name="property"></param>
        /// <param name="metadataType"></param>
        /// <returns></returns>
        private static bool IsExcelIgnore(PropertyInfo property, Type metadataType = null)
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
}