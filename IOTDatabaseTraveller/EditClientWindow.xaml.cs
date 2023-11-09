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
    /// Interaction logic for EditClientWindow.xaml
    /// </summary>
    public partial class EditClientWindow : Window
    {
        DataManager manager = ((App)Application.Current).manager;
        public EditClientWindow(Client clientToEdit)
        {
            InitializeComponent();
            PopulateComboBoxes(clientToEdit);
            TextBox_ClientID.Text = clientToEdit.ID.ToString();
            TextBox_ClientName.Text = clientToEdit.ClientName;
        }

        public void PopulateComboBoxes(Client clientToEdit)
        {
            List<ComboBoxStringIdItem> branchNames = manager.GetBranchNames();
            ComboBox_BranchName.ItemsSource = branchNames;
            ComboBox_BranchName.SelectedItem = branchNames.Find(combo => combo.GetID() == clientToEdit.BranchID);
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_SaveClient_Click(object sender, RoutedEventArgs e)
        {
            Client changedClient = CreateClientFromForms();
            if (!manager.CheckClientIsValid(changedClient))
            {
                MessageBox.Show("Invalid Data Entered");
                return;
            }
            manager.EditClient(changedClient);
            Close();
        }

        private Client CreateClientFromForms()
        {
            int.TryParse(TextBox_ClientID.Text, out int id);
            Client client = new Client()
            {
                ID = id,
                ClientName = TextBox_ClientName.Text,
                BranchID = ((ComboBoxStringIdItem)ComboBox_BranchName.SelectedItem).GetID(),
            };
            return client;
        }
    }
}
