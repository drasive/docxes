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
    
    public partial class Grade
    {
        public int Id { get; private set; }
        public int SubjectId { get; private set; }
        public decimal Value { get; private set; }
        public int Weight { get; private set; }
        public string Comment { get; private set; }
    
        public virtual Subject Subject { get; set; }
    }
}