using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eventkalender.WS.ConsoleApp.EventkalenderReference;
using Eventkalender.WS.ConsoleApp.CronusReference;

namespace Eventkalender.WS.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            int caseSwitch;
            EventkalenderApp eventApp = new EventkalenderApp();
            CronusApp cronusApp = new CronusApp();
            Console.WriteLine("Hej och välkommen till den perfekta Konsollapplikationen\n");
            Console.WriteLine("Vilken applikation vill du köra?");

            while (true)
            {
                
                Console.WriteLine("\nEventkalenderapplikation: Tryck 1\nCronusapplikationen: Tryck 2\nAvsluta programmet: Tryck -1");
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
                        eventApp.Start();
                        break;
                    case 2:
                        cronusApp.Start();
                        break;
                    case -1:
                        break;
                    case 0:
                        eventApp.VeryGoodMethod();
                        break;
                }
                if(caseSwitch == -1){
                    break;
                }
            }
        }

      
       
    }
}
