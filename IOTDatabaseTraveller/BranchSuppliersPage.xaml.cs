﻿using IOTDatabaseTraveller.Datamanager;
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
    /// Interaction logic for BranchSuppliersPage.xaml
    /// </summary>
    public partial class BranchSuppliersPage : Page
    {
        DataManager manager = ((App)Application.Current).manager;

        public BranchSuppliersPage()
        {
            InitializeComponent();
            ReloadBranchSuppliers();
        }

        public void ReloadBranchSuppliers()
        {
            ListView_BranchSuppliers.DataContext = null;
            ListView_BranchSuppliers.DataContext = manager.GetBranchSuppliers();
        }
    }
}
