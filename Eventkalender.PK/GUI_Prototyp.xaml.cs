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
 //       private EventkalenderController eventkalenderController;
        private EventkalenderViewModel eventkalenderViewModel;
        private CronusServiceSoapClient cronusClient;
        private EventkalenderServiceSoapClient eventkalenderWSClient;
        
        public GUI_Prototyp()
        {
            InitializeComponent();
 //           eventkalenderController = new EventkalenderController("Resources/eventkalender-db.xml");
            eventkalenderViewModel = new EventkalenderViewModel();
            cronusClient = new CronusServiceSoapClient();
            eventkalenderWSClient = new EventkalenderServiceSoapClient();
            DataContext = eventkalenderViewModel;
        }

//------------------------------------------------------------------------------------------------------------------------------------
//
//                                  CRONUS Eventhandlers
//
//-----------------------------------------------------------------------------------------------------------------------------------

        private void btnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbMetadata_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            datagridCronus.Columns.Clear();
            datagridCronus.ItemsSource = null;
            Utility.AddColumns(datagridCronus, eventkalenderViewModel.Metadata);
            datagridCronus.ItemsSource = eventkalenderViewModel.Metadata;
        }

        private void cmbData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            datagridCronus.Columns.Clear();
            datagridCronus.ItemsSource = null;
            Utility.AddColumns(datagridCronus, eventkalenderViewModel.Data);
            datagridCronus.ItemsSource = eventkalenderViewModel.Data;
            //Selected index måste med bror
            // Nope inte nödvändigt! Jag fixar detta mannen / Daniel
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
                    eventkalenderViewModel.DeleteNation(nation.Id);
                    datagridPersonNation.Items.RemoveAt(index);
                }
                if (cmbFilterList.SelectedIndex == 1)
                {
                    int index = datagridPersonNation.SelectedIndex;
                    Database.Person person = eventkalenderViewModel.Persons.ElementAt(index);
                    eventkalenderViewModel.DeletePerson(person.Id);
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
                eventkalenderViewModel.DeteleEvent(ev.Id);
            }
        }

        private void btnRegNationNameClick(object sender, RoutedEventArgs e)
        {
            if (txtBoxNationName.Text != "")
            {
                eventkalenderViewModel.AddNation(txtBoxNationName.Text);
//                eventkalenderController.AddNation(txtBoxNationName.Text);
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

                eventkalenderViewModel.AddEvent(txtBoxEventName.Text, txtBoxSummary.Text, dateStart, dateEnd, n.Id);
               // eventkalenderController.AddEvent(txtBoxEventName.Text, txtBoxSummary.Text, dateStart, dateEnd, n.Id);
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
            if (cmbFilterList.SelectedIndex == 1)
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

        private void datagridInvitePersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
