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
            ReloadEmployees();
        }



        private void ReloadEmployees()
        {
            ListView_Employees.DataContext = null;
            ListView_Employees.DataContext = manager.GetEmployees();
            PopulateSearchCombos();
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

        private void PopulateSearchCombos()
        {
            foreach (Employee employee in manager.employees)
            {
                    ComboBox_Supervisor.ItemsSource = manager.GetSupervisorNames();
                    ComboBox_Branch.ItemsSource = manager.GetBranchNames();
            }
        }

        private void Button_SearchEmployee_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(TextBox_SearchID.Text, out int searchId);
            decimal.TryParse(TextBox_SalaryLow.Text, out decimal searchSalaryLow);
            decimal.TryParse(TextBox_SalaryHigh.Text, out decimal searchSalaryHigh);
            int? supervisorID = null;
            int? branchId = null;
            DateTime? dob = null;
            if (ComboBox_Supervisor.SelectedItem != null)
            {
                supervisorID = ((ComboBoxItem)ComboBox_Supervisor.SelectedItem).GetID();
            }
            if (ComboBox_Branch.SelectedItem != null)
            {
                branchId = ((ComboBoxItem)ComboBox_Branch.SelectedItem).GetID();
            }
            if (DatePicker_DOB.SelectedDate != null)
            {
                dob = (DateTime)DatePicker_DOB.SelectedDate;
            }
            
            Employee searchEmployee = new()
            {
                ID = searchId,
                FirstName = TextBox_SearchName.Text,
                LastName = TextBox_SearchLastName.Text,
                DateOfBirth = dob,
                Gender = TextBox_Gender.Text,
                SupervisorID = supervisorID,
                BranchID = branchId
            };

            ListView_Employees.DataContext = null;
            ListView_Employees.DataContext = manager.SearchEmployees(searchEmployee);

        }
    }
}
