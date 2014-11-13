namespace VrankenBischof.Docxes {

    /// <summary>
    /// Contains the business objects that are currently managed by the user.
    /// </summary>
    internal class Workspace {

        protected Teacher teacher;


        internal School School { get; private set; }

        internal Teacher Teacher {
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

        internal Subject Subject { get; set; }


        internal Workspace(School school) {
            School = school;
        }

    }

}
