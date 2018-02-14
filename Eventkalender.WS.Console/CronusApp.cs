using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventkalender.WS.ConsoleApp.CronusReference;

namespace Eventkalender.WS.ConsoleApp
{
    public class CronusApp
    {
        CronusServiceSoapClient cronusclient = new CronusServiceSoapClient();
        EventkalenderApp eventApp = new EventkalenderApp();
        CronusData cronusData = new CronusData();
        CronusMetaData cronusMetaData = new CronusMetaData();
        int caseSwitch;

        public void GetColumnsForEmployeeTable()
        {

        }
        
        public void Start()
        {
            while (true)
            {
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
                        break;
                    case 0:
                        eventApp.VeryGoodMethod();
                        break;
                }
                if(caseSwitch == -1)
                {
                    break;
                }

            }
            
        }

    }
}
