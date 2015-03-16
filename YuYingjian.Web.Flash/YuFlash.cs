using System;
using System.Linq;
using System.Text;
using System.Web;

namespace YuYingjian.Web.Flash
{
    /// <summary>
    /// 使用方法：
    /// 在逻辑代码中
    /// YuFlash.Flash("success", "操作成功");
    /// 在页面代码中
    /// @if(YuFlash.HasFlash("success")){
    ///     <p class="success">@YuFlash.Flash("success")</p>
    /// }
    /// </summary>
    public static class YuFlash
    {
        public static void Flash(string key, string message)
        {
            HttpCookie cookie = new HttpCookie(string.Format("yyj_flash_{0}", key));
            cookie.Value = HttpUtility.UrlEncode(message, Encoding.UTF8);
            cookie.Expires = DateTime.Now.AddMinutes(6);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string Flash(string key)
        {
            if (HasFlash(key))
            {
                var cookie = HttpContext.Current.Request.Cookies[string.Format("yyj_flash_{0}", key)];
                var message = HttpUtility.UrlDecode(cookie.Value, Encoding.UTF8);
                cookie.Expires = DateTime.Now.AddHours(-6);
                HttpContext.Current.Response.Cookies.Add(cookie);
                return message;
            }
            return string.Empty;
        }

        public static bool HasFlash(string key)
        {
            return HttpContext.Current.Request.Cookies.AllKeys.Contains(string.Format("yyj_flash_{0}", key));
        }
    }
}
