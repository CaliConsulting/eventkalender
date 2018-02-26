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
    public class Nation
    {
        public Nation()
        {
            Events = new List<Event>();
        }

        public Nation(string name) : this()
        {
            Name = name;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public virtual List<Event> Events { get; set; }

        public override bool Equals(object obj)
        {
            Nation n = obj as Nation;
            if (n == null)
            {
                return false;
            }
            return Id == n.Id;
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
