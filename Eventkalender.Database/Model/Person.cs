using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database.Model
{
    public class Person
    {
        //private int id;
        //private string firstName;
        //private string lastName;

        //private List<Event> events;

        public Person()
        {

        }

        [Key]
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
            private set { }
        }

        public List<Event> Events { get; set; }
    }
}
