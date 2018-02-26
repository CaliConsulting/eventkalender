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
    public class Person
    {
        public Person()
        {
            Events = new List<Event>();
        }

        public Person(string firstName, string lastName) : this()
        {
            FirstName = firstName;
            LastName = lastName;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [NotMapped]
        [XmlIgnore]
        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
            private set { }
        }
        
        public virtual List<Event> Events { get; set; }

        public override bool Equals(object obj)
        {
            Person p = obj as Person;
            if (p == null)
            {
                return false;
            }
            return Id == p.Id;
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
