using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPrescription.Models;
using MyPrescription.DAL;

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
