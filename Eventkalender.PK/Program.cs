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
            string con = DatabaseClient.GetEntityFrameworkConnectionString("eventkalender-db.xml");
            Console.WriteLine(con);

            EventkalenderDAL dal = new EventkalenderDAL();
            Person p = dal.GetPerson(1);
            Console.WriteLine(p.FirstName);

            string s = DatabaseClient.GetConnectionString("eventkalender-db.xml");
            Console.WriteLine(s);

            Console.ReadKey();
        }
    }
}
