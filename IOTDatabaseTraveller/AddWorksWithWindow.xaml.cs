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
    /// Interaction logic for AddWorksWithWindow.xaml
    /// </summary>
    public partial class AddWorksWithWindow : Window
    {
        DataManager manager = ((App)Application.Current).manager;
        public AddWorksWithWindow()
        {
            InitializeComponent();
            ComboBox_ClientName.ItemsSource = manager.GetClientNames();
            ComboBox_Employee.ItemsSource= manager.GetEmployeeNames();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_AddBranch_Click(object sender, RoutedEventArgs e)
        {
            WorksWith newWorksWith = CreateWorksWithFromForms();
            if (!manager.CheckWorksWithIsValid(newWorksWith))
            {
                MessageBox.Show("Invalid Data entered");
                return;
            }
            manager.AddWorksWith(newWorksWith);
            Close();
        }

        private WorksWith CreateWorksWithFromForms()
        {
            decimal.TryParse(TextBox_SalesAmount.Text, out decimal totalSales);
            WorksWith newWorksWith = new()
            {
                EmployeeID = ((ComboBoxStringIdItem)ComboBox_Employee.SelectedItem).GetID(),
                ClientID = ((ComboBoxStringIdItem)ComboBox_ClientName.SelectedItem).GetID(),
                TotalSales = totalSales
            };
            return newWorksWith;
        }
    }
}
