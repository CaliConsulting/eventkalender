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
        public static bool CheckIfEmpty(params string[] values)
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
            for (int i = 0; i < lst[0].Count; i++)
            {
                DataGridTextColumn t = new DataGridTextColumn();
                t.Header = lst.First()[i];
                t.Binding = new Binding("[" + i + "]");

                grid.Columns.Add(t);
            }
        }


        private static List<List<string>> GetValuableInformation(CronusReference.DataTuple[] values)
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

        private static List<List<string>> DataTupleToNiceFormat(List<string> lst)
        {
            List<List<string>> newList = new List<List<string>>();

            //DataGridTextColumn t = new DataGridTextColumn();
            //t.Header = 0;
            //t.Binding = new Binding("[" + 0 + "]");

            //datagridCronus.Columns.Add(t);

            for (int i = 0; i < lst.Count; i++)
            {
                List<string> element = new List<string>();
                element.Add(lst.ElementAt(i));
                newList.Add(element);
            }

            return newList;
        }

        private static List<List<string>> DataTupleToNiceFormat(List<List<string>> lst)
        {
            //for (int i = 0; i < lst[0].Count; i++)
            //{
            //    DataGridTextColumn t = new DataGridTextColumn();
            //    t.Header = lst.First()[i];
            //    t.Binding = new Binding("[" + i + "]");

            //    datagridCronus.Columns.Add(t);
            //}
            lst.RemoveAt(0);

            return lst;
        }

        public static List<List<string>> GetCronusMetadata(CronusServiceSoapClient cronusClient, int index)
        {
            switch (index)
            {
                case 0:
                    return DataTupleToNiceFormat(cronusClient.GetIndexes());
                case 1:
                    return DataTupleToNiceFormat(cronusClient.GetKeys());
                case 2:
                    return DataTupleToNiceFormat(cronusClient.GetColumnsForEmployeeTable());
                case 3:
                    return DataTupleToNiceFormat(cronusClient.GetTableConstraints());
                case 4:
                    return DataTupleToNiceFormat(cronusClient.GetTables());
                case 5:
                    return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeeMetadata()));
                case 6:
                    return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeeAbsenceMetadata()));
                case 7:
                    return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeeRelativeMetadata()));
                case 8:
                    return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeeQualificationMetadata()));
                case 9:
                    return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeePortalSetupMetadata()));
                case 10:
                    return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeeStatisticsGroupMetadata()));
            }
            return null;
        }

        public static List<List<string>> GetCronusData(CronusServiceSoapClient cronusClient, int index)
        {
            List<List<string>> stringValues = new List<List<string>>();
            switch (index)
            {
                case 0:
                    CronusReference.DataTuple value = cronusClient.GetIllestPerson();
                    CronusReference.DataTuple[] values = new CronusReference.DataTuple[] { value };
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
                case 1:
                    values = cronusClient.GetIllPersonsByYear(2004, 2005); //statiskt anrop för 2004 och 2005 som efterfrågas
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
                case 2:
                    values = cronusClient.GetEmployeeAndRelatives();
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
                case 3:
                    values = cronusClient.GetEmployeeData();
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
                case 4:
                    values = cronusClient.GetEmployeeAbsenceData();
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
                case 5:
                    values = cronusClient.GetEmployeeRelativeData();
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
                case 6:
                    values = cronusClient.GetEmployeeQualificationData();
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
                case 7:
                    values = cronusClient.GetEmployeePortalSetupData();
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
                case 8:
                    values = cronusClient.GetEmployeeStatisticsGroupData();
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
            }
            return null;
        }


    }
}
