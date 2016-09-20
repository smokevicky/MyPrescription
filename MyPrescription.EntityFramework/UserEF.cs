/********************************************************
** FileName:  UserDAL.cs
** Author:    Jyoti Prakash Jena  
** Date:      19.9.2016
** Purpose:   Does all the database operations for User
********************************************************/

using MyPrescription.EntityFramework.App_Data;
using MyPrescription.Models;

namespace MyPrescription.EntityFramework
{
    /// <summary>
    /// Does all the database operations for User
    /// </summary>
    public class UserEF
    {
        /// <summary>
        /// Signs the user in.
        /// </summary>
        /// <param name="userModelObject">The user model object.</param>
        /// <returns></returns>
        public UserModel SignIn(UserModel userModelObject)
        {
            var user = new UserMaster
            {
                UserId = 000000007,
                EMail = "test@test.com",
                Password = "test-test"
            };

            using (var entities = new MyPrescriptionEntities())
            {
                entities.UserMasters.Add(user);
                entities.SaveChanges();
            }

            return new UserModel();
        }
    }
}
