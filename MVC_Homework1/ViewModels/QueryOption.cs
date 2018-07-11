using System;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MVC_Homework1.ViewModels
{
    public class QueryOption : ICloneable
    {
        private int pageCount = 0;

        [JsonProperty("keyword")]
        public string Keyword { get; set; } = "";

        [JsonProperty("page")]
        public int Page { get; set; } = 1;

        [JsonProperty("sortOrder")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SortOrder SortOrder { get; set; } = SortOrder.ASC;

        [JsonProperty("sortField")]
        public string SortField { get; set; } = "Id";


        public virtual int GetPageSize() => 10;

        public void SetPageCount(int count)
        {
            this.pageCount = count;
        }

        public int GetPageCount() =>
            this.pageCount;

        /// <summary>
        /// Dynamic Linq 用的Sort 參數
        /// </summary>
        /// <returns></returns>
        public string GetSortString() => $"{SortField} {SortOrder}";

        /// <summary>
        /// 產生分頁用的QueryOption
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public QueryOption ClonePageOption(int page)
        {
            var clone = this.Clone();
            clone.Page = page;
            return clone;
        }

        public QueryOption Clone() => 
            (this as ICloneable).Clone() as QueryOption;

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class 客戶資料QueryOption : QueryOption
    {
        [JsonProperty("category")]
        public string Category { get; set; } = "";
    }

    public class 客戶聯絡人QueryOption : QueryOption
    {
        [JsonProperty("job")]
        public string Job { get; set; }
    }

    /// <summary>
    /// JS API 回傳用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryOptionResult<T> : QueryOption
    {
        public QueryOptionResult(QueryOption query, T datas)
        {
            this.Keyword = query.Keyword;
            this.Page = query.Page;
            this.SortField = query.SortField;
            this.SortOrder = query.SortOrder;
            this.SetPageCount(query.GetPageCount());
            this.Datas = datas;
        }

        [JsonProperty("datas")]
        public T Datas { get; set; }

        [JsonProperty("pageCount")]
        public int PageCount => GetPageCount();
    }

    public enum SortOrder
    {
        ASC,
        DESC
    }
}