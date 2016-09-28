using MyPrescription.DAL;
using MyPrescription.Models;

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
