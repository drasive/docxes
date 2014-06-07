using System;
using System.Windows;

namespace VrankenBischof.Docxes {

    // TODO: Move DataPath into AppConfig

    /// <summary>
    /// Interaction logic for <see cref="App.xaml"/>.
    /// </summary>
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            ApplicationPropertyManager.Application = Application.Current;

            var userprofile = Environment.GetEnvironmentVariable("Userprofile");
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(userprofile, @"SkyDrive\Programming\Windows Desktop\Docxes\Development\Docxes\Data\"));

            Window windowToShow = new UserInterface.ManageSchools();
            windowToShow.Show();
        }

    }

}
