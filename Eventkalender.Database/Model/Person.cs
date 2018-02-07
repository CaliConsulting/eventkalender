using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database.Model
{
    public class Person
    {
        private string firstname;
        private string lastname;

        private List<Event> events;

        public Person()
        {

        }

        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public List<Event> Events { get; set; }

    }
}
