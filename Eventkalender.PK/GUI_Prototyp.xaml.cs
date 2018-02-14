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
        List<string> timeList = new List<string> {"00:00", "00:30", "01;00", "01;30", "02:00", "02:30", "03;00", "03;30", "04:00", "04:30", "05;00", "05;30",
                                                           "06:00", "06:30", "07;00", "07;30","08:00", "08:30", "09;00", "09;30","10:00", "10:30", "11;00", "11;30",
                                                           "12:00", "12:30", "13;00", "13;30","14:00", "14:30", "15;00", "15;30","16:00", "16:30", "17;00", "17;30",
                                                           "18:00", "18:30", "19;00", "19;30","20:00", "20:30", "21;00", "21;30","22:00", "22:30", "23;00", "23;30"};
        public GUI_Prototyp()
        {
            InitializeComponent();
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
        private void cmb_SortEvents_Selected(object sender, RoutedEventArgs e)
        {
            if(cmb_SortEvents.SelectedValue == Datum)
            {
                
            }
        }
    }
}
