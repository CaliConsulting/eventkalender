using Eventkalender.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EventkalenderDAL d = new EventkalenderDAL("Resources/eventkalender-db.xml");

            //CronusController cronusController = new CronusController("Resources/cronus-db.xml");

            //cronusController.AddEmployee("FFFFF", "Philip", "Eriksson");

            Person p = d.GetPerson(2);

            Event e = d.GetEvent(3);

            p.Events.Add(e);
            e.Persons.Add(p);

            d.UpdatePerson(p);


            //Employee e = cronusController.GetEmployee("AL");

            //List<Employee> employees = cronusController.GetEmployees();

            //cronusController.UpdateEmployee("FFFFF", "pHHIILLIP", "Eriksson");

            Console.ReadKey();
        }
    }
}
