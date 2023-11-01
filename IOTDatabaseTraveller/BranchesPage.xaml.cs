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
    }
}
