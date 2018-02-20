using System;
using System.Collections;
using Eventkalender.Database;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Eventkalender.PK.CronusReference;
using Eventkalender.PK.EventkalenderReference;

namespace Eventkalender.PK.GUI
{
    public class EventkalenderViewModel : INotifyPropertyChanged
    {

        private EventkalenderController eventkalenderController;
        private List<string> timesList;

        private ObservableCollection<Database.Event> events;
        private ObservableCollection<Database.Nation> nations;
        private ObservableCollection<Database.Person> persons;

        private EventkalenderDAL eventkalenderDAL;
        private CronusServiceSoapClient cronusClient;
        public event PropertyChangedEventHandler PropertyChanged;
        private EventkalenderServiceSoapClient eventkalenderWSClient;

        public EventkalenderViewModel()
        {
            cronusClient = new CronusServiceSoapClient();
            eventkalenderController = new EventkalenderController("Resources/eventkalender-db.xml");
            eventkalenderDAL = new EventkalenderDAL("Resources/eventkalender-db.xml");
            eventkalenderWSClient = new EventkalenderServiceSoapClient();
        }


        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

       /* protected void OnPropertyChanged(ObservableCollection<object> lst)
        {
            
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }*/


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
            set
            {
            }
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
            set
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Nations"));
                }
            }
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
            set
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Persons"));
                }
            }
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

        public void AddNation(string name)
        {
            Database.Nation n = new Database.Nation(name);

            Nations.Add(n);
            eventkalenderDAL.AddNation(n);
        }

        public void DeleteNation(int id)
        {
            Database.Nation n = new Database.Nation("");
            n.Id = id;

            Nations.Remove(n);
            eventkalenderDAL.DeleteNation(n);
        }
        
        public void AddPerson(string name, string lastname)
        {
            Database.Person p = new Database.Person(name, lastname);

            Persons.Add(p);
            eventkalenderDAL.AddPerson(p);
        }

        public void DeletePerson(int id)
        {
            Database.Person p = new Database.Person("","");
            p.Id = id;

            Persons.Remove(p);
            eventkalenderDAL.DeletePerson(p);
        }

        public void AddEvent(string name, string summary, DateTime startTime, DateTime endTime, int nationId)
        {
            Database.Event e = new Database.Event(name, summary, startTime, endTime, nationId);

            Events.Add(e);
            eventkalenderDAL.AddEvent(e);
        }

        public List<string> DataCombobox

        {
            get
            {
                List<string> lst = new List<string>();
                lst.Add("Hämta personen som har varit sjuk flest antal gånger");
                lst.Add("Hämta sjuka personer mellan åren 2004 och 2005");
                lst.Add("Hämta anställda och deras anhöriga");
                lst.Add("Hämta personaldata");
                lst.Add("Hämta personal frånvarodata");
                lst.Add("Hämta personal anhörigdata");
                lst.Add("Hämta personal kompetensdata");
                lst.Add("Hämta personal portalsetupdata");
                lst.Add("Hämta personal statisticsgroupdata");


                return lst;
            }
            private set { }
        }

        private List<List<string>> data;
        public List<List<string>> Data
        {
            get
            {
                return data;
            }
            set
            {
                if (data != value)
                {
                    data = value;
                    NotifyPropertyChanged("Data");
                }
            }
        }

        private List<List<string>> metadata;
        public List<List<string>> Metadata
        {
            get
            {
                return metadata;
            }
            set
            {
                if (metadata != value)
                {
                    metadata = value;
                    NotifyPropertyChanged("Metadata");
                }
            }
        }

        public List<string> MetadataCombobox
        {
            get
            {
                List<string> lst = new List<string>();
                lst.Add("Hämta indexes");
                lst.Add("Hämta nycklar");
                lst.Add("Hämta kolumner för personal tabell");
                lst.Add("Hämta tabellbegränsningar");
                lst.Add("Hämta tabeller");
                lst.Add("Hämta personal metadata");
                lst.Add("Hämta personal frånvarometadata");
                lst.Add("Hämta personal anhörigmetadata");
                lst.Add("Hämta personal kompetensmetadata");
                lst.Add("Hämta personal portalsetupmetadata");
                lst.Add("Hämta personal statisticsgroupmetadata");
                return lst;
            }
            private set { }
        }

        private int dataSelectedIndex = -1;
        public int DataSelectedIndex
        {
            get
            {
                return dataSelectedIndex;
            }
            set
            {
                if (dataSelectedIndex != value)
                {
                    dataSelectedIndex = value;
                    NotifyPropertyChanged("DataSelectedItem");

                    Data = Utility.GetCronusData(cronusClient, dataSelectedIndex);
                }
            }
        }

        private int metadataSelectedIndex = -1;
        public int MetadataSelectedIndex
        {
            get
            {
                return metadataSelectedIndex;
            }
            set
            {
                if (metadataSelectedIndex != value)
                {
                    metadataSelectedIndex = value;
                    NotifyPropertyChanged("MetadataSelectedIndex");

                    Data = Utility.GetCronusMetadata(cronusClient, metadataSelectedIndex);
                }
            }
        }

    }
}
