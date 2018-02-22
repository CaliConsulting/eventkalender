using Eventkalender.WS.ConsoleApp.CronusReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.WS.ConsoleApp.Utility
{
    public class ConversionUtility
    {
        public static string ToString(DataTuple tuple)
        {
            return ToString(new DataTuple[] { tuple });
        }

        public static string ToString(DataTuple[] tuples)
        {
            int[] longestColumnLength = null;

            bool isFirst = true;
            List<List<string>> totals = new List<List<string>>();
            for (int i = 0; i < tuples.Length; i++)
            {
                CronusReference.DataTuple t = tuples[i];

                if (longestColumnLength == null)
                {
                    longestColumnLength = new int[t.Count];
                }

                List<string> array = new List<string>();
                List<string> columns = new List<string>();

                if (isFirst)
                {
                    totals.Add(columns);
                    isFirst = false;
                }
                for (int j = 0; j < t.Count; j++)
                {
                    SerializableKeyValuePairOfStringString s = t.ElementAt(j);

                    // Figure out max column length
                    if (s.Value != null)
                    {
                        longestColumnLength[j] = Math.Max(s.Value.Length, longestColumnLength[j]);
                    }
                    columns.Add(s.Key);
                    array.Add(s.Value);
                }
                totals.Add(array);
            }

            // Is the column name the longest value in the column?
            for (int i = 0; i < totals[0].Count; i++)
            {
                string column = totals[0][i];
                if (column != null)
                {
                    longestColumnLength[i] = Math.Max(column.Length, longestColumnLength[i]);
                }
            }
            return GenerateOutputWithColumns(totals, longestColumnLength);
        }

        private static string GenerateOutputWithColumns(List<List<string>> totals, int[] longestColumnLength)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < totals.Count; i++)
            {
                if (i == 1)
                {
                    // We are on the column row; let's generate the columns
                    int currentPos = 0;
                    int currentIndex = longestColumnLength[currentPos];

                    builder.Append("|");

                    int totalColumnLength = longestColumnLength.Sum();
                    int totalOutputLength = totalColumnLength + (longestColumnLength.Length * 2);
                    for (int j = 0; j < totalOutputLength; j++)
                    {
                        builder.Append("-");
                        // Are we on the index that represents the beginning of a new column?
                        if (j == currentIndex + 1)
                        {
                            builder.Append("|");
                            currentPos++;
                            currentIndex += longestColumnLength[Math.Min(currentPos, longestColumnLength.Length - 1)] + 2;
                        }
                    }
                    builder.Append("\n");
                }
                List<string> innerList = totals[i];
                for (int j = 0; j < innerList.Count; j++)
                {
                    builder.Append("| ");
                    string element = innerList[j];
                    builder.Append(element);
                    for (int k = 0; k < longestColumnLength[j] - element.Length; k++)
                    {
                        builder.Append(" ");
                    }
                    builder.Append(" ");
                }
                builder.Append("|");
                if (i != totals.Count - 1)
                {
                    builder.Append("\n");
                }
            }
            return builder.ToString();
        }

    }
}
