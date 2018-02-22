using Eventkalender.Database;
using Eventkalender.WS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "http://cali.eventkalender/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class EventkalenderService : WebService
{
    private EventkalenderController eventkalenderController;

    public EventkalenderService()
    {
        string databaseFilePath = PathUtility.GetPhysicalPath("~/App_Data") + "/eventkalender-db.xml";
        eventkalenderController = new EventkalenderController(databaseFilePath);
    }

    [WebMethod]
    public string GetFile(string path)
    {
        string filePath = string.Format("{0}/Files/{1}", PathUtility.GetPhysicalPath("~/App_Data"), path);
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }
        return string.Empty;
    }

    [WebMethod]
    public List<string> GetFiles()
    {
        string filePath = string.Format("{0}/Files/", PathUtility.GetPhysicalPath("~/App_Data"));
        string[] files = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories);
        return files.Select(s => s.Substring(filePath.Length)).ToList();
    }

    [WebMethod]
    public void AddFile(string path, string content)
    {
        string filePath = string.Format("{0}/Files/{1}", PathUtility.GetPhysicalPath("~/App_Data"), path);
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        using (FileStream ms = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
        {
            using (StreamWriter sw = new StreamWriter(ms))
            {
                sw.Write(content);
            }
        }
    }

    [WebMethod]
    public Nation GetNation(int id)
    {
        return eventkalenderController.GetNation(id);
    }

    [WebMethod]
    public List<Nation> GetNations()
    {
        return eventkalenderController.GetNations();
    }

    [WebMethod]
    public void AddNation(string name)
    {
        eventkalenderController.AddNation(name);
    }

    [WebMethod]
    public void AddEvent(string name, string summary, DateTime startTime, DateTime endTime, int nationId)
    {
        //Lösning så att tid sätts in i rätt format för datetime?
        eventkalenderController.AddEvent(name, summary, startTime, endTime, nationId);
    }

    [WebMethod]
    public Event GetEvent(int id)
    {
        return eventkalenderController.GetEvent(id);
    }

    [WebMethod]
    public List<Event> GetEvents()
    {
        return eventkalenderController.GetEvents();
    }

    [WebMethod]
    public void AddPerson(string firstName, string lastName)
    {
        eventkalenderController.AddPerson(firstName, lastName);
    }

    [WebMethod]
    public Person GetPerson(int id)
    {
        return eventkalenderController.GetPerson(id);
    }

    [WebMethod]
    public List<Person> GetPersons()
    {
        return eventkalenderController.GetPersons();
    }
}