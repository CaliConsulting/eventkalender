using Eventkalender.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Eventkalender.PK.CronusReference;
using Eventkalender.PK.EventkalenderReference;
using System.Data;

namespace Eventkalender.PK.GUI
{
    /// <summary>
    /// Interaction logic for GUI_Prototyp.xaml
    /// </summary>
    public partial class GUI_Prototyp : Window
    {
        private ObservableCollection<Database.Person> persons;
        private ObservableCollection<Database.Event> events;
        private List<string> timesList;
        private EventkalenderController eventkalenderController;
        private CronusServiceSoapClient cronusClient;
        private EventkalenderServiceSoapClient eventkalenderWSClient;


        public GUI_Prototyp()
        {
            InitializeComponent();
            DataContext = this;
            eventkalenderController = new EventkalenderController("Resources/eventkalender-db.xml");
            cronusClient = new CronusServiceSoapClient();
            eventkalenderWSClient = new EventkalenderServiceSoapClient();
        }

        /*  public void GetMetadataByDataTuples(CronusReference.DataTuple[] inputTuple)
          {
              CronusReference.DataTuple[] data = inputTuple;
              for (int i = 0; i < data.Length; i++)
              {
                  Console.WriteLine(data[i].ToString());
              }
          }

          public void GetMetadataListOfString(List<string> metod)
          {
              foreach (string row in metod)
              {
                  Console.WriteLine(row);
              }
          } */

        public void GetEmployeeMetadata()
        {
            cronusClient.GetEmployeeMetadata();
            cronusClient.GetIndexes();
        }

        public void GetEmployeeAbsenceMetadata()
        {
            cronusClient.GetEmployeeAbsenceMetadata();
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
        public List<string> Nations
        {
            get
            {
                List<Database.Nation> nations = eventkalenderController.GetNations();
                List<string> nationName = new List<string>();
                foreach (Database.Nation n in nations)
                {
                    nationName.Add(n.Name);
                }
                return nationName;
            }
            set { }
        }
        public List<string> DataCombobox
        {
            get
            {
                List<string> lst = new List<string>();
                lst.Add("GetIllestPerson");
                lst.Add("GetIllPersonsByYear");
                lst.Add("GetEmployeeAndRelatives");
                lst.Add("GetEmployeeData");
                lst.Add("GetEmployeeAbsenceData");
                lst.Add("GetEmployeeRelativeData");
                lst.Add("GetEmployeeQualificationData");
                lst.Add("GetEmployeePortalSetupData");
                lst.Add("GetEmployeeStatisticsGroupData");


                return lst;
            }
            private set { }
        }

        public DataTable GetData
        {
            get
            {
                switch (cmbData.SelectedIndex)
                {
                    case 0:
                        CronusReference.DataTuple value = cronusClient.GetIllestPerson();
                        CronusReference.DataTuple[] values = new CronusReference.DataTuple[] { value };
                        return ConvertListToDataTable(GetValuableInformation(values));
                    case 1:
                        values = cronusClient.GetIllPersonsByYear(2004, 2005); //statiskt anrop för 2004 och 2005 som efterfrågas
                        return ConvertListToDataTable(GetValuableInformation(values));
                    case 2:
                        values = cronusClient.GetEmployeeAndRelatives();
                        return ConvertListToDataTable(GetValuableInformation(values));
                    case 3:
                        values = cronusClient.GetEmployeeData();
                        return ConvertListToDataTable(GetValuableInformation(values));
                    case 4:
                        values = cronusClient.GetEmployeeAbsenceData();
                        return ConvertListToDataTable(GetValuableInformation(values));
                    case 5:
                        values = cronusClient.GetEmployeeRelativeData();
                        return ConvertListToDataTable(GetValuableInformation(values));
                    case 6:
                        values = cronusClient.GetEmployeeQualificationData();
                        return ConvertListToDataTable(GetValuableInformation(values));
                    case 7:
                        values = cronusClient.GetEmployeePortalSetupData();
                        return ConvertListToDataTable(GetValuableInformation(values));
                    case 8:
                        values = cronusClient.GetEmployeeStatisticsGroupData();
                        return ConvertListToDataTable(GetValuableInformation(values)); 
                }


                return null;
            }
            set { }
        }

        static DataTable ConvertListToDataTable(List<string[]> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add(list.First()[i]);
            }

            // Add rows.
            for (int j = 1; j < list.Count; j++)
            {
                string[] row = list[j];
                DataRow dr = table.NewRow();
                for (int k = 0; k < columns; k++)
                {
                    dr[k] = row[k];
                }
                

                table.Rows.Add(dr);
                
            }

            return table;
        }

        public List<string[]> GetValuableInformation(CronusReference.DataTuple[] values)
        {
            bool isFirst = true;
            List<string[]> totals = new List<string[]>();
            for(int i=0; i<values.Length; i++)
            {
                CronusReference.DataTuple t = values[i];
                string[] array = new string[t.Count];
                string[] columns = new string[t.Count];

                if (isFirst)
                {
                    totals.Add(columns);
                    isFirst = false;
                }
                for (int j = 0; j < t.Count; j++)
                {        
                    SerializableKeyValuePairOfStringString s = t.ElementAt(j);
                    columns[j] = s.Key;
                    array[j] = s.Value;
  
                }
                totals.Add(array);
            }
            
            return totals;
        }


        public List<string> MetadataCombobox
        {
            get
            {
                List<string> lst = new List<string>();
                lst.Add("GetIndexes");
                lst.Add("GetKeys");
                lst.Add("GetColumnsForEmployeeTable");
                lst.Add("GetTableConstraints");
                lst.Add("GetTables");
                lst.Add("GetEmployeeMetadata");
                lst.Add("GetEmployeeAbsenceMetadata");
                lst.Add("GetEmployeeRelativeMetadata");
                lst.Add("GetEmployeeQualificationMetadata");
                lst.Add("GetEmployeePortalSetupMetadata");
                lst.Add("GetEmployeeStatisticsGroupMetadata");
                
                return lst;
            } 
            private set { }
        }
        
        public List<string> GetMetadata
        {
            get
            {
                switch (cmbMetaData.SelectedIndex)
                {
                    case 0:
                        List<string> values = cronusClient.GetTableConstraints(); //Combobox.whatever(i) i = val i listan
                        return values;
                    case 1:
                        values = cronusClient.GetTableConstraints(); 
                        return values;
                    case 2:
                        values = cronusClient.GetTableConstraints();
                        return values;
                    case 3:
                        values = cronusClient.GetTableConstraints(); 
                        return values;
                    case 4:
                        values = cronusClient.GetTableConstraints(); 
                        return values;
                    case 5:
                        values = cronusClient.GetTableConstraints(); 
                        return values;
                    case 6:
                        values = cronusClient.GetTableConstraints();
                        return values;
                    case 7:
                        values = cronusClient.GetTableConstraints(); 
                        return values;
                    case 8:
                        values = cronusClient.GetTableConstraints(); 
                        return values;
                    case 9:
                        values = cronusClient.GetTableConstraints(); 
                        return values;
                    case 10:
                        values = cronusClient.GetTableConstraints();
                        return values;
                   
                }
                return null;


            }
            set { }
        }
        public ObservableCollection <Database.Person> Persons
        {
            get
            {
                if( persons == null)
                {
                    return persons = new ObservableCollection<Database.Person>(eventkalenderController.GetPersons());
                }
                return persons;
            }
            set { }
        }
        public ObservableCollection <Database.Event> Events
        {
            get
            {
                if( events == null)
                { 
                    return events = new ObservableCollection<Database.Event>(eventkalenderController.GetEvents());
                }
                return events;
            }
            set { }
        }
        
        private void SearchBoxGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }

        private void btnEraseFromListClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult raderaResultat = MessageBox.Show("Vill ni verkligen ta bort innehållet?", "Radera", MessageBoxButton.YesNo);
            if (raderaResultat == MessageBoxResult.Yes)
            {

            }
            if (raderaResultat == MessageBoxResult.No)
            {

            }
        }

        private void btnDeleteEventClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult raderaResultat = MessageBox.Show("Vill ni verkligen ta bort innehållet?", "Radera", MessageBoxButton.YesNo);
            if (raderaResultat == MessageBoxResult.Yes)
            {

            }
            if (raderaResultat == MessageBoxResult.No)
            {

            }
        }

        private void btnRegNationNameClick(object sender, RoutedEventArgs e)
        {
            if(txtBoxNationName.Text != "")
            {
                eventkalenderController.AddNation(txtBoxNationName.Text);
                txtBoxNationName.Text = "";
             }
            else
            {
                MessageBox.Show("Inget värde ifyllt");
            }
        }
        private void btnRegPers_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxFirstName.Text != "" && txtBoxLastName.Text != "")
            {
                eventkalenderController.AddPerson(txtBoxFirstName.Text, txtBoxLastName.Text);
                txtBoxFirstName.Text = "";
                txtBoxLastName.Text = "";
            }
            else if (txtBoxLastName.Text == "")
            {
                MessageBox.Show("Glöm inte ange efternamnet");
            }
            else if (txtBoxFirstName.Text == "")
            {
                MessageBox.Show("Glöm inte ange förnamnet");
            }
        }

        private void btnSrchEventClick(object sender, RoutedEventArgs e)
        {
            if(txtBoxSearchEvents.Text != "")
            {
                bool isNumeric = int.TryParse(txtBoxSearchEvents.Text, out int eventId);

                if (isNumeric)
                {
                    eventkalenderController.GetEvent(eventId);
                }
                
            }
            else
            {
                MessageBox.Show("Ange ett värde.");
            }

        }

        private void btnRegsterEventClick(object sender, RoutedEventArgs e)
        {
            if(Utility.CheckIfEmpty(txtBoxEventName.Text, cmBoxNation.Text, dtpickStartDate.Text, cmbStartTime.Text, dtpickEndDate.Text, cmbEndTime.Text, txtBoxSummary.Text))
            {
                DateTime dateStart = Utility.ToDate(dtpickStartDate.Text, cmbStartTime.Text);
                DateTime dateEnd = Utility.ToDate(dtpickEndDate.Text, cmbEndTime.Text);
                int nationID = Convert.ToInt32(cmBoxNation.Text);
                eventkalenderController.AddEvent(txtBoxEventName.Text, txtBoxSummary.Text, dateStart, dateEnd, nationID);
            }
            else
            {

            }
        }

        private void dtgShowEventsSelectionChanged(object sender, SelectionChangedEventArgs e)

        {

        }

        private void btnUpdateCronusClick(object sender, RoutedEventArgs e)
        {

        }

        private void cmbMetaData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            datagridCronus.ItemsSource = GetMetadata;
        }

        private void cmbData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // datagridCronus.ItemsSource = GetData;
            DataTable table = ConvertListToDataTable(GetValuableInformation(cronusClient.GetEmployeeAbsenceData()));  //new CronusReference.DataTuple[0]
            datagridCronus.DataContext = table.DefaultView;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            datagridEvents.ItemsSource = Events;
        }

        private void btnAllEvents_Click(object sender, RoutedEventArgs e)
        {
            
           dtgShowEvents.ItemsSource = Events;
        }

        private void datagridCronus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable table = ConvertListToDataTable(GetValuableInformation(cronusClient.GetEmployeeAbsenceData()));  //new CronusReference.DataTuple[0]
            datagridCronus.DataContext = table.DefaultView;
        }

        private void cmbMetaData_DropDownClosed(object sender, EventArgs e)
        {
            
        }

        private void cmbData_DropDownClosed(object sender, EventArgs e)
        {
            DataTable table = ConvertListToDataTable(GetValuableInformation(cronusClient.GetEmployeeAbsenceData()));  //new CronusReference.DataTuple[0]
            datagridCronus.DataContext = table.DefaultView;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
