//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyPrescription.EntityFramework.App_Data
{
    using System.Collections.Generic;

    public partial class VaultMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VaultMaster()
        {
            this.FileMasters = new HashSet<FileMaster>();
        }
    
        public int VaultId { get; set; }
        public string VName { get; set; }
        public int UserId { get; set; }
        public int HospitalId { get; set; }
        public int DoctorId { get; set; }
        public System.DateTime Date { get; set; }
        public int RecordId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string Status { get; set; }
    
        public virtual DoctorMaster DoctorMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileMaster> FileMasters { get; set; }
        public virtual HospitalMaster HospitalMaster { get; set; }
        public virtual RecordTypeMaster RecordTypeMaster { get; set; }
        public virtual UserMaster UserMaster { get; set; }
    }
}
