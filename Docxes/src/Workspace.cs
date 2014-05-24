using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VrankenBischof.Docxes {

    // ASK High DB relations dependency a problem?
    public class Workspace {

        protected Teacher teacher;
        protected Subject subject;


        public School School { get; private set; }

        public Teacher Teacher {
            get {
                return teacher;
            }
            set {
                // Clear dependencies
                Subject = null;

                // Set new value
                teacher = value;
            }
        }

        public Subject Subject { get; set; }


        public Workspace(School school) {
            School = school;
        }

    }

}
