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
    /// Interaction logic for BranchesPage.xaml
    /// </summary>
    public partial class BranchesPage : Page
    {
        DataManager manager = ((App)Application.Current).manager;

        public BranchesPage()
        {
            InitializeComponent();
            ReloadBranches();
        }

        public void ReloadBranches()
        {
            ListView_Branches.DataContext = null;
            ListView_Branches.DataContext = manager.GetBranches();
        }

        private void Button_AddBranch_Click(object sender, RoutedEventArgs e)
        {
            AddBranchWindow addBranchWindow = new();
            addBranchWindow.Owner = Application.Current.MainWindow;
            addBranchWindow.ShowDialog();
            ReloadBranches();
        }

        private void Button_RemoveBranch_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationDialog dialog = new();
            dialog.Owner = Application.Current.MainWindow;
            Branch? oldBranch = ListView_Branches.SelectedItem as Branch;
            if (oldBranch != null)
            {
                bool? dialogResult = dialog.ShowDialog();
                if (dialogResult == true)
                {
                    manager.DeleteBranch(oldBranch);
                }
            }
            ReloadBranches();
            
        }

        private void Button_EditBranch_Click(object sender, RoutedEventArgs e)
        {
            Branch selectedBranch = (Branch)ListView_Branches.SelectedItem;
            EditBranchWindow editBranchWindow = new(selectedBranch);
            editBranchWindow.Owner = Application.Current.MainWindow;
            editBranchWindow.ShowDialog();
            ReloadBranches();


        }

        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ClearSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
