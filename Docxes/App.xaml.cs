using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Interaction logic for <see cref="App.xaml"/>.
    /// </summary>
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            SplashScreen splashScreen = null;
            Window windowToShow = null;

            try {
                base.OnStartup(e);

                // Check for already running instance
                if (ApplicationRunningHelper.CheckAlreadyRunning()) {
                    Shutdown(0);
                }
                else {
                    // Show the splash screen
                    splashScreen = new SplashScreen("Resources/Images/Splash Screen.png");
                    splashScreen.Show(false);



                    // Initialize
                    // -- Set the application context
                    ApplicationPropertyManager.Application = Application.Current;

                    // -- Set the data directory
                    var dataDirectory = ConfigurationReader.GetDataDirectoryPath();
                    AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);

                    // -- Initialize the database connection
                    var schoolProcessor = new BusinessLogic.SchoolProcessor();
                    schoolProcessor.Get(); // A database query forces the Entity Framework initialization

                    // Close the splash screen
                    splashScreen.Close(new TimeSpan(0));



                    // Show the first window
                    windowToShow = new UserInterface.ManageSchools();
                    windowToShow.Show();
                }
            }
            catch (Exception ex) {
                Logger.Log(ex);
                
                MessageBox.Show("Beim Start von Docxes ist ein Fehler aufgetreten!" + Environment.NewLine +
                                "Überprüfen Sie die Applikationskonfiguration und stellen Sie sicher, dass die Anwendung über die benötigten Berechtigungen auf dem Dateisystem verfügt.",
                                "Fehler beim Start", MessageBoxButton.OK, MessageBoxImage.Error);
                if (splashScreen != null) {
                    splashScreen.Close(new TimeSpan(0));
                }
                if (windowToShow != null) {
                    windowToShow.Close();
                }

                Shutdown(1);
            }
        }

    }

}
