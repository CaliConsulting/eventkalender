using System;

namespace Eventkalender.WS.ConsoleApp
{
    public class CronusApp
    {
        private int caseSwitch;
        private bool returnBool;

        private CronusAppData cronusData;
        private CronusAppMetadata cronusMetadata;

        public CronusApp()
        {
            cronusData = new CronusAppData();
            cronusMetadata = new CronusAppMetadata();
        }

        public void ReturnMethod()
        {
            returnBool = false;
            Console.WriteLine("\nDu är nu tillbaka i Huvudmenyn\n");
        }

        public void Start()
        {
            Console.WriteLine("Hej och välkommen till CronusApp\n");
            Console.WriteLine("Välj vad du vill göra!\n");

            while (true)
            {
                returnBool = true;
                Console.WriteLine("\nFör Metadata: Tryck 1\nFör Data: Tryck 2\nGå Tillbaka: Tryck -1");
                string userInput = Console.ReadLine();
                bool isNumeric = int.TryParse(userInput, out caseSwitch);
                if (!isNumeric || (caseSwitch < -1 || caseSwitch > 2))
                {
                    Console.WriteLine("Välj mellan 1, 2 eller -1\n");
                    caseSwitch = 1337;
                }
                switch (caseSwitch)
                {
                    case 1:
                        cronusMetadata.Start();
                        break;
                    case 2:
                        cronusData.Start();
                        break;
                    case -1:
                        ReturnMethod();
                        break;
                    case 0:
                        Program.VeryGoodMethod();
                        break;
                }
                if (!returnBool)
                {
                    break;
                }
            }
        }
    }
}