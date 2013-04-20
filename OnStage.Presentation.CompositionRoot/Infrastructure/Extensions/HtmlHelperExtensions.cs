using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Dynamic;
using System.Web.Routing;

namespace OnStage.Presentation.CompositionRoot.Infrastructure.Extensions
{
    public static class HtmlHelperExtensions
    {

        private const string ACTIVE_CLASS = "active";

        private static dynamic CloneToExpando(object value)
        {
            var result = new ExpandoObject();
            foreach (var prop in value.GetType().GetProperties())
            {
                ((IDictionary<string, object>)result).Add(prop.Name, prop.GetValue(value, null));
            }

            return result;
        }

        public static MvcHtmlString HeaderActionLinkLi(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            var link = ((string)htmlHelper.ViewContext.RouteData.Values["controller"] == controllerName && (string)htmlHelper.ViewContext.RouteData.Values["action"] == actionName) ? " class=\"" + ACTIVE_CLASS + "\"" : null;
            return new MvcHtmlString("<li" + (link != null ? link : "") + ">" + LinkExtensions.ActionLink(htmlHelper, linkText, actionName, controllerName) + "</li>");
        }

        public static AssetsHelper Assets(this HtmlHelper htmlHelper)
        {
            return AssetsHelper.GetInstance(htmlHelper);
        }

    }
}