using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eventkalender.Database.Model
{
    public class Person
    {
        public Person()
        {

        }

        public Person(string firstName, string lastName) : base()
        {
            FirstName = firstName;
            LastName = lastName;
        }

        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        [XmlIgnore]
        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
            private set { }
        }

        public virtual List<Event> Events { get; set; }
    }
}
