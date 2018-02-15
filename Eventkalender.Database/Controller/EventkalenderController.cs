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

        public Person GetPerson(int id)
        {
            return dal.GetPerson(id);
        }

        public List<Person> GetPersons()
        {
            return dal.GetPersons();
        }
    }
}
