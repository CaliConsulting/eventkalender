using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Eventkalender.WS.ConsoleApp
{
    public class CronusApp
    {
        
        EventkalenderApp eventApp = new EventkalenderApp();
        CronusAppData cronusData = new CronusAppData();
        CronusAppMetaData cronusMetaData = new CronusAppMetaData();
        private int caseSwitch;
        private bool returnBool;

        public void ReturnMethod()
        {
            returnBool = false;
            Console.WriteLine("\nDu är nu tillbaka i Huvudmenyn\n");
        }

        public void Start()
        {
            Console.WriteLine("Hej och välkommen till CronusApp\n");
            while (true)
            {
                returnBool = true;
                Console.WriteLine("\nFör Metadata: Tryck 1\nFör Data: Tryck 2\nGå Tillbaka: Tryck -1");
                string userInput = Console.ReadLine();
                bool isNumeric = int.TryParse(userInput, out caseSwitch);

                if (!isNumeric || (caseSwitch < -1 || caseSwitch > 2))
                {
                    Console.WriteLine("Välj mellan 1,2 eller -1\n");
                    caseSwitch = 1337;
                }
                switch (caseSwitch)
                {
                    case 1:
                        cronusMetaData.Start();
                        break;
                    case 2:
                        cronusData.Start();
                        break;
                    case -1:
                        ReturnMethod();
                        break;
                    case 0:
                        eventApp.VeryGoodMethod();
                        break;
                }
                if(!returnBool)
                {
                    break;
                }

            }
            
        }

    }
}
