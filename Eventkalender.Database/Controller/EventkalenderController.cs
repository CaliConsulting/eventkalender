using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database
{
    public class EventkalenderController
    {
        private EventkalenderDAL dal;

        public EventkalenderController(string xmlPath)
        {
            dal = new EventkalenderDAL(xmlPath);
        }

        public void AddNation(string name)
        {
            Nation n = new Nation(name);
            dal.AddNation(n);
        }

        public void DeleteNation(int id)
        {
            Nation n = new Nation();
            n.Id = id;
            dal.DeleteNation(n);
        }

        public Nation GetNation(int id)
        {
            return dal.GetNation(id);
        }

        public List<Nation> GetNations()
        {
            return dal.GetNations();
        }

        public void AddEvent(string name, string summary, DateTime startTime, DateTime endTime, int nationId)
        {
            Event e = new Event(name, summary, startTime, endTime, nationId);
            dal.AddEvent(e);
        }

        public void DeleteEvent(int id)
        {
            Event e = new Event();
            e.Id = id;
            dal.DeleteEvent(e);
        }

        public Event GetEvent(int id)
        {
            return dal.GetEvent(id);
        }

        public List<Event> GetEvents()
        {
            return dal.GetEvents();
        }

        public void AddPerson(string firstName, string lastName)
        {
            Person p = new Person(firstName, lastName);
            dal.AddPerson(p);
        }

        public void DeletePerson(int id)
        {
            Person p = new Person();
            p.Id = id;
            dal.DeletePerson(p);
        }

        public Person GetPerson(int id)
        {
            return dal.GetPerson(id);
        }

        public List<Person> GetPersons()
        {
            return dal.GetPersons();
        }

        public void UpdatePerson(Person p)
        {
            dal.UpdatePerson(p);
        }
    }
}
