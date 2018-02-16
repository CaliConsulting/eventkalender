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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Eventkalender.PK.GUI
{
    /// <summary>
    /// Interaction logic for GUI_Prototyp.xaml
    /// </summary>
    public partial class GUI_Prototyp : Window
    {
        private List<string> timesList;
        private List<Nation> nationer;
        private List<Event> events;
        private EventkalenderController eventkalenderController;
        private CronusController cronusController;

        public GUI_Prototyp()
        {
            InitializeComponent();
            DataContext = this;
            eventkalenderController = new EventkalenderController("Resources/eventkalender-db.xml");
            cronusController = new CronusController("Resources/cronus-db.xml");
            events = new List<Event>();
            timesList = new List<string>();
            nationer = new List<Nation>();
        }

        public void GetMetadataByDataTuples(DataTuple[] inputTuple)
        {
            DataTuple[] data = inputTuple;
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine(data[i].ToString());
            }
        }

        public void GetMetadataListOfString(List<string> metod)
        {
            List<string> outputList = metod;

            /* int limit = Math.Min(metod.Count, 65000);
            for (int i = 0; i < limit; i++)
            {
                Console.WriteLine(metod.ElementAt(i));
            } */

            foreach (string row in outputList)
            {
                Console.WriteLine(row);
            }
        }

        public List<string> TimesList
        {
            set
            {  }
            get
            {
               return  timesList = Utility.GenerateList();
            }
        }
        public List<string> Nationerlist
        {
            get
            {
                nationer = eventkalenderController.GetNations();
                List<string> nationer1 = new List<string>();
                foreach(Nation n in nationer)
                {
                    nationer1.Add(n.Name);
                }
                return nationer1;
            }
            set {  }
        }
        public List<Event> EventList
        {
            get { return events = eventkalenderController.GetEvents(); }
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

        private void btnSrchEventClick(object sender, RoutedEventArgs e)
        {

        }

        private void btnRegsterEventClick(object sender, RoutedEventArgs e)
        {
            if(Utility.CheckIfEmpty(txtBoxEventName.Text, cmBoxNation.Text, dtpickStartDate.Text, cmbStartTime.Text, dtpickEndDate.Text, cmbEndTime.Text, txtBoxSummary.Text))
            {
                DateTime dateStart = Utility.ToDate(dtpickStartDate.Text, cmbStartTime.Text);
                DateTime dateEnd = Utility.ToDate(dtpickStartDate.Text, cmbStartTime.Text);
                int NationID = Convert.ToInt32(cmBoxNation.Text);
                eventkalenderController.AddEvent(txtBoxEventName.Text, txtBoxSummary.Text, dateStart, dateEnd, NationID);
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
    }
}
