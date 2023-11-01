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
        }

        private void PopulateTextBoxes(Employee selectedEmployee)
        {
            TextBox_EmployeeId.Text = selectedEmployee.ID.ToString();
            TextBox_EmployeeFirstName.Text = selectedEmployee.FirstName;
            TextBox_EmployeeLastName.Text = selectedEmployee.LastName;
            DatePicker_EmployeeDateOfBirth.SelectedDate = selectedEmployee.DateOfBirth;
            TextBox_EmployeeGender.Text = selectedEmployee.Gender;
            TextBox_EmployeeSalary.Text = selectedEmployee.Salary.ToString();
            TextBox_EmployeeSupervisorID.Text = selectedEmployee.SupervisorID.ToString();
            TextBox_EmployeeBranchID.Text = selectedEmployee.BranchID.ToString();
        }

        private void Button_EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee changedEmployee = CreateEmployeeFromForms();
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
            int.TryParse(TextBox_EmployeeSupervisorID.Text, out int supervisorId);
            int.TryParse(TextBox_EmployeeBranchID.Text, out int branchId);
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
