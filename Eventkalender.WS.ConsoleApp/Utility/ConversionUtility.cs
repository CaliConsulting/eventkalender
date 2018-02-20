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

        public string ToString(DataTuple[] tuples)
        {
            StringBuilder builder = new StringBuilder();

            bool isFirst = true;

            for (int i = 0; i < tuples.Length; i++)
            {
                DataTuple tuple = tuples[i];

                for (int j = 0; j < tuple.Count; j++)
                {
                    SerializableKeyValuePairOfStringString s = tuple.ElementAt(j);

                    string key = s.Key;
                    string value = s.Value;

                    if (isFirst)
                    {
                        builder.Append(key);

                        isFirst = false;
                    }
                }

            return builder.ToString();
        }

    }
}
