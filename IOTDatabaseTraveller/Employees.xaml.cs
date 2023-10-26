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

namespace IOTDatabaseTraveller
{
    /// <summary>
    /// Interaction logic for Employees.xaml
    /// </summary>
    public partial class Employees : Page
    {

        DataManager manager = ((App)Application.Current).manager;
        public Employees()
        {
            InitializeComponent();
        }

        private void Button_ReloadData_Click(object sender, RoutedEventArgs e)
        {
            ReloadEmployees();
        }

        private void ReloadEmployees()
        {
            ListView_Employees.DataContext = null;
            ListView_Employees.DataContext = manager.GetEmployees();
        }

        private void Button_AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new();
            addEmployeeWindow.Owner = Application.Current.MainWindow;
            addEmployeeWindow.ShowDialog();
            ReloadEmployees();
        }

        private void Button_RemoveEmployee_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ConfirmationDialog();
            dialog.Owner = Application.Current.MainWindow;
            Employee? selectedEmployee = ListView_Employees.SelectedItem as Employee;
            if (selectedEmployee != null)
            {
                bool? dialogResult = dialog.ShowDialog();
                if (dialogResult == true)
                {
                    manager.RemoveEmployee(selectedEmployee);
                }
                
            }
            ReloadEmployees();
        }

        private void Button_SearchEmployee_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
