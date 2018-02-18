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

            Nation n2 = new Nation("testnation");

            //Event e2 = new Event("testevent", "testsummary", DateTime.Now, DateTime.Now);
            //e2.Nation = n2;
            //n2.Events.Add(e2);

            //d.AddNation(n2);

            Nation n3 = d.GetNation(1);

            Event e3 = new Event("testevent_ny", "testsummary_ny", DateTime.Now, DateTime.Now);
            n3.Events.Add(e3);
            d.UpdateNation(n3);


            d.GetEvent(1000);
            d.GetEvents();

            string res = "";
            res = ExceptionHandler.GetErrorMessage(new FileNotFoundException("meddelande", "file.txt"));
            Console.WriteLine(res);

            Console.ReadKey();
        }
    }
}
