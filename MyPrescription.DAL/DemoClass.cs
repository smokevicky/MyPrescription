using MyPrescription.Models;
using System.Data.Entity;

namespace MyPrescription.DAL
{
    public class DemoClass : DbContext
    {
        public DbSet<HospitalModel> Hospitals { get; set; }
        public DbSet<DoctorModel> DoctorModels { get; set; }
        public DbSet<VaultModel> VaultModels { get; set; }
    }
}
