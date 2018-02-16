using Eventkalender.Database;
using Eventkalender.WS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Eventkalender.WS
{
    public class Initialization : IProcessHostPreloadClient
    {
        public void Preload(string[] parameters)
        {
            string eventkalenderDatabaseFilePath = PathUtility.GetPhysicalPath("~/App_Data") + "/eventkalender-db.xml";
            using (EventkalenderContext context = new EventkalenderContext(eventkalenderDatabaseFilePath))
            {
                context.Nation.FirstOrDefault();
            }
        }
    }
}
