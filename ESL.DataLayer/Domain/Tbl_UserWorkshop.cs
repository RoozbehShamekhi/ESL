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
    
    public partial class Tbl_UserWorkshop
    {
        public int UW_ID { get; set; }
        public System.Guid UW_Guid { get; set; }
        public int UW_UserID { get; set; }
        public int UW_WPID { get; set; }
        public Nullable<int> UW_PaymentID { get; set; }
        public bool UW_IsPresent { get; set; }
        public System.DateTime UW_CreationDate { get; set; }
        public System.DateTime UW_ModifiedDate { get; set; }
        public bool UW_IsDelete { get; set; }
    
        public virtual Tbl_User Tbl_User { get; set; }
        public virtual Tbl_WorkshopPlan Tbl_WorkshopPlan { get; set; }
    }
}
