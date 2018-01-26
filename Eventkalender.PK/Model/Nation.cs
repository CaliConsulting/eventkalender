using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Model
{
    public class Nation
    {
        private string name;

        private List<Event> events;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<Event> Events
        {
            get { return events; }
            set { events = value; }
        }

    }
}
