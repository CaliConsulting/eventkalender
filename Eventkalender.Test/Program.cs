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

            try
            {
                Nation n = null;
                n.Name = "this is null";
                //d.AddNation(n);
            }
            catch (Exception e)
            {
                Console.WriteLine(ExceptionHandler.GetErrorMessage(e));
            }

            d.GetEvent(1000);
            d.GetEvents();

            string res = "";
            res = ExceptionHandler.GetErrorMessage(new FileNotFoundException("meddelande", "file.txt"));
            Console.WriteLine(res);

            Console.ReadKey();
        }
    }
}
