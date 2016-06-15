using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using System.Reflection;
using System.Globalization;

namespace WebPortal.Models
{
    public static class Helper
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {
            UsersManager mgr = HttpContext.Current.GetOwinContext().GetUserManager<UsersManager>();
            return new MvcHtmlString(mgr.FindByIdAsync(id).Result.UserName); 
        }

        public static MvcHtmlString ClaimType(this HtmlHelper html, string claimType)
        {
            FieldInfo[] fields = typeof(ClaimTypes).GetFields();
            foreach (FieldInfo field in fields) {
                if (field.GetValue(null).ToString() == claimType)
                {
                    return new MvcHtmlString(field.Name);
                }
            }
                return new MvcHtmlString(string.Format("{0}",
                claimType.Split('/', '.').Last()
            ));
        }

        public static string Splitter(this string name, char delimiter, int order)
        {
            var split = name.Split(delimiter)[order];
            return split;
        }

        public static string CustomDateFormat(this string resultdate)
        {
            DateTime dt = DateTime.ParseExact(resultdate, "yyyyMMdd", CultureInfo.InvariantCulture);
            return dt.ToString("yyyy-MM-dd");
        }
    }
}