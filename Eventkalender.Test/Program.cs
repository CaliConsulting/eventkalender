using Eventkalender.Database;
using System;
using System.Collections.Generic;
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

            try
            {
                Nation n = new Nation("testnation");
                n.Id = 1;
                d.AddNation(n);
            }
            catch (Exception e)
            {
                Console.WriteLine(ExceptionHandler.GetErrorMessage(e));
            }

            Console.ReadKey();


        }
    }
}
