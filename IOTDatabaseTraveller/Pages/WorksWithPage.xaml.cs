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
using IOTDatabaseTraveller.DataClasses;

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
            ComboBox_SearchClient.SelectedIndex = 0;
            ComboBox_SearchEmployee.ItemsSource = manager.GetEmployeeNames();
            ComboBox_SearchEmployee.SelectedIndex = 0;
        }

        private void Button_SearchWorksWith_Click(object sender, RoutedEventArgs e)
        {
            WorksWith searchWorksWithItem = new()
            {
                ClientID = ((ComboBoxStringIdItem)ComboBox_SearchClient.SelectedItem).GetID(),
                EmployeeID = ((ComboBoxStringIdItem)ComboBox_SearchEmployee.SelectedItem).GetID(),
            };

            if ((searchWorksWithItem.EmployeeID == 0) && (searchWorksWithItem.ClientID == 0))
            {
                ReloadWorksWith();
                return;
            }

            ListView_WorkingWith.DataContext = null;
            ListView_WorkingWith.DataContext = manager.SearchWorksWith(searchWorksWithItem);
        }

        private void Button_ClearSearch_Click(object sender, RoutedEventArgs e)
        {
            ReloadWorksWith();
        }

        private void Button_AddWorksWith_Click(object sender, RoutedEventArgs e)
        {
            AddWorksWithWindow addWorksWithWindow = new();
            addWorksWithWindow.Owner = Application.Current.MainWindow;
            addWorksWithWindow.ShowDialog();
            ReloadWorksWith();
        }

        private void Button_RemoveWorksWith_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationDialog dialog = new();
            dialog.Owner = Application.Current.MainWindow;
            WorksWith? oldWorksWith = ListView_WorkingWith.SelectedItem as WorksWith;
            if (oldWorksWith != null)
            {
                bool? dialogResult = dialog.ShowDialog();
                if (dialogResult == true)
                {
                    manager.DeleteWorksWith(oldWorksWith);
                }
            }
            ReloadWorksWith();
        }

        private void Button_EditWorksWith_Click(object sender, RoutedEventArgs e)
        {
            WorksWith? selectedWorksWith = ListView_WorkingWith.SelectedItem as WorksWith;
            if (selectedWorksWith == null)
            {
                MessageBox.Show("Please select a row to edit");
                return;
            }
            EditWorksWithWindow editWorksWithWindow = new(selectedWorksWith);
            editWorksWithWindow.Owner = Application.Current.MainWindow;
            editWorksWithWindow.ShowDialog();
            ReloadWorksWith();
        }
    }
}
