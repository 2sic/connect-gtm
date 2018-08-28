using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Portals;

namespace Connect.Gtm
{
    public static class ScriptTag
    {
        public static string Basic(bool ip, bool isEditor)
        {
            var header = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            var ipValue = String.IsNullOrEmpty(header) ? HttpContext.Current.Request.UserHostAddress : header.Split(',').First().Trim();

            var isEditorValue = PortalSettings.Current != null
                ? (PortalSettings.Current.UserInfo.IsInRole(PortalSettings.Current.AdministratorRoleName))
                : false;

            var script = @"
                <script>
                    (function(w) {
                    w.gtmData = w.gtmData || {};
                    // IP to detect internal access, will not be stored";

            if (ip)
                script += $"w.gtmData.ipAddress = '{ipValue}';";
            if (isEditor)
                script += $"w.gtmData.isEditor = {isEditorValue.ToString().ToLower()};";

            script += @"})(window);
                </script>";

            return script;
        }
    }
}