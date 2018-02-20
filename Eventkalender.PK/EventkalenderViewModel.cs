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

        public List<List<string>> GetValuableInformation(CronusReference.DataTuple[] values)
        {
            bool isFirst = true;
            List<List<string>> totals = new List<List<string>>();
            for (int i = 0; i < values.Length; i++)
            {
                CronusReference.DataTuple t = values[i];

                List<string> array2 = new List<string>();
                List<string> columns2 = new List<string>();

                //string[] array = new string[t.Count];
                //string[] columns = new string[t.Count];

                if (isFirst)
                {
                    totals.Add(columns2);
                    isFirst = false;
                }
                for (int j = 0; j < t.Count; j++)
                {
                    SerializableKeyValuePairOfStringString s = t.ElementAt(j);
                    columns2.Add(s.Key);
                    array2.Add(s.Value);
                }
                totals.Add(array2);
            }
            return totals;
        }

        public List<List<string>> DataTupleToNiceFormat(List<string> lst)
        {
            List<List<string>> newList = new List<List<string>>();

            //DataGridTextColumn t = new DataGridTextColumn();
            //t.Header = 0;
            //t.Binding = new Binding("[" + 0 + "]");

            //datagridCronus.Columns.Add(t);

            for (int i = 0; i < lst.Count; i++)
            {
                List<string> element = new List<string>();
                element.Add(lst.ElementAt(i));
                newList.Add(element);
            }

            return newList;
        }

        public List<List<string>> DataTupleToNiceFormat(List<List<string>> lst)
        {
            //for (int i = 0; i < lst[0].Count; i++)
            //{
            //    DataGridTextColumn t = new DataGridTextColumn();
            //    t.Header = lst.First()[i];
            //    t.Binding = new Binding("[" + i + "]");

            //    datagridCronus.Columns.Add(t);
            //}
            lst.RemoveAt(0);

            return lst;
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

        //public List<List<string>> GetMetadata
        //{
        //    get
        //    {
        //        switch (int id /*cmbMetaData.SelectedIndex*/)
        //        {
        //            case 0:
        //                return DataTupleToNiceFormat(cronusClient.GetIndexes());
        //            case 1:
        //                return DataTupleToNiceFormat(cronusClient.GetKeys());
        //            case 2:
        //                return DataTupleToNiceFormat(cronusClient.GetColumnsForEmployeeTable());
        //            case 3:
        //                return DataTupleToNiceFormat(cronusClient.GetTableConstraints());
        //            case 4:
        //                return DataTupleToNiceFormat(cronusClient.GetTables());
        //            case 5:
        //                return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeeMetadata()));
        //            case 6:
        //                return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeeAbsenceMetadata()));
        //            case 7:
        //                return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeeRelativeMetadata()));
        //            case 8:
        //                return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeeQualificationMetadata()));
        //            case 9:
        //                return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeePortalSetupMetadata()));
        //            case 10:
        //                return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeeStatisticsGroupMetadata()));
        //        }
        //        return null;
        //    }
        //    set { }
        //}

        private int metadataSelectedIndex;
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
                    NotifyPropertyChanged("MetadataSelectedItem");

                    Data = DataOperation(metadataSelectedIndex);
                }
            }
        }

        public List<List<string>> DataOperation(int index)
        {
            List<List<string>> stringValues = new List<List<string>>();
            switch (index)
            {
                case 0:
                    CronusReference.DataTuple value = cronusClient.GetIllestPerson();
                    CronusReference.DataTuple[] values = new CronusReference.DataTuple[] { value };
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
                case 1:
                    values = cronusClient.GetIllPersonsByYear(2004, 2005); //statiskt anrop för 2004 och 2005 som efterfrågas
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
                case 2:
                    values = cronusClient.GetEmployeeAndRelatives();
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
                case 3:
                    values = cronusClient.GetEmployeeData();
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
                case 4:
                    values = cronusClient.GetEmployeeAbsenceData();
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
                case 5:
                    values = cronusClient.GetEmployeeRelativeData();
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
                case 6:
                    values = cronusClient.GetEmployeeQualificationData();
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
                case 7:
                    values = cronusClient.GetEmployeePortalSetupData();
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
                case 8:
                    values = cronusClient.GetEmployeeStatisticsGroupData();
                    return DataTupleToNiceFormat((GetValuableInformation(values)));
            }

            return null;
        }

    }
}
