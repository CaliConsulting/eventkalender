using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database.Model
{
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

        public virtual Nation Nation { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
    }
}
