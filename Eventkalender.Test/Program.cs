using Eventkalender.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Eventkalender.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {

            EventkalenderController c = new EventkalenderController("Resources/eventkalender-db.xml");

            Stopwatch s = new Stopwatch();

            s.Start();
            List<Event> events = c.GetEvents();
            s.Stop();

            Console.WriteLine(s.ElapsedMilliseconds);

            Console.ReadKey();

        }
    }
}
