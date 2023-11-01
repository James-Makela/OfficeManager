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
    /// Interaction logic for ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        DataManager manager = ((App)Application.Current).manager;
        public ClientsPage()
        {
            InitializeComponent();
            ReloadClients();
        }

        private void ReloadClients()
        {
            ListView_Clients.DataContext = null;
            ListView_Clients.DataContext = manager.GetClients();
        }
    }
}
