//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VrankenBischof.Docxes
{
    using System;
    using System.Collections.Generic;
    
    public partial class Subject
    {
        public Subject()
        {
            this.Events = new HashSet<Event>();
            this.Documents = new HashSet<Document>();
            this.Notes = new HashSet<Note>();
            this.Grades = new HashSet<Grade>();
        }
    
        public int Id { get; private set; }
        public int TeacherId { get; private set; }
        public string Name { get; private set; }
    
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
