using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MVC_Homework.Models.ViewModels;

namespace MVC_Homework.Utils
{
    public static class HtmlHelperExtension
    {
        #region Sort Link

        /// <summary>
        /// 建立排序標題
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="propertyExpression"></param>
        /// <param name="actionName">Action方法</param>
        /// <param name="query">查詢參數</param>
        /// <returns></returns>
        public static MvcHtmlString BuildSortLink<TModel, TProperty>(this HtmlHelper<IEnumerable<TModel>> helper,
            Expression<Func<TModel, TProperty>> propertyExpression, string actionName, QueryOption query)
        {
            var metaData = ModelMetadata.FromLambdaExpression<TModel, TProperty>(propertyExpression, new ViewDataDictionary<TModel>());
            //var tt = ExpressionHelper.GetExpressionText((LambdaExpression)propertyExpression);

            string propertyName = string.IsNullOrEmpty(metaData.PropertyName) ? propertyExpression.GetFieldName() : metaData.PropertyName;
            var displayName = string.IsNullOrEmpty(metaData.DisplayName) ? metaData.PropertyName : metaData.DisplayName;

            return
                helper.BuildSortLink(actionName, query, propertyName, displayName);
        }

        /// <summary>
        /// 建立排序標題
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="propertyExpression"></param>
        /// <param name="actionName">Action方法</param>
        /// <param name="query">查詢參數</param>
        /// <returns></returns>
        public static MvcHtmlString BuildSortLink<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> propertyExpression, string actionName, QueryOption query)
        {
            var metaData = ModelMetadata.FromLambdaExpression<TModel, TProperty>(propertyExpression, helper.ViewData);

            string propertyName = string.IsNullOrEmpty(metaData.PropertyName) ? propertyExpression.GetFieldName() : metaData.PropertyName;
            var displayName = string.IsNullOrEmpty(metaData.DisplayName) ? metaData.PropertyName : metaData.DisplayName;

            return
                helper.BuildSortLink(actionName, query, propertyName, displayName);
        }

        /// <summary>
        /// 建立排序標題
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="actionName">Action方法</param>
        /// <param name="query">查詢參數</param>
        /// <param name="propertyName">屬性名稱</param>
        /// <param name="displayName">顯示名稱</param>
        /// <returns></returns>
        public static MvcHtmlString BuildSortLink(this HtmlHelper helper,
            string actionName, QueryOption query, string propertyName, string displayName)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var isCurrentField = propertyName == query.SortField;

            StringBuilder classBuilder = new StringBuilder("glyphicon glyphicon-sort");
            if (isCurrentField)
            {
                classBuilder.Append("-by-alphabet");
                if (query.SortOrder == SortOrder.DESC)
                {
                    classBuilder.Append("-alt");
                }
            }

            var outputQuery = query.Clone();
            outputQuery.SortField = propertyName;
            outputQuery.SortOrder = query.SortOrder == SortOrder.ASC ? SortOrder.DESC : SortOrder.ASC;

            return new MvcHtmlString(
                $"<a href=\"{urlHelper.Action(actionName, outputQuery)}\">" +
                $"{displayName} <span class=\"{classBuilder}\"></span>" +
                "</a>");
        }

        private static string GetFieldName<TModel, TProperty>(this Expression<Func<TModel, TProperty>> propertyExpression)
        {
            if (propertyExpression.Body.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression body = (MemberExpression)propertyExpression.Body;
                return (body.Member as PropertyInfo) != null ? body.Member.Name : (string)null;
            }

            return null;
        }

        #endregion Sort Link

        #region Menu Link

        public static MvcHtmlString MenuActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName,
            object routeValues = null, Match match = Match.ControllerAndAction)
        {
            bool isActive = match > 0;

            if (match.HasFlag(Match.Action))
                isActive = htmlHelper.GetCurrentAction() == actionName && isActive;
            if (match.HasFlag(Match.Controller))
                isActive = htmlHelper.GetCurrentController() == controllerName && isActive;
            StringBuilder sb = new StringBuilder();
            sb.Append($"<li{(isActive ? " class=\"active\"" : "")}>");
            sb.Append(htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, null));
            sb.Append("</li>");

            return new MvcHtmlString(sb.ToString());
        }

        #endregion

        public static string GetCurrentController(this HtmlHelper htmlHelper) =>
            htmlHelper.ViewContext.RouteData.Values["controller"].ToString();

        public static string GetCurrentAction(this HtmlHelper htmlHelper) =>
            htmlHelper.ViewContext.RouteData.Values["action"].ToString();
    }

    [Flags]
    public enum Match
    {
        None = 0,
        Controller = 1,
        Action = 2,
        ControllerAndAction = Controller | Action,
    }
}