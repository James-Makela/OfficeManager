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
    /// Interaction logic for EditBranchSupplier.xaml
    /// </summary>
    public partial class EditBranchSupplierWindow: Window
    {
        DataManager manager = ((App)Application.Current).manager;
        public EditBranchSupplierWindow(BranchSupplier changedBranchSupplier)
        {
            InitializeComponent();
            PopulateComboBox(changedBranchSupplier);
            TextBox_ProductSupplied.Text = changedBranchSupplier.ProductSupplied;
            TextBox_SupplierName.Text = changedBranchSupplier.SupplierName;
            TextBox_SupplierID.Text = changedBranchSupplier.SupplierID.ToString();
        }

        private void PopulateComboBox(BranchSupplier changedBranchSupplier)
        {
            List<ComboBoxStringIdItem> branchNames = manager.GetBranchNames();
            ComboBox_BranchName.ItemsSource = branchNames;
            ComboBox_BranchName.SelectedItem = branchNames.Find(combo => combo.GetID() == changedBranchSupplier.BranchID);
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_SaveBranchSupplier_Click(object sender, RoutedEventArgs e)
        {
            BranchSupplier changedBranchSupplier = CreateBranchSupplierFromForms();
            if (!manager.CheckBranchSupplierIsValid(changedBranchSupplier))
            {
                MessageBox.Show("Invalid Data Entered");
                return;
            }
            manager.EditBranchSupplier(changedBranchSupplier);
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
