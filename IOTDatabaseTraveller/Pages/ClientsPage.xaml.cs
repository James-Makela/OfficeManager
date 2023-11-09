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
using IOTDatabaseTraveller.DataClasses;

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

        private void Button_AddClient_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new ();
            addClientWindow.Owner = Application.Current.MainWindow;
            addClientWindow.ShowDialog();
            ReloadClients();
        }

        private void Button_RemoveClient_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationDialog dialog = new();
            dialog.Owner = Application.Current.MainWindow;
            Client? selectedClient = ListView_Clients.SelectedItem as Client;
            if (selectedClient != null)
            {
                bool? dialogResult = dialog.ShowDialog();
                if (dialogResult == true)
                {
                    manager.DeleteClient(selectedClient);
                }
            }
            ReloadClients();
        }

        private void Button_EditClient_Click(object sender, RoutedEventArgs e)
        {
            Client selectedClient = (Client)ListView_Clients.SelectedItem;
            if (selectedClient == null)
            {
                MessageBox.Show("Please select a client to edit");
                return;
            }
            EditClientWindow editClientWindow = new(selectedClient);
            editClientWindow.Owner = Application.Current.MainWindow;
            editClientWindow.ShowDialog();
            ReloadClients();
        }
    }
}
