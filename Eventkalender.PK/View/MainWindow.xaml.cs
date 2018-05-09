using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Eventkalender.PK
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EventkalenderViewModel eventkalenderViewModel;

        public MainWindow()
        {
            InitializeComponent();
            eventkalenderViewModel = new EventkalenderViewModel();
            DataContext = eventkalenderViewModel;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------
        //
        //                                  CRONUS Eventhandlers
        //
        //-----------------------------------------------------------------------------------------------------------------------------------

        private void btnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
            int index = dgEmployee.SelectedIndex;
            if (index >= 0)
            {
                CronusReference.Employee emp = eventkalenderViewModel.Employees.ElementAt(index);
                string no = emp.No;
                eventkalenderViewModel.DeleteEmployee(no);
            }
        }

        private void btnUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
            int index = dgEmployee.SelectedIndex;
            if (Utility.IsNotEmpty(txtEmployeeNumber.Text, txtEmployeeFirstName.Text, txtEmployeeLastName.Text))
            {
                string no = txtEmployeeNumber.Text;
                if (eventkalenderViewModel.Employees.Any(temp => temp.No == no))
                {
                    string firstName = txtEmployeeFirstName.Text;
                    string lastName = txtEmployeeLastName.Text;
                    eventkalenderViewModel.UpdateEmployee(no, firstName, lastName, index);
                }
                else
                {
                    WriteOutput("Det finns ingen anställd med detta ID");
                }
            }
            else
            {
                WriteOutput("Du måste fylla i alla fält");
            }
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
            if (Utility.IsNotEmpty(txtEmployeeNumber.Text, txtEmployeeFirstName.Text, txtEmployeeLastName.Text))
            {
                string no = txtEmployeeNumber.Text;
                if (!eventkalenderViewModel.Employees.Any(temp => temp.No == no))
                {
                    string firstName = txtEmployeeFirstName.Text;
                    string lastName = txtEmployeeLastName.Text;
                    eventkalenderViewModel.AddEmployee(no, firstName, lastName);
                }
                else
                {
                    WriteOutput("Det finns redan en anställd med detta ID");
                }
            }
            else
            {
                WriteOutput("Du måste fylla i alla fält");
            }
        }

        private void dgEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = dgEmployee.SelectedIndex;
            if (index > -1)
            {
                CronusReference.Employee temp = new CronusReference.Employee();
                temp = eventkalenderViewModel.Employees.ElementAt(index);
                txtEmployeeFirstName.Text = temp.FirstName;
                txtEmployeeLastName.Text = temp.LastName;
                txtEmployeeNumber.Text = temp.No;
            }
        }

        private void cmbMetadata_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgCronus.Columns.Clear();
            dgCronus.ItemsSource = null;
            Utility.AddColumns(dgCronus, eventkalenderViewModel.Metadata);
            dgCronus.ItemsSource = eventkalenderViewModel.Metadata;
            eventkalenderViewModel.Autosize(dgCronus);
        }

        private void cmbData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgCronus.Columns.Clear();
            dgCronus.ItemsSource = null;
            Utility.AddColumns(dgCronus, eventkalenderViewModel.Data);
            dgCronus.ItemsSource = eventkalenderViewModel.Data;
            eventkalenderViewModel.Autosize(dgCronus);
        }

        //------------------------------------------------------------------------------------------------------------------------------------
        //
        //                                  Programkonstruktion Eventhandlers
        //
        //-----------------------------------------------------------------------------------------------------------------------------------

        private void btnDeletePerson_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
            int index = dgPerson.SelectedIndex;
            if (index > -1)
            {
                Database.Person person = eventkalenderViewModel.Persons.ElementAt(index);
                eventkalenderViewModel.DeletePerson(person.Id);
            }
        }

        private void btnDeleteNation_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
            int index = dgNation.SelectedIndex;
            if (index > -1)
            {
                Database.Nation n = eventkalenderViewModel.Nations.ElementAt(index);
                eventkalenderViewModel.DeleteNation(n.Id);
            }
        }

        private void btnDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
            int index = dgEvents.SelectedIndex;
            if (index > -1)
            {
                Database.Event ev = eventkalenderViewModel.Events.ElementAt(index);
                eventkalenderViewModel.DeleteEvent(ev.Id);
            }
            
        }

        private void btnRegisterNationName_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
            if (txtNationName.Text != "")
            {
                eventkalenderViewModel.AddNation(txtNationName.Text);
                txtNationName.Text = "";
            }
            else
            {
                WriteOutput("Ange ett namn till nationen.");
            }
        }

        private void btnRegisterPerson_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
            if (txtFirstName.Text != "" && txtLastName.Text != "")
            {
                eventkalenderViewModel.AddPerson(txtFirstName.Text, txtLastName.Text);
                txtFirstName.Text = "";
                txtLastName.Text = "";
            }
            else if (txtLastName.Text == "")
            {
                WriteOutput("Glöm inte ange efternamnet");
            }
            else if (txtFirstName.Text == "")
            {
                WriteOutput("Glöm inte ange förnamnet");
            }
        }

        private void btnRegisterEvent_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
            int index = cmbNation.SelectedIndex;

            if (txtEventName.Text == "")
            {
                WriteOutput("Ange ett namn till evenemanget.");
            }
            else if (!eventkalenderViewModel.Nations.Any(temp => temp.Name.Equals(Convert.ToString(cmbNation.Text))) || cmbNation.SelectedIndex == -1)
            {
                WriteOutput("Du måste välja en befintlig nation.");
            }
            else if (dtpickEndDate.Text == "" || dtpickStartDate.Text == "")
            {
                WriteOutput("Ange start- och slutdatum.");
            }
            else if (cmbStartTime.SelectedIndex == -1 || cmbEndTime.SelectedIndex == -1)
            {
                WriteOutput("Ange start- och sluttid.");
                cmbStartTime.Text = "";
                cmbEndTime.Text = "";
            }
            else if (txtSummary.Text == "")
            {
                WriteOutput("Ange en beskrivning till evenemanget.");
            }
            if (Utility.IsNotEmpty(txtEventName.Text, cmbNation.Text, dtpickStartDate.Text, cmbStartTime.Text, dtpickEndDate.Text, cmbEndTime.Text, txtSummary.Text))
            {
                DateTime dateStart = Utility.ToDate(dtpickStartDate.Text, cmbStartTime.Text);
                DateTime dateEnd = Utility.ToDate(dtpickEndDate.Text, cmbEndTime.Text);

                if (dateEnd.CompareTo(dateStart) < 0)
                {
                    WriteOutput("Ditt evenemang kan inte sluta före det börjar.");
                }
                else
                {
                    Database.Nation n = eventkalenderViewModel.Nations.ElementAt(index);
                    eventkalenderViewModel.AddEvent(txtEventName.Text, txtSummary.Text, dateStart, dateEnd, n.Id);

                    dtpickStartDate.Text = "";
                    dtpickEndDate.Text = "";
                    cmbStartTime.SelectedIndex = -1;
                    cmbEndTime.SelectedIndex = -1;
                    txtEventName.Text = "";
                    txtSummary.Text = "";
                }
            }
        }

        private void btnMarkAllPerson_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
            dgInvitePersons.SelectAllCells();
        }

        private void cmbSearchEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = cmbSearchEvents.SelectedIndex;
            if (index > -1)
            {
                Database.Nation n = eventkalenderViewModel.Nations.ElementAt(index);
                dgEvents.ItemsSource = n.Events;
            }
            else
            {
                dgEvents.ItemsSource = eventkalenderViewModel.Events;
            }
        }

        private void cmbEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = cmbEvents.SelectedIndex;
            if (index == -1)
            {
                dgFindEvents.ItemsSource = eventkalenderViewModel.Events;
            }
            else if (index > -1)
            {
                Database.Nation n = eventkalenderViewModel.Nations.ElementAt(index);
                dgFindEvents.ItemsSource = n.Events;
            }
        }

        private void btnInviteToEvent_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
            int index = dgInviteEvent.SelectedIndex;
            if (index > -1)
            {
                Database.Event ev = eventkalenderViewModel.Events.ElementAt(index);
                eventkalenderViewModel.InviteToEvent(dgInvitePersons.SelectedItems, ev);
            }
        }

        private void cmbInviteEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = cmbInviteEvent.SelectedIndex;
            if (index == -1)
            {
                dgInviteEvent.ItemsSource = eventkalenderViewModel.Events;
            }
            else if (index > -1)
            {
                Database.Nation n = eventkalenderViewModel.Nations.ElementAt(index);
                dgInviteEvent.ItemsSource = n.Events;
            }
            else
            {
                dgInvitePersons.ItemsSource = eventkalenderViewModel.Persons;
            }
        }

        private void cmbInvitePersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = cmbInvitePersons.SelectedIndex;
            if (index > -1)
            {
                Database.Person person = eventkalenderViewModel.Persons.ElementAt(index);
                dgInvitePersons.ItemsSource = new List<Database.Person>() { person };
            }
            else
            {
                dgInvitePersons.ItemsSource = eventkalenderViewModel.Persons;
            }
        }

        public void WriteOutput(string message)
        {
            txtConsole.Text = message;
        }

        public void ClearOutput()
        {
            txtConsole.Text = "";
        }

        //-----------------------------------------------------------------------------------------------------------------------------------
        //
        //                                  Webservice eventhandlers
        //
        //-----------------------------------------------------------------------------------------------------------------------------------

        private void cmbWebService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbWebService.SelectedIndex == 0) //event kalla webservicen
            {
                eventkalenderViewModel.EventGridWrapAutoSize(dgWebService);
                dgWebService.ItemsSource = eventkalenderViewModel.GetEvents();
            }
            if (cmbWebService.SelectedIndex == 1) //nation
            {
                eventkalenderViewModel.NationGridWrapAutoSize(dgWebService);
                dgWebService.ItemsSource = eventkalenderViewModel.GetNations();
            }
            if (cmbWebService.SelectedIndex == 2) //person
            {
                eventkalenderViewModel.PersonGridWrapAutoSize(dgWebService);
                dgWebService.ItemsSource = eventkalenderViewModel.GetPersons();
            }
        }
        private void cmbSearchFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearOutput();
            string path = cmbSearchFile.SelectedItem as string;
            if (cmbSearchFile.SelectedIndex > -1)
            {
                txtOutput.Text = eventkalenderViewModel.GetFile(path);
            }
        }

        private void Webservices_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Warm up Entity Framework on WS
            eventkalenderViewModel.GetEmployees();
        }

        private void btnUpdatePersonNation_Click(object sender, RoutedEventArgs e)
        {
            eventkalenderViewModel.UpdateDatabase();
        }

        private void btnUpdateInvites_Click(object sender, RoutedEventArgs e)
        {
            eventkalenderViewModel.UpdateDatabase();
        }

        private void btnUpdateEvent_Click(object sender, RoutedEventArgs e)
        {
            eventkalenderViewModel.UpdateDatabase();
        }

        private void btnUpdateFind_Click(object sender, RoutedEventArgs e)
        {
            eventkalenderViewModel.UpdateDatabase();
        }
    }
}