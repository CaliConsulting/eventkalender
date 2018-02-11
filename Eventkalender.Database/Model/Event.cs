using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eventkalender.Database.Model
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class Event
    {
        public Event()
        {

        }

        public Event(string name, string summary, DateTime startTime, DateTime endTime) : base()
        {
            Name = name;
            Summary = summary;
            StartTime = startTime;
            EndTime = endTime;
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [XmlIgnore]
        public virtual Nation Nation { get; set; }

        [XmlIgnore]
        public virtual List<Person> Persons { get; set; }
    }
}
