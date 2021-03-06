﻿using Eventkalender.Database;
using Eventkalender.PK.CronusReference;
using Eventkalender.PK.EventkalenderReference;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Eventkalender.PK
{
    public class EventkalenderViewModel : INotifyPropertyChanged
    {
        private List<string> timesList;

        private ObservableCollection<string> files;
        private ObservableCollection<Database.Event> events;
        private ObservableCollection<Database.Nation> nations;
        private ObservableCollection<Database.Person> persons;
        private ObservableCollection<CronusReference.Employee> employees;

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
        public ObservableCollection<string> Files
        {
            get
            {
                if(files == null)
                {
                    files = new ObservableCollection<string>(GetFiles());
                }
                return files;
            }
            set
            {
                if (files != value)
                {
                    files = value;

                    NotifyPropertyChanged("Files");
                }
            }
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

        public ObservableCollection<CronusReference.Employee> Employees
        {
            get
            {
                if (employees == null)
                {
                    employees = new ObservableCollection<CronusReference.Employee>(cronusClient.GetEmployees());
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
            try
            {
                Database.Nation temp = new Database.Nation(name);
                Nations.Add(temp);
                eventkalenderDAL.AddNation(temp);
                //NotifyPropertyChanged("Nations");
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }

        public void DeleteNation(int id)
        {
            try
            {
                Database.Nation temp = new Database.Nation();
                temp.Id = id;

                Nations.Remove(Nations.FirstOrDefault(n => n.Id == temp.Id));
                eventkalenderDAL.DeleteNation(temp);
                Events = new ObservableCollection<Database.Event>(eventkalenderDAL.GetEvents());
                //NotifyPropertyChanged("Nations");
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }

        public void AddPerson(string name, string lastname)
        {
            try
            {
                Database.Person p = new Database.Person(name, lastname);

                Persons.Add(p);
                eventkalenderDAL.AddPerson(p);

                //NotifyPropertyChanged("Persons");
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                Database.Person temp = new Database.Person();
                temp.Id = id;

                Persons.Remove(Persons.FirstOrDefault(p => p.Id == temp.Id));
                eventkalenderDAL.DeletePerson(temp);

                Events = new ObservableCollection<Database.Event>(eventkalenderDAL.GetEvents());
                //NotifyPropertyChanged("Persons");
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }    
        }

        public void AddEvent(string name, string summary, DateTime startTime, DateTime endTime, int nationId)
        {
            try
            {
                Database.Event e = new Database.Event(name, summary, startTime, endTime, nationId);

                Events.Add(e);
                Database.Nation n1 = Nations.First(temp => temp.Id == nationId);
                n1.Events.Add(e);

                eventkalenderDAL.AddEvent(e);

                NotifyPropertyChanged("Nations");
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }

        public void DeleteEvent(int id)
        {
            try
            {
                Database.Event temp = new Database.Event();
                temp.Id = id;

                Events.Remove(Events.FirstOrDefault(e => e.Id == temp.Id));
                eventkalenderDAL.DeleteEvent(temp);

                Persons = new ObservableCollection<Database.Person>(eventkalenderDAL.GetPersons());
                //NotifyPropertyChanged("Events");
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }

        public void DeleteEmployee(string no)
        {
            try
            {
                CronusReference.Employee temp = new CronusReference.Employee();
                temp.No = no;

                Employees.Remove(Employees.FirstOrDefault(e => e.No == temp.No));
                cronusClient.DeleteEmployee(temp.No);
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }

        public List<CronusReference.Employee> GetEmployees()
        {
            try
            {
                return cronusClient.GetEmployees().ToList();
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
            return new List<CronusReference.Employee>();
        }

        public void UpdateEmployee(string no, string firstName, string lastName, int index)
        {
            try
            {
                CronusReference.Employee emp = new CronusReference.Employee();
                emp.No = no;
                emp.FirstName = firstName;
                emp.LastName = lastName;
                index = Employees.IndexOf(Employees.Where(e => e.No == no).FirstOrDefault());
                Employees.ElementAt(index).FirstName = emp.FirstName;
                Employees.ElementAt(index).LastName = emp.LastName;
                cronusClient.UpdateEmployee(no, firstName, lastName);
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }

        public void AddEmployee(string no, string firstName, string lastName)
        {
            try
            {
                CronusReference.Employee emp = new CronusReference.Employee();
                emp.No = no;
                emp.FirstName = firstName;
                emp.LastName = lastName;
                Employees.Add(emp);
                cronusClient.AddEmployee(no, firstName, lastName);
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }

        public string GetFile(string path)
        {
            return eventkalenderClient.GetFile(path);
        }

        public List<string> GetFiles()
        {
            try
            {
                return eventkalenderClient.GetFiles();
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
            return new List<string>();
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
                lst.Add("Evenemang");
                lst.Add("Nationer");
                lst.Add("Personer");

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

        public string status = "";
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                NotifyPropertyChanged("Status");
            }
        }

        public void InviteToEvent(IList list, Database.Event ev)
        {
            try
            {
                foreach (Database.Person p in list)
                {
                    if (!p.Events.Contains(ev))
                    {
                        p.Events.Add(ev);
                        eventkalenderDAL.UpdatePerson(p);
                    }
                }
                Events = new ObservableCollection<Database.Event>(eventkalenderDAL.GetEvents());
                Persons = new ObservableCollection<Database.Person>(eventkalenderDAL.GetPersons());
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
        }

        public EventkalenderReference.Event[] GetEvents()
        {
            try
            {
                return eventkalenderClient.GetEvents();
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
            return new EventkalenderReference.Event[] { };
        }

        public EventkalenderReference.Nation[] GetNations()
        {
            try
            {
                return eventkalenderClient.GetNations();
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
            return new EventkalenderReference.Nation[] { };
        }

        public EventkalenderReference.Person[] GetPersons()
        {
            try
            {
                return eventkalenderClient.GetPersons();
            }
            catch (Exception ex)
            {
                Status = ExceptionHandler.GetErrorMessage(ex);
            }
            return new EventkalenderReference.Person[] { };
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
            Autosize(dgWebService);
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
            Autosize(dgWebService);
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

        public void Autosize(DataGrid grid)
        {
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                grid.Columns[i].Width = 0;
                grid.UpdateLayout();
                grid.Columns[i].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }
        public void UpdateDatabase()
        {
            Events = new ObservableCollection<Database.Event>(eventkalenderDAL.GetEvents());
            Persons = new ObservableCollection<Database.Person>(eventkalenderDAL.GetPersons());
            Nations = new ObservableCollection<Database.Nation>(eventkalenderDAL.GetNations());
        }
    }
}