using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eventkalender.Database
{
    [Serializable]
    public class DataTuple
    {
        //[XmlAttribute]
        private List<SerializableKeyValuePair<string, string>> values;

        public DataTuple()
        {
            values = new List<SerializableKeyValuePair<string, string>>();
        }

        public void Add(string column, string value)
        {
            SerializableKeyValuePair<string, string> pair = new SerializableKeyValuePair<string, string>(column, value);
            values.Add(pair);
        }

        [XmlElement("Entry")]
        public List<SerializableKeyValuePair<string, string>> Values
        {
            get { return values; }
            set { values = value; }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (SerializableKeyValuePair<string, string> pair in values)
            {
                string key = pair.Key;
                string value = pair.Value;
                builder.Append(key + ": " + value);
                builder.AppendLine();
            }
            return builder.ToString();
        }
        //[XmlIgnore]
        //public Dictionary<string, string>.KeyCollection Columns
        //{
        //    get { return values.Keys; }
        //    private set { }
        //}

        //[XmlIgnore]
        //public Dictionary<string, string>.ValueCollection Values
        //{
        //    get { return values.Values; }
        //    private set { }
        //}
    }

    [Serializable]
    public class SerializableKeyValuePair<K, V>
    {
        public SerializableKeyValuePair()
        {

        }

        public SerializableKeyValuePair(K key, V value)
        {
            Key = key;
            Value = value;
        }

        public K Key { get; set; }

        public V Value { get; set; }
    }
}
