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
using IOTDatabaseTraveller.DataClasses;
using IOTDatabaseTraveller.Datamanager;

namespace IOTDatabaseTraveller
{
    /// <summary>
    /// Interaction logic for EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {

        DataManager manager = ((App)Application.Current).manager;

        public EmployeesPage()
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
            ConfirmationDialog dialog = new();
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
            ComboBox_Supervisor.ItemsSource = manager.GetSupervisorNames();
            ComboBox_Supervisor.SelectedIndex = 0;
            ComboBox_Branch.ItemsSource = manager.GetBranchNames();
            ComboBox_Branch.SelectedIndex = 0;
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
                supervisorID = ((ComboBoxStringIdItem)ComboBox_Supervisor.SelectedItem).GetID();
            }
            if (ComboBox_Branch.SelectedItem != null)
            {
                branchId = ((ComboBoxStringIdItem)ComboBox_Branch.SelectedItem).GetID();
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

            if (searchEmployee.ID == 0 && searchEmployee.FirstName == "" && searchEmployee.LastName == "" &&
                searchEmployee.Gender == "" && searchEmployee.SupervisorID == 0 && searchEmployee.BranchID == 0 &&
                searchSalaryLow == 0 && searchSalaryHigh == 0)
            {
                ReloadEmployees();
                return;
            }

            ListView_Employees.DataContext = null;
            ListView_Employees.DataContext = manager.SearchEmployees(searchEmployee, searchSalaryLow, searchSalaryHigh);

        }

        private void Button_ClearSearch_Click(object sender, RoutedEventArgs e)
        {
            TextBox_SearchID.Text = null;
            TextBox_SearchName.Text = null;
            TextBox_SearchLastName.Text = null;
            TextBox_Gender.Text = null;
            TextBox_SalaryLow.Text = null;
            TextBox_SalaryHigh.Text = null;
            ComboBox_Supervisor.SelectedIndex = 0;
            ComboBox_Branch.SelectedIndex = 0;
            ReloadEmployees();
        }

        private void Button_EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (ListView_Employees.SelectedItem == null)
            {
                MessageBox.Show("You must select an employee to edit");
                return;
            }
            Employee selectedEmployee = (Employee)ListView_Employees.SelectedItem;
            EditEmployeeWindow editEmployeeWindow = new(selectedEmployee);
            editEmployeeWindow.Owner = Application.Current.MainWindow;
            editEmployeeWindow.ShowDialog();
            ReloadEmployees();
        }
    }
}
