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
    /// Interaction logic for WorksWithPage.xaml
    /// </summary>
    public partial class WorksWithPage : Page
    {
        DataManager manager = ((App)Application.Current).manager;
        public WorksWithPage()
        {
            InitializeComponent();
            ReloadWorksWith();
        }

        public void ReloadWorksWith()
        {
            ListView_WorkingWith.DataContext = null;
            ListView_WorkingWith.DataContext = manager.GetWorksWiths();
        }
    }
}
