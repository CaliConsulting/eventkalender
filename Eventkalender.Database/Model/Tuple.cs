using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eventkalender.Database.Model
{
    [Serializable]
    public class Tuple
    {
        //[XmlAttribute]
        private List<SerializableKeyValuePair<string, string>> values;

        public Tuple()
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
    public class SerializableKeyValuePair<TKey, TValue>
    {
        public SerializableKeyValuePair()
        {

        }

        public SerializableKeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public TKey Key { get; set; }
        public TValue Value { get; set; }

    }
}
