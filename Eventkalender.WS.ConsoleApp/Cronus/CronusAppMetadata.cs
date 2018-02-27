using Eventkalender.Database;
using Eventkalender.WS.ConsoleApp.CronusReference;
using Eventkalender.WS.ConsoleApp.Utility;
using System;
using System.Collections.Generic;

namespace Eventkalender.WS.ConsoleApp
{
    public class CronusAppMetadata
    {
        private bool returnBool;
        private int caseSwitch;

        private CronusServiceSoapClient cronusClient;

        public CronusAppMetadata()
        {
            cronusClient = new CronusServiceSoapClient();
        }

        public void GetOutput(List<string> lst)
        {
            if (lst?.Count > 0)
            {
                // Remove column header
                lst.RemoveAt(0);
            }
            foreach (string row in lst)
            {
                Console.WriteLine(row);
            }
        }

        public void GetColumnsForEmployeeTable()
        {
            Console.WriteLine("Följande är kolumner i personaltabellen: ");
            GetOutput(cronusClient.GetColumnsForEmployeeTable());
            ExitQuestion();
        }

        public void GetKeys()
        {
            Console.WriteLine("Följande är alla nycklar: ");
            GetOutput(cronusClient.GetKeys());
            ExitQuestion();
        }

        public void GetIndexes()
        {
            Console.WriteLine("Följande är alla indexes: ");
            GetOutput(cronusClient.GetIndexes());
            ExitQuestion();
        }

        public void GetTableConstraints()
        {
            Console.WriteLine("Följande är alla constraints: ");
            GetOutput(cronusClient.GetTableConstraints());
            ExitQuestion();
        }

        public void GetTables()
        {
            Console.WriteLine("Följande är alla tabeller: ");
            GetOutput(cronusClient.GetTables());
            ExitQuestion();
        }

        public void GetOutput(CronusReference.DataTuple[] inputTuple)
        {
            Console.WriteLine(ConversionUtility.ToString(inputTuple));
        }

        public void GetEmployeeMetadata()
        {
            Console.WriteLine("Employee Metadata är följande: ");
            GetOutput(cronusClient.GetEmployeeMetadata());
            ExitQuestion();
        }

        public void GetEmployeeAbsenceMetadata()
        {
            Console.WriteLine("Employee Absence Metadata är följande:");
            GetOutput(cronusClient.GetEmployeeAbsenceMetadata());
            ExitQuestion();
        }

        public void GetEmployeeRelativeMetadata()
        {
            Console.WriteLine("Employee Relative Metadata är följande: ");
            GetOutput(cronusClient.GetEmployeeRelativeMetadata());
            ExitQuestion();
        }

        public void GetEmployeeQualificationMetadata()
        {
            Console.WriteLine("Employee Qualification Metadata är följande: ");
            GetOutput(cronusClient.GetEmployeeQualificationMetadata());
            ExitQuestion();
        }

        public void GetEmployeePortalSetupMetadata()
        {
            Console.WriteLine("Employee Portal Setup Metadata är följande: ");
            GetOutput(cronusClient.GetEmployeePortalSetupMetadata());
            ExitQuestion();
        }

        public void GetEmployeeStatisticsGroupMetadata()
        {
            Console.WriteLine("Employee Statistics Group Metadata är följande: ");
            GetOutput(cronusClient.GetEmployeeStatisticsGroupMetadata());
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
                try
                {
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