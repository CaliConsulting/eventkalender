using Eventkalender.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "http://www.ics.lu.se.cali/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class EventkalenderService : WebService
{
    private EventkalenderController eventkalenderController;

    public EventkalenderService()
    {
        string xmlPath = HttpContext.Current.Server.MapPath("~/App_Data/eventkalender-db.xml");
        eventkalenderController = new EventkalenderController(xmlPath);
    }

    [WebMethod]
    public Nation GetNation(int id) {
        return eventkalenderController.GetNation(id);
    }

    [WebMethod]
    public List<Nation> GetNations()
    {
        return eventkalenderController.GetNations();
    }

    [WebMethod]
    public void AddNation(string name)  //Lösning så att tid sätts in i rätt format för datetime
    {
        eventkalenderController.AddNation(name);
       
    }

    [WebMethod]
    public void AddEvent(string name, string summary, DateTime startTime , DateTime endTime)
    {
        eventkalenderController.AddEvent(name,summary, startTime, endTime);
        
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