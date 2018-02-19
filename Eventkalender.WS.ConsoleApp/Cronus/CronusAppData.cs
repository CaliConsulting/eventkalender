using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventkalender.WS.ConsoleApp.CronusReference;

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
      
        public void GetDataByDataTuples(DataTuple[] inputTuple)
        {
            DataTuple[] data = inputTuple;
            List<DataTuple> tuples = data.ToList();
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine(data[i].ToString());
            }
        }

        public void GetIllestPerson()
        {
            Console.WriteLine("Anställd som har varit sjuk flest antal gånger: ");
            DataTuple data = cronusClient.GetIllestPerson();
            Console.WriteLine(data);
            ExitQuestion();
        }

        public void GetIllPersonsByYear()
        {
            Console.WriteLine("Skriv in Årsintervallen, skriv först in startåret:");
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
            GetDataByDataTuples(cronusClient.GetIllPersonsByYear(startYear, endYear));
            ExitQuestion();
        }

        public void GetEmployeeAndRelatives()
        {
            Console.WriteLine("Anställda samt deras släktingar är följande: ");
            GetDataByDataTuples(cronusClient.GetEmployeeAndRelatives());
            ExitQuestion();
        }

        public void GetEmployeeData()
        {
            Console.WriteLine("Employee Data är följande: ");
            GetDataByDataTuples(cronusClient.GetEmployeeData());
            ExitQuestion();
        }

        public void GetEmployeeAbsenceData()
        {
            Console.WriteLine("Employee Absence Data är följande:");
            GetDataByDataTuples(cronusClient.GetEmployeeAbsenceData());
            ExitQuestion();
        }

        public void GetEmployeeRelativeData()
        {
            Console.WriteLine("Employee Relative Data är följande: ");
            GetDataByDataTuples(cronusClient.GetEmployeeRelativeData());
            ExitQuestion();
        }

        public void GetEmployeeQualificationData()
        {
            Console.WriteLine("Employee Qualification Data är följande: ");
            GetDataByDataTuples(cronusClient.GetEmployeeQualificationData());
            ExitQuestion();
        }

        public void GetEmployeePortalSetupData()
        {
            Console.WriteLine("Employee Portal Setup Data är följande: ");
            GetDataByDataTuples(cronusClient.GetEmployeePortalSetupData());
            ExitQuestion();
        }

        public void GetEmployeeStatisticsGroupData()
        {
            Console.WriteLine("Employee Statistics Group Data är följande: ");
            GetDataByDataTuples(cronusClient.GetEmployeeStatisticsGroupData());
            ExitQuestion();
        }

        public void ReturnMethod()
        {
            returnBool = false;
            Console.WriteLine("\nDu är nu tillbaka i Cronus huvudmenyn\n");
        }

        public void ExitQuestion()
        {
            Console.WriteLine("\nVill du återgå till Cronusmenyn? Tryck J, Återgå till Datamenyn? Tryck M");
            string userInput = Console.ReadLine();

            bool valid = Utility.ValidateAlternative(userInput, "J", "M");
            if (!valid)
            {
                Console.WriteLine("Var god välj ett av alternativen.");
                ExitQuestion();
            }

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
        }
    }
}
