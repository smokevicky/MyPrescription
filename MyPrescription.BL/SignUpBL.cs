using MyPrescription.DAL;
using MyPrescription.Models;

namespace MyPrescription.BL
{
    public class SignUpBL
    {
        public UserModel SignUp(UserModel userModelObject)
        {
            SignUpDAL signUpDALObject = new SignUpDAL();
            UserModel userModelReturnObject = signUpDALObject.SignUp(userModelObject);
            return userModelReturnObject;
        }
    }
}
