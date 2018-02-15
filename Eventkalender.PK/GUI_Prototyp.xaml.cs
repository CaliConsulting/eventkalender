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
        public GUI_Prototyp()
        {
            InitializeComponent();
        }
        public List<string> GenerateList()
        {
            List<string> times = new List<string>();
            for (int i = 0; i < 48; i++)
            {
                int hour = (int)Math.Floor(i / 2d);
                string strHour = hour.ToString();
                if (hour < 10)
                {
                    strHour = "0" + strHour;
                }
                string res = strHour + ":";
                if (i % 2 == 1)
                {
                    res += "3";
                }
                else
                {
                    res += "0";
                }
                res += "0";
                times.Add(res);
            }
            return times;
        }
        public List<string> timesList
        {
            get
            {
                if (timesList == null)
                {
                    timesList = GenerateList();
                    return timesList;
                }
                else
                {
                    return timesList;
                }
            }
            set
            {
                timesList = GenerateList();
            }
        }
        private void SearchBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }

        private void datagridEvents_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void datagridEvents_Selected(object sender, RoutedEventArgs e)
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
/*        private void cmb_SortEvents_Selected(object sender, RoutedEventArgs e)
        {
            if(cmb_SortEvents.SelectedValue == Datum)
            {
                
            }
        }*/
    }
}
