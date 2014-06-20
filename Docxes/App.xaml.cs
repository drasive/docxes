using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Interaction logic for <see cref="App.xaml"/>.
    /// </summary>
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            ApplicationPropertyManager.Application = Application.Current;

            // Set the data directory
            var DataDirectoryPath = ConfigurationReader.GetDataDirectoryPath();
            AppDomain.CurrentDomain.SetData("DataDirectory", DataDirectoryPath);

            // Show the initial window
            Window windowToShow = new UserInterface.ManageSchools();
            windowToShow.Show();
        }

    }

}
