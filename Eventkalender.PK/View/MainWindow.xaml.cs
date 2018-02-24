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

namespace Eventkalender.PK
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
 //       private EventkalenderController eventkalenderController;
        private EventkalenderViewModel eventkalenderViewModel;

        // Dessa två ska inte finnas kvar i vyn om det går att lösa 
        // användandet av dem på ett snyggt sätt i ViewModel
        private CronusServiceSoapClient cronusClient;
        private EventkalenderServiceSoapClient eventkalenderWSClient;
        
        public MainWindow()
        {
            InitializeComponent();

            eventkalenderViewModel = new EventkalenderViewModel();
            cronusClient = new CronusServiceSoapClient();
            eventkalenderWSClient = new EventkalenderServiceSoapClient();

            DataContext = eventkalenderViewModel;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------
        //
        //                                  CRONUS Eventhandlers
        //
        //-----------------------------------------------------------------------------------------------------------------------------------

        private void btnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
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
            int index = dgEmployee.SelectedIndex;
            bool exists = false;
           // if (index >= 0)
          //  {
                if (Utility.IsNotEmpty(txtEmployeeNumber.Text, txtEmployeeFirstName.Text, txtEmployeeLastName.Text))
                {
                    
                    foreach (CronusReference.Employee emp in eventkalenderViewModel.Employees)
                    {
                        if (txtEmployeeNumber.Text.Equals(emp.No))
                        {
                             exists = true;                     
                        }
                    }
                    if (exists)
                    {                                              
                        string no = txtEmployeeNumber.Text;
                        string firstName = txtEmployeeFirstName.Text;
                        string lastName = txtEmployeeLastName.Text;
                        eventkalenderViewModel.UpdateEmployee(no, firstName, lastName, index);
                    }
                    else
                    {
                        WriteOutput("Det finns ingen person med detta ID");
                    }
                }
                else
                {
                    WriteOutput("Du måste fylla i alla fält");
                }
           // }
        }

       

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            bool exists = true;
            if (Utility.IsNotEmpty(txtEmployeeNumber.Text, txtEmployeeFirstName.Text, txtEmployeeLastName.Text))
            {
                foreach(CronusReference.Employee emp in eventkalenderViewModel.Employees)
                {
                    if (txtEmployeeNumber.Text.Equals(emp.No))
                    {
                        exists = false;
                    }
                }
                if (exists)
                {
                    string no = txtEmployeeNumber.Text;
                    string firstName = txtEmployeeFirstName.Text;
                    string lastName = txtEmployeeLastName.Text;
                    eventkalenderViewModel.AddEmployee(no, firstName, lastName);
                }
                else
                {
                    WriteOutput("Det finns redan en person med detta ID");
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
            CronusReference.Employee temp = new CronusReference.Employee();
            temp = eventkalenderViewModel.Employees.ElementAt(index);
            txtEmployeeFirstName.Text = temp.FirstName;
            txtEmployeeLastName.Text = temp.LastName;
            txtEmployeeNumber.Text = temp.No;
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
        }

        //------------------------------------------------------------------------------------------------------------------------------------
        //
        //                                  Programkonstruktion Eventhandlers
        //
        //-----------------------------------------------------------------------------------------------------------------------------------

        

        private void btnEraseFromPerson(object sender, RoutedEventArgs e)
        {
            int index = datagridPerson.SelectedIndex;
            if (index > -1)
            {
                Database.Person person = eventkalenderViewModel.Persons.ElementAt(index);
                eventkalenderViewModel.DeletePerson(person.Id);
            }
        }

        private void btnEraseFromNation_Click(object sender, RoutedEventArgs e)
        {
            int index = datagridNation.SelectedIndex;
            if (index > -1)
            {
                Database.Nation n = eventkalenderViewModel.Nations.ElementAt(index);
                eventkalenderViewModel.DeleteNation(index);
            }
        }

        private void btnDeleteEventClick(object sender, RoutedEventArgs e)
        {
            int index = datagridEvents.SelectedIndex;
            if(index > -1)
            {
                Database.Event ev = eventkalenderViewModel.Events.ElementAt(index);
                eventkalenderViewModel.DeleteEvent(ev.Id);
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
                WriteOutput("Inget värde ifyllt");
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
                WriteOutput("Glöm inte ange efternamnet");
            }
            else if (txtBoxFirstName.Text == "")
            {
                WriteOutput("Glöm inte ange förnamnet");
            }
        }

        private void btnRegsterEventClick(object sender, RoutedEventArgs e)
        {
            int index = cmBoxNation.SelectedIndex;
            
            if (txtBoxEventName.Text == "")
            {
                WriteOutput("Ange ett namn till evenemanget.");
            }
            else if (eventkalenderViewModel.Nations.Select(temp => temp.Name.Equals(Convert.ToString(cmBoxNation.SelectedItem))).Count() <= 0 || cmBoxNation.SelectedIndex == -1)
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
            }
            else if (txtBoxSummary.Text == "")
            {
                WriteOutput("Ange en beskrivning till evenemanget.");
            }
            if (Utility.IsNotEmpty(txtBoxEventName.Text, cmBoxNation.Text, dtpickStartDate.Text, cmbStartTime.Text, dtpickEndDate.Text, cmbEndTime.Text, txtBoxSummary.Text))
            {
                //  Database.Event eventet = new Database.Event();
                DateTime dateStart = Utility.ToDate(dtpickStartDate.Text, cmbStartTime.Text);
                DateTime dateEnd = Utility.ToDate(dtpickEndDate.Text, cmbEndTime.Text);

                if (dateEnd.CompareTo(dateStart) < 0)
                {
                    WriteOutput("Ditt evenemang kan inte sluta före det börjar.");
                }
                else
                {
                    Database.Nation n = eventkalenderViewModel.Nations.ElementAt(index);
                    /*  eventet.Name = txtBoxEventName.Text;
                    eventet.Summary = txtBoxSummary.Text;
                    eventet.StartTime = dateStart;
                    eventet.EndTime = dateEnd;

                    n.Events.Add(eventet);*/
                    eventkalenderViewModel.AddEvent(txtBoxEventName.Text, txtBoxSummary.Text, dateStart, dateEnd, n.Id);

                    dtpickStartDate.Text = "";
                    dtpickEndDate.Text = "";
                    cmbStartTime.SelectedIndex = -1;
                    cmbEndTime.SelectedIndex = -1;
                    txtBoxEventName.Text = "";
                    txtBoxSummary.Text = "";
                }

            }
        }

        private void btnInvToEvent_Click(object sender, RoutedEventArgs e)
        {
            int index = datagridInviteEvent.SelectedIndex;
            Database.Event ev = eventkalenderViewModel.Events.ElementAt(index);

            eventkalenderViewModel.InviteToEvent(datagridInvitePersons.SelectedItems, ev);
        }

        private void cmBoxSearchEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = cmBoxSearchEvents.SelectedIndex;
            if (index > -1)
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
            if (index == -1)
            {
                datagridFindEvents.ItemsSource = eventkalenderViewModel.Events;
            }
            else if (index > -1)
            {
                Database.Nation n = eventkalenderViewModel.Nations.ElementAt(index);
                datagridFindEvents.ItemsSource = n.Events;
            }
            
        }

        private void cmbInviteEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = cmbInviteEvent.SelectedIndex;
            if (index == -1)
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

        public void WriteOutput(string message)
        {
            txtboxConsole.Text = message;
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
        
        private void btnChoiceOfFile_Click(object sender, RoutedEventArgs e)
        {
            if(txtboxSearchFile.Text != "")
            {
                string path = txtboxSearchFile.Text;
                if(eventkalenderViewModel.GetFiles().Contains(path))
                {
                    txtboxOutput.Text = eventkalenderViewModel.GetFile(path);
                    
                }
                else
                {
                    string error = path + " fanns ej i systemet. Skrev du rätt filnamn?";
                    WriteOutput(error);
                }
            }
            else
            {
                string s = "Fyll i textboxen för att söka efter en fil";
                WriteOutput(s);
            }
        }

        private void btnMarkAllPerson_Click(object sender, RoutedEventArgs e)
        {
            datagridInvitePersons.SelectAllCells();
        }

        private void Webservices_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Warm up Entity Framework on WS
            eventkalenderViewModel.GetEmployees();
        }
    }
}
