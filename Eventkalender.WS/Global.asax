<%@ Application Language="C#" %>
<%@ Import namespace="System" %>
<%@ Import namespace="System.IO" %>
<%@ Import namespace="System.Threading" %>
<%@ Import namespace="Eventkalender.Database" %>
<%@ Import namespace="Eventkalender.WS" %>

<script runat="server">
    
    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

        // Warm up EntityFramework
        string eventkalenderDatabaseFilePath = PathUtility.GetPhysicalPath("~/App_Data") + "/eventkalender-db.xml";
        //using (EventkalenderContext context = new EventkalenderContext(eventkalenderDatabaseFilePath))
        //{
        //    context.Nation.FirstOrDefault();
        //}


        ParameterizedThreadStart pts = new ParameterizedThreadStart(WarmupEntityFramework);
        Thread t = new Thread(pts);
        t.Start(eventkalenderDatabaseFilePath);
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    public static void WarmupEntityFramework(object xmlPath)
    {
        while (true)
        {
            string xmlPathAsString = xmlPath as string;
            EventkalenderController c = new EventkalenderController(xmlPathAsString);
            List<Nation> nations = c.GetNations();

            //Console.WriteLine("WARMUP2 DONE");

            string path = Path.GetDirectoryName(xmlPathAsString);

            string logMessage = "Warmup iteration done, sleeping 60 seconds...";

            System.IO.File.AppendAllText(string.Format(@"{0}\log2.txt", path), string.Format("{0} {1}{2}", DateTime.Now, logMessage, Environment.NewLine));

            System.Diagnostics.Debug.WriteLine(logMessage);

            // Sleep 1 minute
            Thread.Sleep(60 * 1000);
        }
    }

</script>
