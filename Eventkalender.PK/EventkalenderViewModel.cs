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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ObservableCollection<Database.Event> Events
        {
            get
            {
                if (events == null)
                {
                    events = new ObservableCollection<Database.Event>(eventkalenderController.GetEvents());
                }
                return events;
            }
            set
            {
                if (events != value)
                {
                    events = value;

                    NotifyPropertyChanged("Events");
                }
            }
        }

        public ObservableCollection<Database.Nation> Nations
        {
            get
            {
                if (nations == null)
                {
                    nations = new ObservableCollection<Database.Nation>(eventkalenderController.GetNations());
                }
                return nations;
            }
            set
            {
                if (nations != value)
                {
                    nations = value;

                    NotifyPropertyChanged("Nations");
                }
            }
        }

        public ObservableCollection<Database.Person> Persons
        {
            get
            {
                if (persons != null)
                {
                    persons = new ObservableCollection<Database.Person>(eventkalenderController.GetPersons());
                }
                return persons;
            }
            set
            {
                if (persons != value)
                {
                    persons = value;

                    NotifyPropertyChanged("Persons");
                }
            }
        }
        public List<string> TimesList
        {
            get
            {
                if (timesList == null)
                {
                    timesList = Utility.GenerateList();
                }
                return timesList;
            }
            private set { }
        }

        public void AddNation(string name)
        {
            Database.Nation temp = new Database.Nation(name);

            Nations.Add(temp);
            eventkalenderDAL.AddNation(temp);

            //NotifyPropertyChanged("Nations");
        }

        public void DeleteNation(int id)
        {
            Database.Nation temp = new Database.Nation();
            temp.Id = id;

            Nations.Remove(Nations.FirstOrDefault(n => n.Id == temp.Id));
            eventkalenderDAL.DeleteNation(temp);

            //NotifyPropertyChanged("Nations");
        }
        
        public void AddPerson(string name, string lastname)
        {
            Database.Person p = new Database.Person(name, lastname);

            Persons.Add(p);
            eventkalenderDAL.AddPerson(p);

            //NotifyPropertyChanged("Persons");
        }

        public void DeletePerson(int id)
        {
            Database.Person temp = new Database.Person();
            temp.Id = id;

            Persons.Remove(Persons.FirstOrDefault(p => p.Id == temp.Id));
            eventkalenderDAL.DeletePerson(temp);

            //NotifyPropertyChanged("Persons");
        }

        public void AddEvent(string name, string summary, DateTime startTime, DateTime endTime, int nationId)
        {
            Database.Event e = new Database.Event(name, summary, startTime, endTime, nationId);

            Events.Add(e);
            eventkalenderDAL.AddEvent(e);

            //NotifyPropertyChanged("Events");
        }

        public void DeleteEvent(int id)
        {
            Database.Event temp = new Database.Event();
            temp.Id = id;

            Events.Remove(Events.FirstOrDefault(e => e.Id == temp.Id));
            eventkalenderDAL.DeleteEvent(temp);

            //NotifyPropertyChanged("Events");
        }

        public EventkalenderReference.Event [] GetEvents()
        {
            return eventkalenderWSClient.GetEvents();
        }

        public EventkalenderReference.Nation[] GetNations()
        {
            return eventkalenderWSClient.GetNations();
        }

        public EventkalenderReference.Person[] GetPersons()
        {
            return eventkalenderWSClient.GetPersons();
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

                    Metadata = Utility.GetCronusMetadata(cronusClient, metadataSelectedIndex);
                }
            }
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

        public List<string> WSCombobox
        {
            get
            {
                List<string> lst = new List<string>();
                lst.Add("Hämta Events");
                lst.Add("Hämta Nationer");
                lst.Add("Hämta Personer");
              
                return lst;
            }
            private set { }
        }

        private int wSSelectedIndex = -1;


        public int WSSelectedIndex
        {
            get
            {
                return wSSelectedIndex;
            }
            set
            {
                if (wSSelectedIndex != value)
                {
                    wSSelectedIndex = value;
                    NotifyPropertyChanged("WSSelectedItem");

                   // Data = Utility.(eventkalenderWSClient, wSSelectedIndex);
                }
            }
        }

    }
}
