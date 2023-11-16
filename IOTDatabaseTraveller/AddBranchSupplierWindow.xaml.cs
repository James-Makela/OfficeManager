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
    /// Interaction logic for AddBranchSupplier.xaml
    /// </summary>
    public partial class AddBranchSupplierWindow : Window
    {
        DataManager manager = ((App)Application.Current).manager;
        public AddBranchSupplierWindow()
        {
            InitializeComponent();
            PopulateComboBoxes();
            
        }

        private void PopulateComboBoxes()
        {
            ComboBox_BranchName.ItemsSource = manager.GetBranchNames();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_AddBranchSupplier_Click(object sender, RoutedEventArgs e)
        {
            BranchSupplier newBranchSupplier = CreateBranchSupplierFromForms();
            if (!manager.CheckBranchSupplierIsValid(newBranchSupplier))
            {
                MessageBox.Show("Invalid Data Entered");
                return;
            }
            manager.AddBranchSupplier(newBranchSupplier);
            Close();
        }

        private BranchSupplier CreateBranchSupplierFromForms()
        {
            int.TryParse(TextBox_SupplierID.Text, out int id);
            BranchSupplier newBranchSupplier = new()
            {
                SupplierID = id,
                BranchID = ((ComboBoxStringIdItem)ComboBox_BranchName.SelectedItem).GetID(),
                SupplierName = TextBox_SupplierName.Text,
                ProductSupplied = TextBox_ProductSupplied.Text,
            };
            return newBranchSupplier;
        }
    }
}
