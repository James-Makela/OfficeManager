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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Page homePage = new HomePage();
            PageHolder.Content = homePage;
        }

        private void Button_ShowEmployees_Click(object sender, RoutedEventArgs e)
        {
            Page employees = new EmployeesPage();
            Image_SmallLogo.Visibility = Visibility.Visible;
            Label_PageTitle.Content = "Employees";
            PageHolder.Content = employees;
        }

        private void Button_Home_Click(object sender, RoutedEventArgs e)
        {
            Page homePage = new HomePage();
            Image_SmallLogo.Visibility = Visibility.Hidden;
            Label_PageTitle.Content = "Home";
            PageHolder.Content = homePage;
        }

        private void Button_ShowBranches_Click(object sender, RoutedEventArgs e)
        {
            Page branchesPage = new BranchesPage();
            Image_SmallLogo.Visibility = Visibility.Visible;
            Label_PageTitle.Content = "Branches";
            PageHolder.Content = branchesPage;
        }

        private void Button_ShowWorksWith_Click(object sender, RoutedEventArgs e)
        {
            Page workingWith = new WorksWithPage();
            Image_SmallLogo.Visibility = Visibility.Visible;
            Label_PageTitle.Content = "Working With";
            PageHolder.Content = workingWith;
        }

        private void Button_ShowClients_Click(object sender, RoutedEventArgs e)
        {
            Page clients = new ClientsPage();
            Image_SmallLogo.Visibility = Visibility.Visible;
            Label_PageTitle.Content = "Clients";
            PageHolder.Content = clients;
        }

        private void Button_ShowBranchSuppliers_Click(object sender, RoutedEventArgs e)
        {
            Page branchSuppliers = new BranchSuppliersPage();
            Image_SmallLogo.Visibility = Visibility.Visible;
            Label_PageTitle.Content = "Branch Suppliers";
            PageHolder.Content = branchSuppliers;
        }
    }
}
