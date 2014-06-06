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

            AppDomain.CurrentDomain.SetData("DataDirectory", @"C:\Privat\Versionsverwaltung\Docxes\Development\Docxes\Data\");

            Window windowToShow = new Interface.ManageSchools();
            windowToShow.Show();
        }

    }

}
