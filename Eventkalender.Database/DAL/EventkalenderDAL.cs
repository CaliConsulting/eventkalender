using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                context.Event.Attach(e);
                context.Event.Remove(e);
                context.SaveChanges();
            }
        }

        public void DeleteNation(Nation n)
        {
            using (var context = new EventkalenderContext(xmlPath))
            {
                context.Nation.Attach(n);
                context.Nation.Remove(n);
                context.SaveChanges();
            }
        }

        public void DeletePerson(Person p)
        {
            using (var context = new EventkalenderContext(xmlPath))
            {
                context.Person.Attach(p);
                context.Person.Remove(p);
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
                List <Event> dbEvents = context.Event.Include(e => e.Nation).Include(e => e.Persons).ToList();

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
                Person dbPerson = GetPerson(p.Id);
                if (dbPerson == null)
                {
                    return;
                }

                if (context.Entry(dbPerson).State == EntityState.Detached)
                {
                    //context.Person.Attach(dbPerson);
                }

                List<Event> deletedEvents = dbPerson.Events.Except(p.Events).ToList();
                List<Event> addedEvents = p.Events.Except(dbPerson.Events).ToList();

                deletedEvents.ForEach(c => dbPerson.Events.Remove(c));

                foreach (Event e in addedEvents)
                {
                    if (context.Entry(e).State == EntityState.Detached)
                    {
                        context.Event.Attach(e);
                    }
                    dbPerson.Events.Add(e);
                }
                
                //var deletedEvents = dbPerson.Events.Except(dbP.Events, cours => cours.Id).ToList();

                //Event dbEvent = context.Event.Find(2);
                //if (dbEvent == null)
                //{
                //    return;
                //}

                //dbPerson.Events.Add(dbEvent);


                ////get current entry from db (db is context)
                //var item = context.Entry(p);

                ////change item state to modified
                //item.State = System.Data.Entity.EntityState.Modified;

                ////load existing items for ManyToMany collection
                ////item.Collection(i => i.Students).Load();

                //List<int> ids = p.Events.Select(temp => temp.Id).ToList();

                ////clear Student items
                //p.Events.Clear();

                ////add Toner items
                //foreach (int studentId in ids)
                //{
                //    var student = context.Event.Find(studentId);
                //    item.Entity.Events.Add(student);
                //}
                context.SaveChanges();
            }
        }

        //public void AddNations(List<Nation> nations)
        //{
        //    context.Nations.Add()
        //}
    }
}
