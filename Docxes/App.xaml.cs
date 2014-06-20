using System;
using System.Windows;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Interaction logic for <see cref="App.xaml"/>.
    /// </summary>
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            try {
                base.OnStartup(e);

                // Set the application context
                ApplicationPropertyManager.Application = Application.Current;

                // Set the data directory
                var DataDirectoryPath = ConfigurationReader.GetDataDirectoryPath();
                AppDomain.CurrentDomain.SetData("DataDirectory", DataDirectoryPath);

                // Show the initial window
                Window windowToShow = new UserInterface.ManageSchools();
                windowToShow.Show();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                MessageBox.Show("Beim Start von Docxes ist ein Fehler aufgetreten." + Environment.NewLine +
                                "Überprüfen Sie die Applikationskonfiguration und stellen Sie sicher, dass die Anwendung über die benötigten Berechtigungen auf dem Dateisystem verfügt.",
                                "Fehler beim Start", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown(1);
            }
        }

    }

}
