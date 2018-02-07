using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database.Model
{
    public class Nation
    {
        //private int id;
        //private string name;

        //private List<Event> events;

        public Nation()
        {

        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Event> Events { get; set; }
    }
}
