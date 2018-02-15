using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventkalender.WS.ConsoleApp.CronusReference;

namespace Eventkalender.WS.ConsoleApp
{
    public class CronusAppMetaData
    {
        CronusServiceSoapClient cronusclient = new CronusServiceSoapClient();
        EventkalenderApp eventApp = new EventkalenderApp();
        private bool returnBool;
        private int caseSwitch;

        public void GetMetadataListOfString(List<string> metod)
        {
            List<string> outputList = metod;

            /* int limit = Math.Min(metod.Count, 65000);
            for (int i = 0; i < limit; i++)
            {
                Console.WriteLine(metod.ElementAt(i));
            } */

            foreach (string row in outputList)
            {
                Console.WriteLine(row);
            }
        }

        public void GetColumnsForEmployeeTable()
        {
            Console.WriteLine("Följande är kolumner i Personaltabellen");
            GetMetadataListOfString(cronusclient.GetColumnsForEmployeeTable());
            ExitQuestion();
        }

        public void GetKeys()
        {
            Console.WriteLine("Följande är alla nycklar: ");
            GetMetadataListOfString(cronusclient.GetKeys());
            ExitQuestion();
        }
        
        public void GetIndexes()
        {
            Console.WriteLine("Konsollen tillåter inte mer än 65 tusen prints vilket indexes överskrider");
            Console.WriteLine("Därför får du iställlet denna output: Hata data, älska tenta");
            GetMetadataListOfString(cronusclient.GetIndexes());
            ExitQuestion();
        }

        public void GetTableConstraints()
        {
            Console.WriteLine("Följande är alla constraints: ");
            GetMetadataListOfString(cronusclient.GetTableConstraints());
            ExitQuestion();
        }

        public void GetTables()
        {
            Console.WriteLine("Följande är alla tables: ");
            GetMetaDataListOfString(cronusclient.GetTables());
            ExitQuestion();
        }
        
        public void GetMetadataByDataTuples(DataTuple[] inputTuple)
        {
            DataTuple[] data = inputTuple;
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine(data[i].ToString());
            }
        }

        public void GetEmployeeMetadata()
        {
            Console.WriteLine("Employee Metadata är följande: ");
            GetMetadataByDataTuples(cronusclient.GetEmployeeMetadata());
            ExitQuestion();
        }
        public void GetEmployeeAbsenceMetadata()
        {
            Console.WriteLine("Employee Absence Metadata är följande:");
            GetMetadataByDataTuples(cronusclient.GetEmployeeAbsenceMetadata());
            ExitQuestion();
        }

        public void GetEmployeeRelativeMetadata()
        {
            Console.WriteLine("Employee Relative Metadata är följande: ");
            GetMetadataByDataTuples(cronusclient.GetEmployeeRelativedetaData());
            ExitQuestion();
        }
        
        public void GetEmployeeQualificationMetadata()
        {
            Console.WriteLine("Employee Qualification Metadata är följande: ");
            GetMetadataByDataTuples(cronusclient.GetEmployeeQualificationMetadata());
            ExitQuestion();
        }
        
        public void GetEmployeePortalSetupMetadata()
        {
            Console.WriteLine("Employee Portal Setup Metadata är följande: ");
            GetMetadataByDataTuples(cronusclient.GetEmployeePortalSetupMetadata());
            ExitQuestion();
        }

        public void GetEmployeeStatisticsGroupMetadata()
        {
            Console.WriteLine("Employee Statistics Group Metadata är följande: ");
            GetMetadataByDataTuples(cronusclient.GetEmployeeStatisticsGroupMetadata());
            ExitQuestion();
        }

        public void ReturnMethod()
        {
            returnBool = false;
            Console.WriteLine("\nDu är nu tillbaka i Cronus huvudmenyn\n");
        }
        public void ExitQuestion()
        {
            Console.WriteLine("\nVill du återgå till Cronusmenyn? Tryck J, Återgå till Metadatamenyn? Tryck M");
            string userInput = Console.ReadLine();
            userInput = userInput.Substring(0, 1);
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
                
                Console.WriteLine("\nVad för Metadata är du intresserad av?!\n");
                Console.WriteLine("Hämta alla nycklar: Tryck 1");
                Console.WriteLine("Hämta kolumner i Personaltabellen : Tryck 2");
                Console.WriteLine("Hämta alla indexes: Tryck 3");
                Console.WriteLine("Hämta alla constraints: Tryck 4");
                Console.WriteLine("Hämta alla tables : Tryck 5");
                Console.WriteLine("Hämta Employee Metadata: Tryck 6");
                Console.WriteLine("Hämta Employee Absence Metadata: Tryck 7");
                Console.WriteLine("Hämta Employee Relative Metadata: Tryck 8");
                Console.WriteLine("Hämta Employee Qualification Metadata: Tryck 9");
                Console.WriteLine("Hämta Employee Portal Setup Metadata: Tryck 10");
                Console.WriteLine("Hämta Employee Statistics Group Metadata: Tryck 11");
                Console.WriteLine("För att gå tillbaka: Tryck -1");

                string userInput = Console.ReadLine();
                bool isNumeric = int.TryParse(userInput, out caseSwitch);

                if (!isNumeric || (caseSwitch < -1 || caseSwitch > 11))
                {
                    Console.WriteLine("Du måste sätta in ett nummer mellan -1 och 11!");
                    ExitQuestion();

                }
                switch (caseSwitch)
                {
                    case 1:
                        GetKeys();
                        break;
                    case 2:
                        GetColumnsForEmployeeTable();
                        break;
                    case 3:
                        GetIndexes();
                        break;
                    case 4:
                        GetTableConstraints();
                        break;
                    case 5:
                        GetTables();
                        break;
                    case 6:
                        GetEmployeeMetadata();
                        break;
                    case 7:
                        GetEmployeeAbsenceMetadata();
                        break;
                    case 8:
                        GetEmployeeRelativeMetadata();
                        break;
                    case 9:
                        GetEmployeeQualificationMetadata();
                        break;
                    case 10:
                        GetEmployeePortalSetupMetadata();
                        break;
                    case 11:
                        GetEmployeeStatisticsGroupMetadata();
                        break;
                    case 0:
                        eventApp.VeryGoodMethod();
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
