using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.Database.Model
{
    public class Nation
    {
        private string name;

        private List<Event> events;

        public Nation()
        {

        }

        public string Name { get; set; }
        public List<Event> Events { get; set; }

    }
}
