using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
