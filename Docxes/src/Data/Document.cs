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
    
    public partial class Document
    {
        public int Id { get; private set; }
        public int SubjectId { get; private set; }
        public string FilePath { get; private set; }
    
        public virtual Subject Subject { get; set; }
    }
}
