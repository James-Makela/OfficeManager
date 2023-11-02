﻿using IOTDatabaseTraveller.Datamanager;
using IOTDatabaseTraveller.DataClasses;
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
    /// Interaction logic for EmployeeSalesPage.xaml
    /// </summary>
    public partial class EmployeeSalesPage : Page
    {
        DataManager manager = ((App)Application.Current).manager;
        public EmployeeSalesPage()
        {
            InitializeComponent();
            ReloadEmployeeSales();
        }

        private void ReloadEmployeeSales()
        {
            ListView_EmployeeSales.DataContext = null;
            ListView_EmployeeSales.DataContext = manager.GetEmployeeSales();
            PopulateEmployeeCombo();
        }

        private void PopulateEmployeeCombo()
        {
            ComboBox_Employees.ItemsSource = manager.GetEmployeeNames();
            ComboBox_Employees.SelectedItem = 0;
        }
    }
}
