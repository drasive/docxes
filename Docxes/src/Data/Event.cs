//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VrankenBischof.Docxes
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public Event()
        {
            this.Documents = new HashSet<Document>();
            this.Notes = new HashSet<Note>();
            this.Grades = new HashSet<Grade>();
        }
    
        public int Id { get; private set; }
        public int SubjectId { get; private set; }
        public string Name { get; private set; }
        public System.DateTime Date { get; private set; }
        public int Type { get; private set; }
    
        public virtual Subject Subject { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
