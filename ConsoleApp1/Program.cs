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
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
                DatabaseClient.WarmupEntityFramework("Resources/eventkalender-db.xml");
                Thread.Sleep(1000);
            }
        }
    }
}
