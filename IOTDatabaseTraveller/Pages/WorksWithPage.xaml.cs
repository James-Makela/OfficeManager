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
using System.Windows.Navigation;
using System.Windows.Shapes;
using IOTDatabaseTraveller.Datamanager;

namespace IOTDatabaseTraveller
{
    /// <summary>
    /// Interaction logic for WorksWithPage.xaml
    /// </summary>
    public partial class WorksWithPage : Page
    {
        DataManager manager = ((App)Application.Current).manager;
        public WorksWithPage()
        {
            InitializeComponent();
            ReloadWorksWith();
        }

        public void ReloadWorksWith()
        {
            ListView_WorkingWith.DataContext = null;
            ListView_WorkingWith.DataContext = manager.GetWorksWiths();
            ComboBox_SearchClient.ItemsSource = manager.GetClientNames();
            ComboBox_SearchEmployee.ItemsSource = manager.GetEmployeeNames();
        }

        private void Button_SearchWorksWith_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ClearSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_AddWorksWith_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_RemoveWorksWith_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_EditWorksWith_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
