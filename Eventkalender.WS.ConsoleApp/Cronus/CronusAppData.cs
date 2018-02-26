using Eventkalender.Database;
using Eventkalender.WS.ConsoleApp.CronusReference;
using Eventkalender.WS.ConsoleApp.Utility;
using System;

namespace Eventkalender.WS.ConsoleApp
{
    public class CronusAppData
    {
        private bool returnBool;
        private int caseSwitch;

        private CronusServiceSoapClient cronusClient;

        public CronusAppData()
        {
            cronusClient = new CronusServiceSoapClient();
        }

        public void GetOutput(CronusReference.DataTuple[] inputTuple)
        {
            Console.WriteLine(ConversionUtility.ToString(inputTuple));
        }

        public void GetIllestPerson()
        {
            Console.WriteLine("Anställd som har varit sjuk flest antal gånger: ");
            CronusReference.DataTuple data = cronusClient.GetIllestPerson();
            Console.WriteLine(ConversionUtility.ToString(data));
            ExitQuestion();
        }

        public void GetIllPersonsByYear()
        {
            Console.WriteLine("Skriv in årsintervallen, skriv först in startåret:");
            string userInput = Console.ReadLine();
            bool isNumericStartYear = int.TryParse(userInput, out int startYear);

            Console.WriteLine("Skriv in slutåret");
            userInput = Console.ReadLine();
            bool isNumericEndYear = int.TryParse(userInput, out int endYear);

            if (!isNumericStartYear || !isNumericEndYear)
            {
                Console.WriteLine("Antingen så var startåret eller slutåret inte i korrekt format - Exempel *2000*");
                ExitQuestion();
            }

            Console.WriteLine("Följande personer har varit sjuka mellan år {0} och {1}", startYear, endYear);
            GetOutput(cronusClient.GetIllPersonsByYear(startYear, endYear));
            ExitQuestion();
        }

        public void GetEmployeeAndRelatives()
        {
            Console.WriteLine("Anställda samt deras släktingar är följande: ");
            GetOutput(cronusClient.GetEmployeeAndRelatives());
            ExitQuestion();
        }

        public void GetEmployeeData()
        {
            Console.WriteLine("Employee Data är följande: ");
            GetOutput(cronusClient.GetEmployeeData());
            ExitQuestion();
        }

        public void GetEmployeeAbsenceData()
        {
            Console.WriteLine("Employee Absence Data är följande:");
            GetOutput(cronusClient.GetEmployeeAbsenceData());
            ExitQuestion();
        }

        public void GetEmployeeRelativeData()
        {
            Console.WriteLine("Employee Relative Data är följande: ");
            GetOutput(cronusClient.GetEmployeeRelativeData());
            ExitQuestion();
        }

        public void GetEmployeeQualificationData()
        {
            Console.WriteLine("Employee Qualification Data är följande: ");
            GetOutput(cronusClient.GetEmployeeQualificationData());
            ExitQuestion();
        }

        public void GetEmployeePortalSetupData()
        {
            Console.WriteLine("Employee Portal Setup Data är följande: ");
            GetOutput(cronusClient.GetEmployeePortalSetupData());
            ExitQuestion();
        }

        public void GetEmployeeStatisticsGroupData()
        {
            Console.WriteLine("Employee Statistics Group Data är följande: ");
            GetOutput(cronusClient.GetEmployeeStatisticsGroupData());
            ExitQuestion();
        }

        public void ReturnMethod()
        {
            returnBool = false;
            Console.WriteLine("\nDu är nu tillbaka i Cronus huvudmenyn\n");
        }

        public void ExitQuestion()
        {
            Console.WriteLine("\nVill du återgå till Cronusmenyn? Tryck J");
            Console.WriteLine("Vill du återgå till senaste menyn? Skriv in valfritt värde");
            string userInput = Console.ReadLine();
            if (userInput.ToUpper().Equals("J"))
            {
                returnBool = false;
            }
        }

        public void Start()
        {
            while (true)
            {
                returnBool = true;

                Console.WriteLine("\nVad för Data är du intresserad av?\n");
                Console.WriteLine("Hämta personen som varit sjuk flest gånger: Tryck 1");
                Console.WriteLine("Hämta sjuka personer under specifika årsintervall: Tryck 2");
                Console.WriteLine("Hämta anställda och deras släktingar: Tryck 3");
                Console.WriteLine("Hämta Employee Data: Tryck 4");
                Console.WriteLine("Hämta Employee Absence Data: Tryck 5");
                Console.WriteLine("Hämta Employee Relative Data: Tryck 6");
                Console.WriteLine("Hämta Employee Qualification Data: Tryck 7");
                Console.WriteLine("Hämta Employee Portal Setup Data: Tryck 8");
                Console.WriteLine("Hämta Employee Statistics Group Data: Tryck 9");
                Console.WriteLine("För att gå tillbaka: Tryck -1");

                string userInput = Console.ReadLine();
                bool isNumeric = int.TryParse(userInput, out caseSwitch);

                if (!isNumeric || (caseSwitch < -1 || caseSwitch > 9))
                {
                    Console.WriteLine("Du måste sätta in ett nummer mellan -1 och 9!");
                    ExitQuestion();
                }
                try
                {
                    switch (caseSwitch)
                    {
                        case 1:
                            GetIllestPerson();
                            break;
                        case 2:
                            GetIllPersonsByYear();
                            break;
                        case 3:
                            GetEmployeeAndRelatives();
                            break;
                        case 4:
                            GetEmployeeData();
                            break;
                        case 5:
                            GetEmployeeAbsenceData();
                            break;
                        case 6:
                            GetEmployeeRelativeData();
                            break;
                        case 7:
                            GetEmployeeQualificationData();
                            break;
                        case 8:
                            GetEmployeePortalSetupData();
                            break;
                        case 9:
                            GetEmployeeStatisticsGroupData();
                            break;
                        case 0:
                            Program.VeryGoodMethod();
                            break;
                        case -1:
                            ReturnMethod();
                            break;
                    }
                    if (!returnBool)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(ExceptionHandler.GetErrorMessage(e));
                    ExitQuestion();
                }
            }
        }
    }
}