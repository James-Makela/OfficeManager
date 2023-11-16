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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IOTDatabaseTraveller
{
    /// <summary>
    /// Interaction logic for BranchSuppliersPage.xaml
    /// </summary>
    public partial class BranchSuppliersPage : Page
    {
        DataManager manager = ((App)Application.Current).manager;

        public BranchSuppliersPage()
        {
            InitializeComponent();
            ReloadBranchSuppliers();
        }

        public void ReloadBranchSuppliers()
        {
            ListView_BranchSuppliers.DataContext = null;
            ListView_BranchSuppliers.DataContext = manager.GetBranchSuppliers();
            ComboBox_Branch.ItemsSource = manager.GetBranchNames();
            ComboBox_Branch.SelectedIndex = 0;
        }

        private void Button_AddBranchSupplier_Click(object sender, RoutedEventArgs e)
        {
            AddBranchSupplierWindow addBranchSupplierWindow = new();
            addBranchSupplierWindow.Owner = Application.Current.MainWindow;
            addBranchSupplierWindow.ShowDialog();
            ReloadBranchSuppliers();
        }

        private void Button_RemoveBranchSupplier_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationDialog dialog = new();
            dialog.Owner = Application.Current.MainWindow;
            BranchSupplier? oldBranchSupplier = ListView_BranchSuppliers.SelectedItem as BranchSupplier;
            if (oldBranchSupplier != null)
            {
                bool? dialogResult = dialog.ShowDialog();
                if (dialogResult == true)
                {
                    manager.RemoveBranchSupplier(oldBranchSupplier);
                }
            }
            ReloadBranchSuppliers();
        }

        private void Button_EditBranchSupplier_Click(object sender, RoutedEventArgs e)
        {
            BranchSupplier selectedBranchSupplier = (BranchSupplier)ListView_BranchSuppliers.SelectedItem;
            if (selectedBranchSupplier == null)
            {
                MessageBox.Show("Please select a branch supplier to edit");
                return;
            }
            EditBranchSupplierWindow editBranchSupplierWindow = new(selectedBranchSupplier);
            editBranchSupplierWindow.Owner = Application.Current.MainWindow;
            editBranchSupplierWindow.ShowDialog();
            ReloadBranchSuppliers();
        }

        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            int? branchID = null;
            if (ComboBox_Branch.SelectedItem != null)
            {
                branchID = ((ComboBoxStringIdItem)ComboBox_Branch.SelectedItem).GetID();
            }
            if (TextBox_SearchProduct.Text == "" && TextBox_SearchSupplierName.Text == "" && branchID == null)
            {
                ReloadBranchSuppliers();
                return;
            }
            BranchSupplier searchBranchSupplier = new()
            {
                BranchID = branchID,
                SupplierName = TextBox_SearchSupplierName.Text,
                ProductSupplied = TextBox_SearchProduct.Text
            };
            ListView_BranchSuppliers.DataContext = null;
            ListView_BranchSuppliers.DataContext = manager.SearchBranchSuppliers(searchBranchSupplier);
        }

        private void Button_ClearSearch_Click(object sender, RoutedEventArgs e)
        {
            TextBox_SearchProduct.Clear();
            TextBox_SearchSupplierName.Clear();
            ComboBox_Branch.SelectedIndex = 0;
            ReloadBranchSuppliers();
        }
    }
}
