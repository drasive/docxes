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
    
    public partial class School
    {
        public School()
        {
            this.Teachers = new HashSet<Teacher>();
        }
    
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Comment { get; private set; }
    
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}