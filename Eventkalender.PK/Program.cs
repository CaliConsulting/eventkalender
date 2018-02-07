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
            Nation n = new Nation();
            n.Name = "TestName";

            Console.WriteLine(n.Name);

            Console.ReadKey();
        }
    }
}
