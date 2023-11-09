using IOTDatabaseTraveller.DataClasses;
using IOTDatabaseTraveller.Datamanager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IOTDatabaseTraveller
{
    /// <summary>
    /// Interaction logic for AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        DataManager manager = ((App)Application.Current).manager;
        public AddClientWindow()
        {
            InitializeComponent();
            PopulateComboBoxes();
        }

        public void PopulateComboBoxes()
        {
            List<ComboBoxStringIdItem> branchNames = manager.GetBranchNames();
            ComboBox_BranchName.ItemsSource = branchNames;
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_AddClient_Click(object sender, RoutedEventArgs e)
        {
            Client newClient = CreateClientFromForms();
            if (!manager.CheckClientIsValid(newClient))
            {
                MessageBox.Show("Invalid Data Entered");
                return;
            }
            manager.AddClient(newClient);
            Close();
        }

        private Client CreateClientFromForms()
        {
            Client client = new Client()
            {
                ClientName = TextBox_ClientName.Text,
                BranchID = ((ComboBoxStringIdItem)ComboBox_BranchName.SelectedItem).GetID(),
            };
            return client;
        }
    }
}
