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

    public partial class FileMaster
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public int VaultId { get; set; }
        public int UserId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string Status { get; set; }
    
        public virtual UserMaster UserMaster { get; set; }
        public virtual VaultMaster VaultMaster { get; set; }
    }
}
