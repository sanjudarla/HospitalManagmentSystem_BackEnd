//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hospital_Management.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Report
    {
        public int ReportId { get; set; }
        public int PaymentId { get; set; }
        public string PatientName { get; set; }
        public string PatientAddress { get; set; }
        public string ContactNo { get; set; }
        public string Disease { get; set; }
        public string PatientCondition { get; set; }
        public System.DateTime AdmissionDate { get; set; }
        public string DoctorName { get; set; }
        public System.DateTime DischargeDate { get; set; }
        public int Patient_Id { get; set; }
        public int Patient_Billid { get; set; }
    
        public virtual Patient Patient { get; set; }
    }
}
