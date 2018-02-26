using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

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
                context.Entry(e).Reference(temp => temp.Nation).Load();
                context.Entry(e).Collection(temp => temp.Persons).Load();
            }
        }

        public void AddNation(Nation n)
        {
            using (var context = new EventkalenderContext(xmlPath))
            {
                context.Nation.Add(n);
                context.SaveChanges();
                context.Entry(n).Collection(temp => temp.Events).Load();
            }
        }

        public void AddPerson(Person p)
        {
            using (var context = new EventkalenderContext(xmlPath))
            {
                context.Person.Add(p);
                context.SaveChanges();
                context.Entry(p).Collection(temp => temp.Events).Load();
            }
        }

        public void DeleteEvent(Event e)
        {
            using (var context = new EventkalenderContext(xmlPath))
            {
                Event dbEvent = context.Event.Find(e.Id);
                if (dbEvent == null)
                {
                    return;
                }
                context.Event.Remove(dbEvent);
                context.SaveChanges();
            }
        }

        public void DeleteNation(Nation n)
        {
            using (var context = new EventkalenderContext(xmlPath))
            {
                Nation dbNation = context.Nation.Find(n.Id);
                if (dbNation == null)
                {
                    return;
                }
                context.Nation.Remove(dbNation);
                context.SaveChanges();
            }
        }

        public void DeletePerson(Person p)
        {
            using (var context = new EventkalenderContext(xmlPath))
            {
                Person dbPerson = context.Person.Find(p.Id);
                if (dbPerson == null)
                {
                    return;
                }
                context.Person.Remove(dbPerson);
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

        public void UpdatePerson(Person p)
        {
            using (var context = new EventkalenderContext(xmlPath))
            {
                Person dbPerson = context.Person.Include(temp => temp.Events).SingleOrDefault(temp => temp.Id == p.Id);
                if (dbPerson == null)
                {
                    return;
                }

                List<Event> addedEvents = p.Events.Except(dbPerson.Events).ToList();
                List<Event> deletedEvents = dbPerson.Events.Except(p.Events).ToList();

                deletedEvents.ForEach(c => dbPerson.Events.Remove(c));

                foreach (Event e in addedEvents)
                {
                    // Set fields to null to avoid circular references
                    e.Nation = null;
                    e.Persons = null;

                    DbEntityEntry eventEntry = context.Entry(e);
                    if (eventEntry.State == EntityState.Detached)
                    {
                        context.Event.Attach(e);
                    }
                    dbPerson.Events.Add(e);
                }
                context.SaveChanges();
            }
        }

        //public void AddNations(List<Nation> nations)
        //{
        //    context.Nations.Add()
        //}
    }
}