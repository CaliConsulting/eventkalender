using Eventkalender.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DatabaseClient.WarmupEntityFramework("Resources/eventkalender-db.xml");
            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine(i);
            //    Thread.Sleep(1000);
            //}

            Thread.Sleep(1);

            Console.WriteLine("DONE");

            EventkalenderController c = new EventkalenderController("Resources/eventkalender-db.xml");

            List<Nation> nations = c.GetNations();
            foreach (Nation n in nations)
            {
                Console.WriteLine(n.Name);
            }


            Console.WriteLine("TESTAR IGEN");
            List<Person> nations2 = c.GetPersons();
            foreach (Person n in nations2)
            {
                Console.WriteLine(n.FullName);
            }

            Console.ReadKey();
        }
    }
}
