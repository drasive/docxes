using System;
using System.Diagnostics;
using System.Windows;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Interaction logic for <see cref="App.xaml"/>.
    /// </summary>
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            Window windowToShow = null;

            try {
                base.OnStartup(e);

                // Check for multiple running instances
                if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1) {
                    MessageBox.Show("Eine andere Instanz von Docxes wird bereits ausgeführt!",
                                    "Andere Instanz wird bereits ausgeführt", MessageBoxButton.OK, MessageBoxImage.Information);
                    Shutdown(0);
                }

                // Set the application context
                ApplicationPropertyManager.Application = Application.Current;

                // Set the data directory
                var DataDirectoryPath = ConfigurationReader.GetDataDirectoryPath();
                AppDomain.CurrentDomain.SetData("DataDirectory", DataDirectoryPath);

                // Show the initial window
                windowToShow = new UserInterface.ManageSchools();
                windowToShow.Show();
            }
            catch (Exception ex) {
                Logger.Log(ex);

                if (windowToShow != null) {
                    windowToShow.Close();
                }
                MessageBox.Show("Beim Start von Docxes ist ein Fehler aufgetreten!" + Environment.NewLine +
                                "Überprüfen Sie die Applikationskonfiguration und stellen Sie sicher, dass die Anwendung über die benötigten Berechtigungen auf dem Dateisystem verfügt.",
                                "Fehler beim Start", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown(1);
            }
        }

    }

}
