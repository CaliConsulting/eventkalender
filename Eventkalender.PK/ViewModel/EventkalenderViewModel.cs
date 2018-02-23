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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;

namespace Eventkalender.PK
{
    public class EventkalenderViewModel : INotifyPropertyChanged
    {
        private List<string> timesList;

        private ObservableCollection<Database.Event> events;
        private ObservableCollection<Database.Nation> nations;
        private ObservableCollection<Database.Person> persons;
        private List<CronusReference.Employee> employees;

        private CronusServiceSoapClient cronusClient;
        private EventkalenderServiceSoapClient eventkalenderClient;
        private EventkalenderDAL eventkalenderDAL;

        public event PropertyChangedEventHandler PropertyChanged;

        public EventkalenderViewModel()
        {
            cronusClient = new CronusServiceSoapClient();
            eventkalenderClient = new EventkalenderServiceSoapClient();
            eventkalenderDAL = new EventkalenderDAL("Resources/eventkalender-db.xml");
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
                    events = new ObservableCollection<Database.Event>(eventkalenderDAL.GetEvents());
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
                    nations = new ObservableCollection<Database.Nation>(eventkalenderDAL.GetNations());
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
                if (persons == null)
                {
                    persons = new ObservableCollection<Database.Person>(eventkalenderDAL.GetPersons());
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

        public List<CronusReference.Employee> Employees
        {
            get
            {
                if (employees == null)
                {
                    employees = new List<CronusReference.Employee>(cronusClient.GetEmployees());
                }
                return employees;

            }
            set
            {
                if (employees != value)
                {
                    employees = value;

                    NotifyPropertyChanged("Employees");
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

        public void DeleteEmployee(string no)
        {
            CronusReference.Employee temp = new CronusReference.Employee();
            temp.No = no;

            Employees.Remove(Employees.FirstOrDefault(e => e.No == temp.No));
            cronusClient.DeleteEmployee(temp.No);
        }

        public void UpdateEmployee(string no, string firstName, string lastName)
        {

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
        public void InviteToEvent(IList list, Database.Event ev)
        {
            foreach (Database.Person p in list)
            {
                p.Events.Add(ev);
                eventkalenderDAL.AddPerson(p);

            }
        }

        public EventkalenderReference.Event[] GetEvents()
        {
            return eventkalenderClient.GetEvents();
        }

        public EventkalenderReference.Nation[] GetNations()
        {
            return eventkalenderClient.GetNations();
        }

        public EventkalenderReference.Person[] GetPersons()
        {
            return eventkalenderClient.GetPersons();
        }

        public void EventGridWrapAutoSize(DataGrid dgWebService)
        {
            ClearColumns(dgWebService);
            var col = new DataGridTextColumn();
            col.Header = "Id";
            col.Binding = new Binding("Id");
            var col1 = new DataGridTextColumn();
            col1.Header = "Namn";
            col1.Binding = new Binding("Name");
            var col2 = new DataGridTextColumn();
            col2.Header = "Evenemangsvärd";
            col2.Binding = new Binding("Nation.Name");
            var col3 = new DataGridTextColumn();
            col3.Header = "Starttid";
            col3.Binding = new Binding("StartTime");
            var col4 = new DataGridTextColumn();
            col4.Header = "Sluttid";
            col4.Binding = new Binding("EndTime");
            var col5 = new DataGridTextColumn();
            col5.Header = "Beskrivning";
            col5.Binding = new Binding("Summary");

            dgWebService.Columns.Add(col);
            dgWebService.Columns.Add(col1);
            dgWebService.Columns.Add(col2);
            dgWebService.Columns.Add(col3);
            dgWebService.Columns.Add(col4);
            dgWebService.Columns.Add(col5);

            for (int i = 5; i < dgWebService.Columns.Count; i++)
            {
                dgWebService.Columns[i].Width = 0;
                dgWebService.UpdateLayout();
                dgWebService.Columns[i].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }      
                WrapColumn(col5);              
        }

        public void NationGridWrapAutoSize(DataGrid dgWebService)
        {
            ClearColumns(dgWebService);
            var col = new DataGridTextColumn();
            col.Header = "Id";
            col.Binding = new Binding("Id");
            var col1 = new DataGridTextColumn();
            col1.Header = "Namn";
            col1.Binding = new Binding("Name");

            dgWebService.Columns.Add(col);
            dgWebService.Columns.Add(col1);

            for (int i = 0; i < dgWebService.Columns.Count; i++)
            {
                dgWebService.Columns[i].Width = 0;
                dgWebService.UpdateLayout();
                dgWebService.Columns[i].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        public void PersonGridWrapAutoSize(DataGrid dgWebService)
        {
            ClearColumns(dgWebService);
            var col = new DataGridTextColumn();
            col.Header = "Id";
            col.Binding = new Binding("Id");
            var col1 = new DataGridTextColumn();
            col1.Header = "Förnamn";
            col1.Binding = new Binding("FirstName");
            var col2 = new DataGridTextColumn();
            col2.Header = "Efternamn";
            col2.Binding = new Binding("LastName");

            dgWebService.Columns.Add(col);
            dgWebService.Columns.Add(col1);
            dgWebService.Columns.Add(col2);

            for (int i = 0; i < dgWebService.Columns.Count; i++)
            {
                dgWebService.Columns[i].Width = 0;
                dgWebService.UpdateLayout();
                dgWebService.Columns[i].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private static void WrapColumn(DataGridTextColumn col) //DataGridTextColumn col
        {
             var style = new Style(typeof(TextBlock));
             style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
             style.Setters.Add(new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center));
             col.ElementStyle = style;    
        }

        private static void ClearColumns(DataGrid dataGrid)
        {
            dataGrid.Columns.Clear();
            dataGrid.ItemsSource = null;
        }

        public string GetFile(string path)
        {
            return eventkalenderClient.GetFile(path);
        }
        public List<string> GetFiles()
        {
            return eventkalenderClient.GetFiles();
        }

    }
}
