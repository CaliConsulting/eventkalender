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
        public Nation()
        {

        }

        public Nation(string name) : base()
        {
            Name = name;
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
