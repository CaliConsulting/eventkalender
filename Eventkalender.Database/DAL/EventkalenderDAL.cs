using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database
{
    public class EventkalenderDAL
    {
        private string xmlPath;

        public EventkalenderDAL(string xmlPath)
        {
            this.xmlPath = xmlPath;
        }

        public void AddEvent(Event e)
        {
            using (var context = new EventkalenderContext(xmlPath))
            {
                context.Event.Add(e);
                context.SaveChanges();
            }
        }

        public void AddNation(Nation n)
        {
            using (var context = new EventkalenderContext(xmlPath))
            {
                context.Nation.Add(n);
                context.SaveChanges();
            }
        }

        public void AddPerson(Person p)
        {
            using (var context = new EventkalenderContext(xmlPath))
            {
                context.Person.Add(p);
                context.SaveChanges();
            }
        }

        public Event GetEvent(int id)
        {
            using (var context = new EventkalenderContext(xmlPath))
            {
                Event dbEvent = context.Event.Include(e => e.Nation).Include(e => e.Persons).SingleOrDefault(e => e.Id == id);

                // Set fields to null to avoid circular references
                if (dbEvent != null)
                {
                    foreach (Person p in dbEvent.Persons ?? Enumerable.Empty<Person>())
                    {
                        if (p != null)
                        {
                            p.Events = null;
                        }
                    }
                    if (dbEvent.Nation != null)
                    {
                        dbEvent.Nation.Events = null;
                    }
                }
                return dbEvent;
            }
        }

        public List<Event> GetEvents()
        {
            using (var context = new EventkalenderContext(xmlPath))
            {
                List<Event> dbEvents = context.Event.Include(e => e.Nation).Include(e => e.Persons).ToList();

                // Set fields to null to avoid circular references
                foreach (Event e in dbEvents ?? Enumerable.Empty<Event>())
                {
                    if (e != null)
                    {
                        foreach (Person p in e.Persons ?? Enumerable.Empty<Person>())
                        {
                            if (p != null)
                            {
                                p.Events = null;
                            }
                        }
                        if (e.Nation != null)
                        {
                            e.Nation.Events = null;
                        }
                    }
                }
                return dbEvents;
            }
        }

        public Nation GetNation(int id)
        {
            using (var context = new EventkalenderContext(xmlPath))
            {
                Nation dbNation = context.Nation.Include(n => n.Events).SingleOrDefault(n => n.Id == id);

                // Set fields to null to avoid circular references
                if (dbNation != null)
                {
                    foreach (Event e in dbNation.Events ?? Enumerable.Empty<Event>())
                    {
                        if (e != null)
                        {
                            e.Nation = null;
                        }
                    }
                }
                return dbNation;
            }
        }

        public List<Nation> GetNations()
        {
            using (var context = new EventkalenderContext(xmlPath))
            {
                List<Nation> dbNations = context.Nation.Include(n => n.Events).Include(n => n.Events.Select(p => p.Persons)).ToList();

                // Set fields to null to avoid circular references
                foreach (Nation n in dbNations ?? Enumerable.Empty<Nation>())
                {
                    if (n != null)
                    {
                        foreach (Event e in n.Events ?? Enumerable.Empty<Event>())
                        {
                            if (e != null)
                            {
                                e.Nation = null;
                                foreach (Person p in e.Persons ?? Enumerable.Empty<Person>())
                                {
                                    if (p != null)
                                    {
                                        p.Events = null;
                                    }
                                }
                            }
                        }
                    }
                }
                return dbNations;
            }
        }

        public Person GetPerson(int id)
        {
            using (var context = new EventkalenderContext(xmlPath))
            {
                Person dbPerson = context.Person.Include(p => p.Events).SingleOrDefault(p => p.Id == id);

                // Set fields to null to avoid circular references
                if (dbPerson != null)
                {
                    foreach (Event e in dbPerson.Events ?? Enumerable.Empty<Event>())
                    {
                        if (e != null)
                        {
                            e.Persons = null;
                        }
                    }
                }
                return dbPerson;
            }
        }

        public List<Person> GetPersons()
        {
            using (var context = new EventkalenderContext(xmlPath))
            {
                List<Person> dbPersons = context.Person.Include(p => p.Events).Include(p => p.Events.Select(n => n.Nation)).ToList();

                // Set fields to null to avoid circular references
                foreach (Person p in dbPersons ?? Enumerable.Empty<Person>())
                {
                    if (p != null)
                    {
                        foreach (Event e in p.Events ?? Enumerable.Empty<Event>())
                        {
                            if (e != null)
                            {
                                e.Persons = null;
                            }
                            if (e.Nation != null)
                            {
                                e.Nation.Events = null;
                            }
                        }
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
