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
    }
}
