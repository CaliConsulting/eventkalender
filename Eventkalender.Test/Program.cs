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

            //Person p = d.GetPerson(2);

            //Event e = d.GetEvent(3);
            //Event e1 = d.GetEvent(1);

            ////e.Persons.Add(p);

            //p.Events.Remove(e);

            //d.UpdatePerson(p);


            //Employee e = cronusController.GetEmployee("AL");
            
            //CronusController cronusController = new CronusController("Resources/cronus-db.xml");

            //cronusController.AddEmployee("FFF", "Philip", "Eriksson");

            
            //Employee e = cronusController.GetEmployee("AL");

            //List<Employee> employees = cronusController.GetEmployees();

            //cronusController.UpdateEmployee("FFFFF", "pHHIILLIP", "Eriksson");

            //cronusController.DeleteEmployee("FFF");

            Console.ReadKey();
        }
    }
}
