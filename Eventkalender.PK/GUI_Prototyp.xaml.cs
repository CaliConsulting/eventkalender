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
        EventkalenderController controller = new EventkalenderController("Resources/eventkalender-db.xml");

        private List<string> timesList;
        private List<Nation> Nationer;
        private List<Event> Events;
        


        public GUI_Prototyp()
        {
            InitializeComponent();
        }
        public List<string> TimesList
        {
            get
            {
                if (timesList == null)
                {
                    timesList = Utility.GenerateList();
                    return timesList;
                }
                else
                {
                    return timesList;
                }
            }
            set
            {
                timesList = Utility.GenerateList();
            }
        }
        public List<Nation> Nationerlist
        {
            get { return Nationer; }
            set { Nationer = controller.GetNations(); }
        }
        public List<Event> EventList
        {
            get { return Events; }
            set { Events = controller.GetEvents(); }
        }
   
        private void SearchBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }

        private void btn_EraseFromList_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult raderaResultat = MessageBox.Show("Vill ni verkligen ta bort innehållet?", "Radera", MessageBoxButton.YesNo);
            if (raderaResultat == MessageBoxResult.Yes)
            {

            }
            if (raderaResultat == MessageBoxResult.No)
            {

            }
        }

        private void btn_DeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult raderaResultat = MessageBox.Show("Vill ni verkligen ta bort innehållet?", "Radera", MessageBoxButton.YesNo);
            if (raderaResultat == MessageBoxResult.Yes)
            {

            }
            if (raderaResultat == MessageBoxResult.No)
            {

            }
        }

        private void btn_RegNationName_Click(object sender, RoutedEventArgs e)
        {
            if(txtBox_NationName.Text != "")
            {
                controller.AddNation(txtBox_NationName.Text);
                txtBox_NationName.Text = "";
             }
            else
            {
                MessageBox.Show("Inget värde ifyllt");
            }
        }

        private void btn_SrchEvent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_RegsterEvent_Click(object sender, RoutedEventArgs e)
        {
            if(Utility.CheckIfEmpty(txtBox_EventName.Text, cmBox_Nation.Text, dtpick_StartDate.Text, cmb_StartTime.Text, dtpick_EndDate.Text, cmb_EndTime.Text, txtBox_Summary.Text))
            {
                DateTime dateStart = Utility.ToDate(dtpick_StartDate.Text, cmb_StartTime.Text);
                DateTime dateEnd = Utility.ToDate(dtpick_StartDate.Text, cmb_StartTime.Text);
                int NationID = Convert.ToInt32(cmBox_Nation.Text);
                controller.AddEvent(txtBox_EventName.Text, txtBox_Summary.Text, dateStart, dateEnd, NationID);
            }
            else
            {
                MessageBox.Show("Ey, stupido, du glömde fylla i en låda. Capish?!");
            }
        }
    }
}
