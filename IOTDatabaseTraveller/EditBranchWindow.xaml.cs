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
using IOTDatabaseTraveller.Datamanager;

namespace IOTDatabaseTraveller
{
    /// <summary>
    /// Interaction logic for EditBranchWindow.xaml
    /// </summary>
    public partial class EditBranchWindow : Window
    {
        DataManager manager = ((App)Application.Current).manager;
        public EditBranchWindow(Branch branchToEdit)
        {
            InitializeComponent();
            PopulateComboBox(branchToEdit);
            PopulateForms(branchToEdit);
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_EditBranch_Click(object sender, RoutedEventArgs e)
        {
            Branch changedBranch = CreateBranchFromForms();
            if (!manager.CheckBranchIsValid(changedBranch))
            {
                MessageBox.Show("Invalid Data Entered");
                return;
            }
            manager.EditBranch(changedBranch);
            Close();
        }

        private Branch CreateBranchFromForms()
        {
            int.TryParse(TextBox_BranchID.Text, out int id);
            Branch editedBranch = new()
            {
                ID = id,
                BranchName = TextBox_BranchName.Text,
                ManagerID = ((ComboBoxStringIdItem)ComboBox_NewBranchManager.SelectedItem).GetID(),
                ManagerStartedAt = DatePicker_ManagerStartDate.SelectedDate,
                CreatedAt = DateTime.Now
            };
            return editedBranch;
        }

        private void PopulateComboBox(Branch branchToEdit)
        {
            List<ComboBoxStringIdItem> employeeNames = manager.GetEmployeeNames();
            ComboBox_NewBranchManager.ItemsSource = employeeNames;
            ComboBox_NewBranchManager.SelectedItem = employeeNames.Find(combo => combo.GetID() == branchToEdit.ManagerID);
        }

        private void PopulateForms(Branch branchToEdit)
        {
            TextBox_BranchID.Text = branchToEdit.ID.ToString();
            TextBox_BranchName.Text = branchToEdit.BranchName;
            DatePicker_ManagerStartDate.SelectedDate = branchToEdit.ManagerStartedAt;
        }
    }
}
