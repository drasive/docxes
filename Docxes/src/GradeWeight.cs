using System.ComponentModel;

namespace VrankenBischof.Docxes {

    /// <summary>
    /// Represents a weight of a grade.
    /// </summary>
    public enum GradeWeight {
        [Description("1")]
        Full,
        [Description("1/2")]
        Half,
        [Description("1/4")]
        Quarter,
        [Description("2/1")]
        Double
    }

}
