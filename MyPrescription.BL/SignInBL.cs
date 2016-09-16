using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPrescription.DAL;
using MyPrescription.Models;

namespace MyPrescription.BL
{
    public class SignInBL
    {
        public UserModel SignIn(UserModel userModelObject)
        {
            SignInDAL signInDALObject = new SignInDAL();
            UserModel userModelReturnObject = signInDALObject.SignIn(userModelObject);
            return userModelReturnObject;
        } 
    }
}
