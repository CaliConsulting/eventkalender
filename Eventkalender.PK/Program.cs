using Eventkalender.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.PK
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //DatabaseClient client = new DatabaseClient("eventkalender-db.xml");

            string d = Directory.GetCurrentDirectory();
            Console.WriteLine(d);
            Console.Read();

            using (SqlConnection c = DatabaseClient.GetConnection("eventkalender-db.xml"))
            {
                string version = c.ServerVersion;
                Console.WriteLine("V: " + version);
            }

            Console.ReadKey();
        }
    }
}
