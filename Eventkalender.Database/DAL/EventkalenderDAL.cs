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
                return context.Nation.Include(n => n.Events).SingleOrDefault(n => n.Id == id);
            }
        }

        public List<Nation> GetNations()
        {
            using (var context = new EventkalenderContext())
            {
                return context.Nation.Include(n => n.Events).ToList();
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
                return context.Event.Include(e => e.Persons).Include(e => e.Persons).SingleOrDefault(e => e.Id == id);
            }
        }

        public List<Event> GetEvents()
        {
            using (var context = new EventkalenderContext())
            {
                return context.Event.Include(e => e.Nation).Include(e => e.Persons).ToList();
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
                return context.Person.Include(p => p.Events).SingleOrDefault(p => p.Id == id);
            }
        }

        public List<Person> GetPersons()
        {
            using (var context = new EventkalenderContext())
            {
                return context.Person.Include(p => p.Events).ToList();
            }
        }

        //public void AddNations(List<Nation> nations)
        //{
        //    context.Nations.Add()
        //}
    }
}
