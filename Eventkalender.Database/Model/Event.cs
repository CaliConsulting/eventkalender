using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eventkalender.Database
{
    [Serializable]
    public class Event
    {
        public Event()
        {
            Persons = new List<Person>();
        }

        public Event(string name, string summary, DateTime startTime, DateTime endTime, int nationId) : base()
        {
            Name = name;
            Summary = summary;
            StartTime = startTime;
            EndTime = endTime;
            NationId = nationId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int NationId { get; set; }
        
        public virtual Nation Nation { get; set; }

        public virtual List<Person> Persons { get; set; }
    }
}
