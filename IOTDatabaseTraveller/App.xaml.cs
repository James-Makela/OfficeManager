﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using IOTDatabaseTraveller.Datamanager;

namespace IOTDatabaseTraveller
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public DataManager manager = new();
    }
}
