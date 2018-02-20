using System;
using System.Collections;
using Eventkalender.Database;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventkalender.PK.GUI

{
    class EventkalenderViewModel
    {
        private EventkalenderController eventkalenderController = new EventkalenderController("Resources/eventkalender-db.xml");
        private List<string> timesList;
        private ObservableCollection<Database.Event> events;
        private ObservableCollection<Database.Nation> nations;
        private ObservableCollection<Database.Person> persons;

        public ObservableCollection<Database.Event> Events
        {
            get
            {
                if (events == null)
                {
                    return events = new ObservableCollection<Database.Event>(eventkalenderController.GetEvents());
                }
                return events;
            }
            set { }
        }

        public ObservableCollection<Database.Nation> Nations
        {
            get
            {
                if (nations == null)
                {
                    return nations = new ObservableCollection<Database.Nation>(eventkalenderController.GetNations());
                }
                return nations;
            }
            set { }
        }

        public ObservableCollection<Database.Person> Persons
        {
            get
            {
                if (persons == null)
                {
                    return persons = new ObservableCollection<Database.Person>(eventkalenderController.GetPersons());
                }
                return persons;
            }
            set { }
        }
        public List<string> TimesList
        {
            set
            { }
            get
            {
                if (timesList == null)
                {
                    return timesList = Utility.GenerateList();
                }
                return timesList;
            }
        }
    }
}
