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
using IOTDatabaseTraveller.DataClasses;
using IOTDatabaseTraveller.Datamanager;

namespace IOTDatabaseTraveller
{
    /// <summary>
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        DataManager manager = ((App)Application.Current).manager;
        public AddEmployeeWindow()
        {
            InitializeComponent();
            PopulateComboBoxes();
        }

        private void PopulateComboBoxes()
        {
            ComboBox_EmployeeBranchID.ItemsSource = manager.GetBranchNames(false);
            ComboBox_EmployeeSupervisorID.ItemsSource = manager.GetEmployeeNames(false);
        }

        private void Button_AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee newEmployee = CreateEmployeeFromForms();
            if (!manager.ValidEmployeeCheck(newEmployee))
            {
                MessageBox.Show("Invalid Data Entered");
                return;
            }
            manager.AddEmployee(newEmployee);
            Close();
        }

        private Employee CreateEmployeeFromForms()
        {
            decimal.TryParse(TextBox_EmployeeSalary.Text, out decimal salary);
            int supervisorId = ((ComboBoxStringIdItem)ComboBox_EmployeeSupervisorID.SelectedItem).GetID();
            int branchId = ((ComboBoxStringIdItem)ComboBox_EmployeeBranchID.SelectedItem).GetID();
            if (TextBox_EmployeeDateOfBirth.SelectedDate == null)
            {
                return null;
            }


            Employee employee = new Employee()
            {
                FirstName = TextBox_EmployeeFirstName.Text,
                LastName = TextBox_EmployeeLastName.Text,
                DateOfBirth = (DateTime)TextBox_EmployeeDateOfBirth.SelectedDate,
                Gender = TextBox_EmployeeGender.Text,
                Salary = salary,
                SupervisorID = supervisorId,
                BranchID = branchId,
                CreatedAt = DateTime.Now,
                LastUpdatedAt = null
            };
            
            return employee;
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
