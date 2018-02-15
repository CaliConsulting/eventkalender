<%@ Application Language="C#" %>
<%@ Import namespace="Eventkalender.Database" %>
<%@ Import namespace="Eventkalender.WS" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

        // Warm up EntityFramework
        //Start(() =>
        //{
        //    string databaseFilePath = PathUtility.GetPath("~/App_Data") + "/eventkalender-db.xml";
        //    using (EventkalenderContext context = new EventkalenderContext(databaseFilePath))
        //    {
        //        context.
        //    }
        //});



        // Warm up EntityFramework
        string eventkalenderDatabaseFilePath = PathUtility.GetPhysicalPath("~/App_Data") + "/eventkalender-db.xml";
        using (EventkalenderContext context = new EventkalenderContext(eventkalenderDatabaseFilePath))
        {
            context.Nation.FirstOrDefault();
        }
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

</script>
