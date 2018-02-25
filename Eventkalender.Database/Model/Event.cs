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

            // Creating a new nation here breaks the Entity Framework join process, so DO NOT instantiate it
            //Nation = new Nation();
        }

        public Event(string name, string summary, DateTime startTime, DateTime endTime) : this()
        {
            Name = name;
            Summary = summary;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Event(string name, string summary, DateTime startTime, DateTime endTime, int nationId) : this(name, summary, startTime, endTime)
        {
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

        public override bool Equals(object obj)
        {
            Event e = obj as Event;
            if (e == null)
            {
                return false;
            }
            return Id == e.Id;/* && string.Equals(Name, e.Name) && string.Equals(Summary, e.Summary) 
                && DateTime.Equals(StartTime, e.StartTime) && DateTime.Equals(EndTime, e.EndTime) 
                && NationId == e.NationId;*/
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int hash = 7;
            hash = prime * hash + Id;
            //hash = prime * hash + (Name == null ? 0 : Name.GetHashCode());
            //hash = prime * hash + (Summary == null ? 0 : Summary.GetHashCode());
            //hash = prime * hash + (StartTime == null ? 0 : StartTime.GetHashCode());
            //hash = prime * hash + (EndTime == null ? 0 : EndTime.GetHashCode());
            //hash = prime * hash + NationId;
            //hash = prime * hash + base.GetHashCode();
            return hash;
        }

    }
}
