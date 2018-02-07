using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database.Model
{
    public class Person
    {
        private string firstName;
        private string lastName;

        private List<Event> events;

        public Person()
        {

        }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public List<Event> Events { get; set; }

    }
}
