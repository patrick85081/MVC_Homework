using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MVC_Homework.Models.ViewModels
{
    public class QueryOption : ICloneable
    {
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

        [JsonProperty("customerName")]
        public string CustomerName { get; set; }
    }

    /// <summary>
    /// JS API 回傳用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryOptionResult<T> : QueryOption
    {
        public QueryOptionResult(QueryOption query, IQueryable<T> datas)
        {
            this.Keyword = query.Keyword;
            this.Page = query.Page;
            this.SortField = query.SortField;
            this.SortOrder = query.SortOrder;
            this.Datas = datas.GetCurrentPage(query);
            this.PageCount = datas.GetPageCount(query);
        }

        [JsonProperty("datas")]
        public IEnumerable<T> Datas { get; set; }

        [JsonProperty("pageCount")]
        public int PageCount { get; set; }
    }

    public enum SortOrder
    {
        ASC,
        DESC
    }
}