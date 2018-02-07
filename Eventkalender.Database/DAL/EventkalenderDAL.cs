using Eventkalender.Database.Context;
using Eventkalender.Database.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database.DAL
{
    public class EventkalenderDAL
    {
        //private EventkalenderContext context;

        public EventkalenderDAL()
        {
            //context = new EventkalenderContext(DatabaseClient.GetConnectionString("eventkalender-db.xml"));
        }

        public void AddNation(Nation n)
        {
            using (var context = new EventkalenderContext())
            {
                context.Nation.Add(n);
                context.SaveChanges();
            }
        }

        public Nation GetNation(int id)
        {
            using (var context = new EventkalenderContext())
            {
                return context.Nation.Find(id);
            }
        }

        public IQueryable<Nation> GetNations()
        {
            using (var context = new EventkalenderContext())
            {
                return context.Nation;
            }
        }

        public void AddEvent(Event e)
        {
            using (var context = new EventkalenderContext())
            {
                context.Event.Add(e);
                context.SaveChanges();
            }
        }

        public Event GetEvent(int id)
        {
            using (var context = new EventkalenderContext())
            {
                return context.Event.Find(id);
            }
        }

        public IQueryable<Event> GetEvents()
        {
            using (var context = new EventkalenderContext())
            {
                return context.Event;
            }
        }

        public void AddPerson(Person p)
        {
            using (var context = new EventkalenderContext())
            {
                context.Person.Add(p);
                context.SaveChanges();
            }
        }

        public Person GetPerson(int id)
        {
            using (var context = new EventkalenderContext())
            {
                return context.Person.Find(id);
            }
        }

        public List<Person> GetPersons()
        {
            using (var context = new EventkalenderContext())
            {
                return context.Person.ToList();
            }
        }

        //public void AddNations(List<Nation> nations)
        //{
        //    context.Nations.Add()
        //}
    }
}
