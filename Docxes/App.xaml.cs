using System;
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

            var userprofile = Environment.GetEnvironmentVariable("Userprofile");
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(userprofile, @"SkyDrive\Programming\Windows Desktop\Docxes\Development\Docxes\Data\"));

            Window windowToShow = new Interface.ManageSchools();
            windowToShow.Show();
        }

    }

}
