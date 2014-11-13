using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VrankenBischof.Docxes.Test {

    internal class App {

        internal static void Initialize() {
            var applicationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataDirectory = Path.Combine(applicationDirectory, @"Data\");

            AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);
        }

    }

}
