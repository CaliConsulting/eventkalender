using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
            return Id == e.Id;
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int hash = 7;
            hash = prime * hash + Id;
            return hash;
        }
    }
}