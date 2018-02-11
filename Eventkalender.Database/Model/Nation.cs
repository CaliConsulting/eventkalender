using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eventkalender.Database.Model
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class Nation
    {
        public Nation()
        {

        }

        public Nation(string name) : base()
        {
            Name = name;
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [XmlIgnore]
        public virtual List<Event> Events { get; set; }
    }
}
