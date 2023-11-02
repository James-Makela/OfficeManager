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
    /// Interaction logic for EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        DataManager manager = ((App)Application.Current).manager;
        public EditEmployeeWindow(Employee selectedEmployee)
        {
            InitializeComponent();
            PopulateTextBoxes(selectedEmployee);
            PopulateComboBoxes(selectedEmployee);
        }

        private void PopulateTextBoxes(Employee selectedEmployee)
        {
            TextBox_EmployeeId.Text = selectedEmployee.ID.ToString();
            TextBox_EmployeeFirstName.Text = selectedEmployee.FirstName;
            TextBox_EmployeeLastName.Text = selectedEmployee.LastName;
            DatePicker_EmployeeDateOfBirth.SelectedDate = selectedEmployee.DateOfBirth;
            TextBox_EmployeeGender.Text = selectedEmployee.Gender;
            TextBox_EmployeeSalary.Text = selectedEmployee.Salary.ToString();
        }
        private void PopulateComboBoxes(Employee selectedEmployee)
        {
            List<ComboBoxItem> branchNames = manager.GetBranchNames();
            List<ComboBoxItem> employeeNames = manager.GetEmployeeNames();

            ComboBox_EmployeeBranchID.ItemsSource = branchNames;
            ComboBox_EmployeeBranchID.SelectedItem = branchNames.Find(combo => combo.GetID() == selectedEmployee.BranchID);

            ComboBox_EmployeeSupervisorID.ItemsSource = employeeNames;
            ComboBox_EmployeeSupervisorID.SelectedItem = employeeNames.Find(combo => combo.GetID() == selectedEmployee.SupervisorID);
        }

        private void Button_EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee changedEmployee = CreateEmployeeFromForms();
            if (!manager.ValidEmployeeCheck(changedEmployee))
            {
                MessageBox.Show("Invalid Data Entered");
                return;
            }
            manager.EditEmployee(changedEmployee);
            Close();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox clickedTextbox = (TextBox)sender;
            clickedTextbox.Clear();
            clickedTextbox.GotFocus -= TextBox_GotFocus;
        }

        private Employee CreateEmployeeFromForms()
        {
            int.TryParse(TextBox_EmployeeId.Text, out int id);
            decimal.TryParse(TextBox_EmployeeSalary.Text, out decimal salary);
            int supervisorId = ((ComboBoxItem)ComboBox_EmployeeSupervisorID.SelectedItem).GetID();
            int branchId = ((ComboBoxItem)ComboBox_EmployeeBranchID.SelectedItem).GetID();
            if (DatePicker_EmployeeDateOfBirth.SelectedDate == null)
            {
                return null;
            }


            Employee employee = new Employee()
            {
                ID = id,
                FirstName = TextBox_EmployeeFirstName.Text,
                LastName = TextBox_EmployeeLastName.Text,
                DateOfBirth = (DateTime)DatePicker_EmployeeDateOfBirth.SelectedDate,
                Gender = TextBox_EmployeeGender.Text,
                Salary = salary,
                SupervisorID = supervisorId,
                BranchID = branchId,
                CreatedAt = DateTime.Now,
                LastUpdatedAt = null
            };

            return employee;
        }
    }
}
