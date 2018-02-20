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
        private EventkalenderController eventkalenderController;
        private EventkalenderViewModel eventkalenderViewModel;
        private CronusServiceSoapClient cronusClient;
        private EventkalenderServiceSoapClient eventkalenderWSClient;


        public GUI_Prototyp()
        {
            InitializeComponent();
            eventkalenderController = new EventkalenderController("Resources/eventkalender-db.xml");
            eventkalenderViewModel = new EventkalenderViewModel();
            cronusClient = new CronusServiceSoapClient();
            eventkalenderWSClient = new EventkalenderServiceSoapClient();
            DataContext = eventkalenderViewModel;
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
//------------------------------------------------------------------------------------------------------------------------------------
//
//                                  CRONUS Eventhandlers
//
//-----------------------------------------------------------------------------------------------------------------------------------

      

        private void btnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbMetaData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.datagridCronus.Columns.Clear();
            this.datagridCronus.ItemsSource = null;
            datagridCronus.ItemsSource = GetMetadata;
        }

        private void cmbData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.datagridCronus.Columns.Clear();
            this.datagridCronus.ItemsSource = null;
            datagridCronus.ItemsSource = GetData;
            //Selected index måste med bror
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

        public List<List<string>> GetData
        {
            get
            {
                List<List<string>> stringValues = new List<List<string>>();



                switch (cmbData.SelectedIndex)
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
            set { }
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

            DataGridTextColumn t = new DataGridTextColumn();
            t.Header = 0;
            t.Binding = new Binding("[" + 0 + "]");

            datagridCronus.Columns.Add(t);

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
            for (int i = 0; i < lst[0].Count; i++)
            {
                DataGridTextColumn t = new DataGridTextColumn();
                t.Header = lst.First()[i];
                t.Binding = new Binding("[" + i + "]");

                datagridCronus.Columns.Add(t);
            }
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

        public List<List<string>> GetMetadata
        {
            get
            {
                switch (cmbMetaData.SelectedIndex)
                {
                    case 0:
                        return DataTupleToNiceFormat(cronusClient.GetIndexes());
                    case 1:
                        return DataTupleToNiceFormat(cronusClient.GetKeys());
                    case 2:
                        return DataTupleToNiceFormat(cronusClient.GetColumnsForEmployeeTable());
                    case 3:
                        return DataTupleToNiceFormat(cronusClient.GetTableConstraints());
                    case 4:
                        return DataTupleToNiceFormat(cronusClient.GetTables());
                    case 5:
                        return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeeMetadata()));
                    case 6:
                        return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeeAbsenceMetadata()));
                    case 7:
                        return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeeRelativeMetadata()));
                    case 8:
                        return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeeQualificationMetadata()));
                    case 9:
                        return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeePortalSetupMetadata()));
                    case 10:
                        return DataTupleToNiceFormat(GetValuableInformation(cronusClient.GetEmployeeStatisticsGroupMetadata()));
                }
                return null;
            }
            set { }
        }


        //------------------------------------------------------------------------------------------------------------------------------------
        //
        //                                  Programkonstruktion Eventhandlers
        //
        //-----------------------------------------------------------------------------------------------------------------------------------

        private void btnEraseFromListClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult raderaResultat = MessageBox.Show("Vill ni verkligen ta bort innehållet?", "Radera", MessageBoxButton.YesNo);
            if (raderaResultat == MessageBoxResult.Yes)
            {
                if (cmbFilterList.SelectedIndex == 0)
                {
                    int index = datagridPersonNation.SelectedIndex;
                    Database.Nation nation = eventkalenderViewModel.Nations.ElementAt(index);
                    eventkalenderController.DeleteNation(nation.Id);
                    datagridPersonNation.Items.RemoveAt(index);
                }
                if (cmbFilterList.SelectedIndex == 1)
                {
                    int index = datagridPersonNation.SelectedIndex;
                    Database.Person person = eventkalenderViewModel.Persons.ElementAt(index);
                    eventkalenderController.DeletePerson(person.Id);
                    datagridPersonNation.Items.RemoveAt(index);
                }
            }
        }

        private void btnDeleteEventClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult raderaResultat = MessageBox.Show("Vill ni verkligen ta bort innehållet?", "Radera", MessageBoxButton.YesNo);
            if (raderaResultat == MessageBoxResult.Yes)
            {
                int index = datagridEvents.SelectedIndex;

                Database.Event ev = eventkalenderViewModel.Events.ElementAt(index);
                eventkalenderController.DeleteEvent(ev.Id);
                datagridEvents.Items.RemoveAt(index);
            }
        }

        private void btnRegNationNameClick(object sender, RoutedEventArgs e)
        {
            if (txtBoxNationName.Text != "")
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
                eventkalenderViewModel.AddPerson(txtBoxFirstName.Text, txtBoxLastName.Text);
                //eventkalenderController.AddPerson(txtBoxFirstName.Text, txtBoxLastName.Text);
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

        private void btnRegsterEventClick(object sender, RoutedEventArgs e)
        {
            if(Utility.CheckIfEmpty(txtBoxEventName.Text, cmBoxNation.Text, dtpickStartDate.Text, cmbStartTime.Text, dtpickEndDate.Text, cmbEndTime.Text, txtBoxSummary.Text))
            {
                DateTime dateStart = Utility.ToDate(dtpickStartDate.Text, cmbStartTime.Text);
                DateTime dateEnd = Utility.ToDate(dtpickEndDate.Text, cmbEndTime.Text);

                int index = cmBoxNation.SelectedIndex;
                Database.Nation n = eventkalenderViewModel.Nations.ElementAt(index);
                
                eventkalenderController.AddEvent(txtBoxEventName.Text, txtBoxSummary.Text, dateStart, dateEnd, n.Id);
                dtpickStartDate.Text = "";
                dtpickEndDate.Text = "";
            }
            else
            {

            }
        }
        private void btnInvToEvent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmBoxSearchEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = cmBoxSearchEvents.SelectedIndex;
            if(index > -1)
            {
                Database.Nation n = eventkalenderViewModel.Nations.ElementAt(index);
                datagridEvents.ItemsSource = n.Events;
            }
            else
            {
                datagridEvents.ItemsSource = eventkalenderViewModel.Events;
            }


        }

        private void cmbEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = cmbEvents.SelectedIndex;
            if (index > -1)
            {
                Database.Nation n = eventkalenderViewModel.Nations.ElementAt(index);
                datagridFindEvents.ItemsSource = n.Events;
            }
            
        }

        private void cmbFilterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbFilterList.SelectedIndex == 0)
            {
                datagridPersonNation.ItemsSource = eventkalenderViewModel.Nations;
             
            }
            if(cmbFilterList.SelectedIndex == 1)
            {
                datagridPersonNation.ItemsSource = eventkalenderViewModel.Persons;
            }
        }

        private void cmbInviteEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = cmbInviteEvent.SelectedIndex;
            if(index == -1)
            {
                datagridInviteEvent.ItemsSource = eventkalenderViewModel.Events;
            }
            else if (index > -1)
            {
                Database.Nation n = eventkalenderViewModel.Nations.ElementAt(index);

                //datagridInviteEvent.Columns[2].val

                datagridInviteEvent.ItemsSource = n.Events;
            }
            else
            {
                datagridInvitePersons.ItemsSource = eventkalenderViewModel.Persons;
            }
        }

        private void cmbInvitePersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = cmbInvitePersons.SelectedIndex;
            if (index > -1)
            {
                Database.Person person = eventkalenderViewModel.Persons.ElementAt(index);
                datagridInvitePersons.ItemsSource = new List<Database.Person>() { person };
            }
            else
            {
                datagridInvitePersons.ItemsSource = eventkalenderViewModel.Persons;
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------
//
//                                  Webservice eventhandlers
//
//-----------------------------------------------------------------------------------------------------------------------------------

        private void cmbWebService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbWebService.SelectedIndex == 0) //event kalla webservicen
            {

            }
            if (cmbWebService.SelectedIndex == 1) //nation
            {

            }
            if (cmbWebService.SelectedIndex == 2) //person
            {

            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void datagridPersonNation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

  
    }
}
