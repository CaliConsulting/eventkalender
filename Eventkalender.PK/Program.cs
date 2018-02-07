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

            List<Person> persons = dal.GetPersons();
            foreach (Person p in persons)
            {
                Console.WriteLine(p.FullName);
            }

            Console.ReadKey();
        }
    }
}
