using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eventkalender.WS
{
    public class PathUtility
    {
        public PathUtility()
        {

        }

        public static string GetPhysicalPath(string folder)
        {
            return HttpContext.Current.Server.MapPath(folder);
        }
    }
}
