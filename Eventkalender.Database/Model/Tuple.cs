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
        [XmlAttribute]
        private Dictionary<string, string> values;

        public Tuple()
        {
            values = new Dictionary<string, string>();
        }

        public void Add(string column, string value)
        {
            values.Add(column, value);
        }

        [XmlIgnore]
        public Dictionary<string, string>.KeyCollection Columns
        {
            get { return values.Keys; }
            private set { }
        }

        [XmlIgnore]
        public Dictionary<string, string>.ValueCollection Values
        {
            get { return values.Values; }
            private set { }
        }
    }
}
