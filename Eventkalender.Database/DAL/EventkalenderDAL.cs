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

        public void AddEvent(Event e)
        {
            using (var context = new EventkalenderContext())
            {
                context.Event.Add(e);
                context.SaveChanges();
            }
        }

        public void AddNation(Nation n)
        {
            using (var context = new EventkalenderContext())
            {
                context.Nation.Add(n);
                context.SaveChanges();
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

        public Event GetEvent(int id)
        {
            using (var context = new EventkalenderContext())
            {
                Event dbEvent = context.Event.Include(e => e.Nation).Include(e => e.Persons).SingleOrDefault(e => e.Id == id);

                // Set fields to null to avoid circular references
                foreach (Person p in dbEvent.Persons)
                {
                    p.Events = null;
                }
                dbEvent.Nation.Events = null;
                return dbEvent;
            }
        }

        public List<Event> GetEvents()
        {
            using (var context = new EventkalenderContext())
            {
                List<Event> dbEvents = context.Event.Include(e => e.Nation).Include(e => e.Persons).ToList();

                // Set fields to null to avoid circular references
                foreach (Event e in dbEvents)
                {
                    foreach (Person p in e.Persons)
                    {
                        p.Events = null;
                    }
                    e.Nation.Events = null;
                }
                return dbEvents;
            }
        }

        public Nation GetNation(int id)
        {
            using (var context = new EventkalenderContext())
            {
                Nation dbNation = context.Nation.Include(n => n.Events).SingleOrDefault(n => n.Id == id);

                // Set fields to null to avoid circular references
                foreach (Event e in dbNation.Events)
                {
                    e.Nation = null;
                }
                return dbNation;
            }
        }

        public List<Nation> GetNations()
        {
            using (var context = new EventkalenderContext())
            {
                List<Nation> dbNations = context.Nation.Include(n => n.Events).ToList();

                // Set fields to null to avoid circular references
                foreach (Nation n in dbNations)
                {
                    foreach (Event e in n.Events)
                    {
                        e.Nation = null;
                    }
                }
                return dbNations;
            }
        }

        public Person GetPerson(int id)
        {
            using (var context = new EventkalenderContext())
            {
                Person dbPerson = context.Person.Include(p => p.Events).SingleOrDefault(p => p.Id == id);

                // Set fields to null to avoid circular references
                foreach (Event e in dbPerson.Events)
                {
                    e.Persons = null;
                }
                return dbPerson;
            }
        }

        public List<Person> GetPersons()
        {
            using (var context = new EventkalenderContext())
            {
                List<Person> dbPersons = context.Person.Include(p => p.Events).Include(p => p.Events.Select(n => n.Nation)).ToList();

                // Set fields to null to avoid circular references
                foreach (Person p in dbPersons)
                {
                    foreach (Event e in p.Events)
                    {
                        e.Persons = null;
                        e.Nation.Events = null;
                    }
                }
                return dbPersons;
            }
        }

        //public void AddNations(List<Nation> nations)
        //{
        //    context.Nations.Add()
        //}
    }
}
