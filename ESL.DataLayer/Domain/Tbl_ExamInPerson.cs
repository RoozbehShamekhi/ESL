//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ESL.DataLayer.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_ExamInPerson
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_ExamInPerson()
        {
            this.Tbl_UserExamInPerson = new HashSet<Tbl_UserExamInPerson>();
        }
    
        public int EIP_ID { get; set; }
        public System.Guid EIP_Guid { get; set; }
        public int EIP_Capacity { get; set; }
        public string EIP_Title { get; set; }
        public int EIP_TotalMark { get; set; }
        public int EIP_PassMark { get; set; }
        public int EIP_Cost { get; set; }
        public System.DateTime EIP_CreationDate { get; set; }
        public System.DateTime EIP_ModifiedDate { get; set; }
        public bool EIP_IsDelete { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_UserExamInPerson> Tbl_UserExamInPerson { get; set; }
    }
}