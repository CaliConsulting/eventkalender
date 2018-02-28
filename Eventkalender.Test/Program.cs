using Eventkalender.Database;
using System;
using System.IO;
using System.Web;


namespace Eventkalender.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EventkalenderDAL d = new EventkalenderDAL("Resources/eventkalender-db.xml");

            //CronusController cronusController = new CronusController("Resources/cronus-db.xml");

            Person p = d.GetPerson(2);

            Event e = d.GetEvent(3);
            Event e1 = d.GetEvent(1);

            p.Events.Add(e);
            //e.Persons.Add(p);

            d.UpdatePerson(p);

            //Employee e = cronusController.GetEmployee("AL");

            //CronusController cronusController = new CronusController("Resources/cronus-db.xml");

            //cronusController.AddEmployee("FFF", "Philip", "Eriksson");

            //Employee e = cronusController.GetEmployee("AL");

            //List<Employee> employees = cronusController.GetEmployees();

            //cronusController.UpdateEmployee("FFFFF", "pHHIILLIP", "Eriksson");

            //cronusController.DeleteEmployee("FFF");

            //string[] files = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories);
            Console.ReadKey();
        }
    }
}