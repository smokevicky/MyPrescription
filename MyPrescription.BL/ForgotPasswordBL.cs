using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPrescription.Models;
using MyPrescription.DAL;

namespace MyPrescription.BL
{
    public class ForgotPasswordBL
    {
        public UserModel FlagAsForgotPassword(string email)
        {
            ForgotPasswordDAL forgotPasswordDALObject = new ForgotPasswordDAL();
            UserModel userModelReturnObject = forgotPasswordDALObject.FlagAsForgotPassword(email);
            return userModelReturnObject;
        }
    }
}
