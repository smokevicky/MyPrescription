using MyPrescription.DAL;
using MyPrescription.Models;

namespace MyPrescription.BL
{
    public class EnterNewPasswordBL
    {
        public UserModel ResetPassword(UserModel userModelObject)
        {
            EnterNewPasswordDAL enterNewPasswordDALObject = new EnterNewPasswordDAL();
            UserModel userModelReturnObject = enterNewPasswordDALObject.ResetPassword(userModelObject);           
            return userModelReturnObject;
        }
    }
}
