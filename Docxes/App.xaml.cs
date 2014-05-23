﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Interaction logic for <see cref="App.xaml"/> 
    /// </summary>
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            ApplicationPropertyManager.Application = Application.Current;

            AppDomain.CurrentDomain.SetData("DataDirectory", @"C:\Privat\Versionsverwaltung\Docxes\Development\Docxes\Data\");

            Window windowToShow = new Interface.ManageSchools();
            windowToShow.Show();
        }

    }

}
