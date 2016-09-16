using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPrescription.DAL;
using MyPrescription.Models;

namespace MyPrescription.BL
{
    public class VerifyBL
    {
        VerifyDAL verifyDALObject = new VerifyDAL();

        public int CheckActivationStatus(string token)
        {    
            int returnVal = verifyDALObject.CheckActivationStatus(token);
            return returnVal;
        }

        public UserModel ActivateAccount(string token)
        {
            UserModel userModelReturnObject = new UserModel();
            userModelReturnObject = verifyDALObject.ActivateAccount(token);
            return userModelReturnObject;
        }
    }
}
