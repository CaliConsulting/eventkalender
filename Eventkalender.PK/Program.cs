using Eventkalender.Database;
using Eventkalender.Database.DAL;
using Eventkalender.Database.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.PK
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EventkalenderDAL dal = new EventkalenderDAL();

            // Create persons
            Person p1 = new Person("Daniel", "Nilsson");
            Person p2 = new Person("Johan", "Dyster-Aas");
            Person p3 = new Person("Philip", "Eriksson");
            Person p4 = new Person("Hampus", "Sandell");

            dal.AddPerson(p1);
            dal.AddPerson(p2);
            dal.AddPerson(p3);
            dal.AddPerson(p4);

            // Create events
            //DateTime startTime = DateTime.Now.AddDays(-1);
            //DateTime endTime = DateTime.Now.AddDays(1);
            //Event e1 = new Event("Casanova", "Meat is back on the menu boys", startTime, endTime);
            //Event e2 = new Event("Sunset", "Meat is back on the menu girls", startTime, endTime);
            
            //dal.AddEvent(e1);
            //dal.AddEvent(e2);

            // Create nations
            //Nation n1 = new Nation("Malmo Nation");
            //Nation n2 = new Nation("Ostgota Nation");

            //dal.AddNation(n1);
            //dal.AddNation(n2);

            //Person johan = dal.GetPerson(2);




            //ICollection<Event> events = johan.Events;

            //foreach (Event a in events)
            //{
            //    Console.WriteLine(a.Name);
            //}

            List<Person> persons = dal.GetPersons().ToList();
            foreach (Person p in persons)
            {
                Console.WriteLine(p.FullName);
            }

            Console.WriteLine("END");

            Console.ReadKey();
        }
    }
}
