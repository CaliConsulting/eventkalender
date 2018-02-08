using Eventkalender.Database.DAL;
using Eventkalender.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "http://www.ics.lu.se.cali/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
// Anropa controller här istället för att direkt anropa databasen. sen till controller så jag kan göra using, fixa reference.

public class Service : System.Web.Services.WebService
{

    private EventkalenderDAL eventkalender;

    public Service () {

        eventkalender = new EventkalenderDAL();
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        
    }


    [WebMethod]
    public string Testmetod()
    {
        return "Det är en testmetod";
    }

    [WebMethod]
    public Nation GetNation(int id) {

        return eventkalender.GetNation(id);
    }
    [WebMethod]
    public List<Nation> GetNations()
    {
        return eventkalender.GetNations();
    }
    [WebMethod]
    public void AddNation()
    {
        eventkalender.AddNation(new Nation());
       //parameter in här med namn?? string name?
    }
    [WebMethod]
    public void AddEvent()
    {
        eventkalender.AddEvent(new Event());
        //parameter in här med namn?? string name?
    }
    [WebMethod]
    public Event GetEvent(int id)
    {
        return eventkalender.GetEvent(id);
        
    }
    [WebMethod]
    public List<Event> GetEvents()
    {
        return eventkalender.GetEvents();
    }
    [WebMethod]
    public void AddPerson()
    {
        eventkalender.AddPerson(new Person());
        //parameter in här med namn?? string name?
    }
    [WebMethod]
    public Person GetPerson(int id)
    {
        return eventkalender.GetPerson(id);

    }
    [WebMethod]
    public List<Person> GetPersons()
    {
        return eventkalender.GetPersons();
    }





}