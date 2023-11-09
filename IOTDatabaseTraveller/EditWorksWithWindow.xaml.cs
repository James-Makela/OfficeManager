using IOTDatabaseTraveller.DataClasses;
using IOTDatabaseTraveller.Datamanager;
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

namespace IOTDatabaseTraveller
{
    /// <summary>
    /// Interaction logic for EditWorksWithWindow.xaml
    /// </summary>
    public partial class EditWorksWithWindow : Window
    {
        DataManager manager = ((App)Application.Current).manager;
        public EditWorksWithWindow(WorksWith worksWithToChange)
        {
            InitializeComponent();
            PopulateComboBoxes(worksWithToChange);
        }

        private void PopulateComboBoxes(WorksWith worksWithToChange)
        {
            List<ComboBoxStringIdItem> clientNames = manager.GetClientNames();
            List<ComboBoxStringIdItem> employeeNames = manager.GetEmployeeNames();
            ComboBox_ClientName.ItemsSource = clientNames;
            ComboBox_ClientName.SelectedItem = clientNames.Find(combo => combo.GetID() == worksWithToChange.ClientID);
            ComboBox_Employee.ItemsSource = employeeNames;
            ComboBox_Employee.SelectedItem = employeeNames.Find(combo => combo.GetID() == worksWithToChange.EmployeeID);
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_EditBranch_Click(object sender, RoutedEventArgs e)
        {
            WorksWith changedWorksWith = CreateWorksWithFromForms();
            if (!manager.CheckWorksWithIsValid(changedWorksWith))
            {
                MessageBox.Show("Invalid Data Entered");
                return;
            }
            manager.EditWorksWith(changedWorksWith);
            Close();
            
        }

        WorksWith CreateWorksWithFromForms()
        {
            decimal.TryParse(TextBox_SalesAmount.Text, out decimal totalSales);
            WorksWith worksWith = new WorksWith()
            {
                ClientID = ((ComboBoxStringIdItem)ComboBox_ClientName.SelectedItem).GetID(),
                EmployeeID = ((ComboBoxStringIdItem)ComboBox_Employee.SelectedItem).GetID(),
                TotalSales = totalSales
            };
            return worksWith;
        }
    }
}
