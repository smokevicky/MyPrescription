using MyPrescription.DAL;
using MyPrescription.Models;

namespace MyPrescription.BL
{
    public class ErrorLogBL
    {
        public static void LogError(ErrorLogModel errorLogModelObject)
        {
            ErrorLogDAL.LogError(errorLogModelObject);
        }
    }
}
