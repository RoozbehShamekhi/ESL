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
    
    public partial class Tbl_ExamRemotelyPlan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_ExamRemotelyPlan()
        {
            this.Tbl_Question = new HashSet<Tbl_Question>();
            this.Tbl_UserExamRemotelyPlan = new HashSet<Tbl_UserExamRemotelyPlan>();
            this.Tbl_UserExamRemotelyPlanAccess = new HashSet<Tbl_UserExamRemotelyPlanAccess>();
        }
    
        public int ERP_ID { get; set; }
        public System.Guid ERP_Guid { get; set; }
        public int ERP_GroupCodeID { get; set; }
        public string ERP_Title { get; set; }
        public int ERP_Mark { get; set; }
        public int ERP_PassMark { get; set; }
        public System.DateTime ERP_CreationDate { get; set; }
        public System.DateTime ERP_ModifiedDate { get; set; }
        public bool ERP_IsDelete { get; set; }
    
        public virtual Tbl_Code Tbl_Code { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Question> Tbl_Question { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_UserExamRemotelyPlan> Tbl_UserExamRemotelyPlan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_UserExamRemotelyPlanAccess> Tbl_UserExamRemotelyPlanAccess { get; set; }
    }
}