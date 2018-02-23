using Eventkalender.PK.CronusReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Eventkalender.PK
{
    public static class Utility
    {
        public static bool IsNotEmpty(params string[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == null || values[i].Equals(""))
                {
                    return false;
                }
            }
            return true;
        }

        public static List<string> GenerateList()
        {
            List<string> times = new List<string>();
            for (int i = 0; i < 48; i++)
            {
                int hour = (int)Math.Floor(i / 2d);
                string strHour = hour.ToString();
                if (hour < 10)
                {
                    strHour = "0" + strHour;
                }
                string res = strHour + ":";
                if (i % 2 == 1)
                {
                    res += "3";
                }
                else
                {
                    res += "0";
                }
                res += "0";
                times.Add(res);
            }
            return times;
        }

        public static DateTime ToDate(string date, string time)
        {
            DateTime dateStart = Convert.ToDateTime(date + "T" + time);
            return dateStart;
        }

        public static void AddColumns(DataGrid grid, List<List<string>> lst)
        {
            if (lst != null)
            {
                for (int i = 0; i < lst[0].Count; i++)
                {
                    DataGridTextColumn t = new DataGridTextColumn();
                    t.Header = lst.First()[i];
                    t.Binding = new Binding("[" + i + "]");

                    grid.Columns.Add(t);
                }
                // Remove the column header from the resulting list
                lst.RemoveAt(0);
            }
        }

        private static List<List<string>> ExtractData(CronusReference.DataTuple[] values)
        {
            bool isFirst = true;
            List<List<string>> totals = new List<List<string>>();
            for (int i = 0; i < values.Length; i++)
            {
                CronusReference.DataTuple t = values[i];

                List<string> array2 = new List<string>();
                List<string> columns2 = new List<string>();

                //string[] array = new string[t.Count];
                //string[] columns = new string[t.Count];

                if (isFirst)
                {
                    totals.Add(columns2);
                    isFirst = false;
                }
                for (int j = 0; j < t.Count; j++)
                {
                    SerializableKeyValuePairOfStringString s = t.ElementAt(j);
                    columns2.Add(s.Key);
                    array2.Add(s.Value);
                }
                totals.Add(array2);
            }
            return totals;
        }

        private static List<List<string>> NormalizeStructure(List<string> lst)
        {
            List<List<string>> newList = new List<List<string>>();
            for (int i = 0; i < lst.Count; i++)
            {
                List<string> element = new List<string>();
                element.Add(lst.ElementAt(i));
                newList.Add(element);
            }
            return newList;
        }

        public static List<List<string>> GetCronusMetadata(CronusServiceSoapClient cronusClient, int index)
        {
            List<List<string>> result = new List<List<string>>();
            switch (index)
            {
                case 0:
                    result = NormalizeStructure(cronusClient.GetIndexes());
                    return result;
                case 1:
                    result = NormalizeStructure(cronusClient.GetKeys());
                    return result;
                case 2:
                    result = NormalizeStructure(cronusClient.GetColumnsForEmployeeTable());
                    return result;
                case 3:
                    result = NormalizeStructure(cronusClient.GetTableConstraints());
                    return result;
                case 4:
                    result = NormalizeStructure(cronusClient.GetTables());
                    return result;
                case 5:
                    result = ExtractData(cronusClient.GetEmployeeMetadata());
                    return result;
                case 6:
                    result = ExtractData(cronusClient.GetEmployeeAbsenceMetadata());
                    return result;
                case 7:
                    result = ExtractData(cronusClient.GetEmployeeRelativeMetadata());
                    return result;
                case 8:
                    result = ExtractData(cronusClient.GetEmployeeQualificationMetadata());
                    return result;
                case 9:
                    result = ExtractData(cronusClient.GetEmployeePortalSetupMetadata());
                    return result;
                case 10:
                    result = ExtractData(cronusClient.GetEmployeeStatisticsGroupMetadata());
                    return result;
            }
            // Down here we are in undefined behavior land...
            return null;
        }

        public static List<List<string>> GetCronusData(CronusServiceSoapClient cronusClient, int index)
        {
            List<List<string>> stringValues = new List<List<string>>();
            switch (index)
            {
               
                case 0:
                    CronusReference.DataTuple[] values = new CronusReference.DataTuple[] { cronusClient.GetIllestPerson() };
                    return ExtractData(values);
                case 1:
                    values = cronusClient.GetIllPersonsByYear(2004, 2005); //statiskt anrop för 2004 och 2005 som efterfrågas
                    return ExtractData(values);
                case 2:
                    values = cronusClient.GetEmployeeAndRelatives();
                    return ExtractData(values);
                case 3:
                    values = cronusClient.GetEmployeeData();
                    return ExtractData(values);
                case 4:
                    values = cronusClient.GetEmployeeAbsenceData();
                    return ExtractData(values);
                case 5:
                    values = cronusClient.GetEmployeeRelativeData();
                    return ExtractData(values);
                case 6:
                    values = cronusClient.GetEmployeeQualificationData();
                    return ExtractData(values);
                case 7:
                    values = cronusClient.GetEmployeePortalSetupData();
                    return ExtractData(values);
                case 8:
                    values = cronusClient.GetEmployeeStatisticsGroupData();
                    return ExtractData(values);
            }
            return null;
        }
        
    }
}
