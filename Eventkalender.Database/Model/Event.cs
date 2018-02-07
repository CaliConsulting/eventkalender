using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database.Model
{
    [Table("NationEvent")]
    public class Event
    {
        //private int id;
        //private string name;
        //private string summary;

        //private DateTime startTime;
        //private DateTime endTime;

        //private List<Person> persons;

        public Event()
        {

        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public List<Person> Persons { get; set; }
    }
}
