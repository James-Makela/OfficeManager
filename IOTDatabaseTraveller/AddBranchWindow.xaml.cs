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
using IOTDatabaseTraveller.DataClasses;

namespace IOTDatabaseTraveller
{
    /// <summary>
    /// Interaction logic for AddBranchWindow.xaml
    /// </summary>
    public partial class AddBranchWindow : Window
    {
        DataManager manager = ((App)Application.Current).manager;
        public AddBranchWindow()
        {
            InitializeComponent();
            PopulateComboBoxes();
        }

        private void PopulateComboBoxes()
        {
            ComboBox_NewBranchManager.ItemsSource = manager.GetEmployeeNames();
        }

        private void Button_AddBranch_Click(object sender, RoutedEventArgs e)
        {
            Branch newBranch = CreateBranchFromForms();
            if (!manager.CheckBranchIsValid(newBranch))
            {
                MessageBox.Show("Invalid Data Entered");
                return;
            }
            manager.AddBranch(newBranch);
            Close();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private Branch CreateBranchFromForms()
        {
            Branch newBranch = new()
            {
                BranchName = TextBox_BranchName.Text,
                ManagerID = ((ComboBoxStringIdItem)ComboBox_NewBranchManager.SelectedItem).GetID(),
                ManagerStartedAt = DatePicker_ManagerStartDate.SelectedDate,
                CreatedAt = DateTime.Now
            };
            return newBranch;
        }
    }
}
