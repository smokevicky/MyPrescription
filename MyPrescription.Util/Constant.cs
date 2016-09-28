/***
page name: constants.cs
created date: 17.7.2016
purose: Keeps all the constants
**/

namespace MyPrescription.Util
{
    /// <summary>
    /// Keeps definitions of all the constants used through out the project
    /// </summary>
    public class Constant
    {
        public static string[] allowedFileTypes = new[] { "jpg", "jpeg", "png" };
        public static string errorPage = "~/Error/Error.aspx";
    }

    public struct StatusCode
    {
        public const int invalid = 0;
        public const int valid = 1;
        public const int error = 2;
    }

    public struct SignInStatusCode
    {
        public const int initial = -1;
        public const int invalid = 0;
        public const int valid = 1;
        public const int error = 2;
    }

    public struct ActivationStatusCode
    {
        public const int initial = -1;
        public const int invalidToken = 0;
        public const int valid = 1;
        public const int alreadyActivated = 2;
        public const int error = 3;
    }

    public enum FieldType
    {
        User,
        Hospital,
        Doctor,
        Vault,
        File
    }

    public struct Id
    {
        public const int user = 0;
        public const int hospital = 1;
        public const int doctor = 2;
        public const int vault = 3;
        public const int record = 4;
        public const int file = 5;
        public const int defaultId = -1;
    }

    public struct ErrorCode
    {
        public const string SignInDAL = "SignIn_LEVEL_DAL";
        public const string SignInPAGE = "SignIn_LEVEL_PAGE";
        public const string SignUpDAL = "SignUp_LEVEL_DAL";
        public const string SignUpPAGE = "SignUp_LEVEL_PAGE";
        public const string ResetDAL = "ResetPassword_LEVEL_DAL";
        public const string ResetPAGE = "ResetPassword_LEVEL_PAGE";
        public const string ForgotDAL = "FlagAsForgotPassword_LEVEL_DAL";
        public const string ForgotPAGE = "FlagAsForgotPassword_LEVEL_PAGE";
        public const string SignUp2PAGE = "SignUpStep2_LEVEL_PAGE";
        public const string ActivationDAL = "ActivateAccount_LEVEL_DAL";
        public const string ActivationPAGE = "ActivateAccount_LEVEL_PAGE";
        public const string CheckActivationDAL = "CheckActivationStatus_LEVEL_DAL";
        public const string ProfilePAGE = "Profile_LEVEL_PAGE";
        public const string DeleteVaultDAL = "DeleteVault_LEVEL_DAL";
        public const string AddNewVaultDAL = "AddNewVault_LEVEL_DAL";
        public const string AddNewDoctorDAL = "AddNewDoctor_LEVEL_DAL";
        public const string DeleteDoctorDAL = "DeleteDoctor_LEVEL_DAL";
        public const string UpdateDoctorDAL = "UpdateDoctor_LEVEL_DAL";
        public const string AddNewFileDAL = "AddNewFile_LEVEL_DAL";
        public const string AddNewHospitalDAL = "AddNewHospital_LEVEL_DAL";
        public const string DeleteHospitalDAL = "DeleteHospital_LEVEL_DAL";
        public const string UpdateHospitalDetailsDAL = "UpdateHospitalDetails_LEVEL_DAL";
        public const string CheckActivationStatusDAL = "CheckActivationStatus_LEVEL_DAL";
        public const string SaveFilePhysicallyBL = "SaveFilePhysically_LEVEL_BL";
        public const string AddNewHospitalEF = "addNewHospital_LEVEL_EF";
        public const string GetHospitalDetailsEF = "GetHospitalDetails_LEVEL_EF";
        public const string DeleteHospitalEF = "DeleteHospital_LEVEL_EF";
        public const string GetSingleHospitalDetailsEF = "GetSingleHospitalDetails_LEVEL_EF";
        public const string UpdateHospitalEF = "UpdateHospital_LEVEL_EF";

        public const string APIAddNewDoctor = "AddNewDoctor_LEVEL_API";
        public const string APIGetDoctorDetails = "GetDoctorDetails_LEVEL_API";
        public const string APIDeleteDoctor = "DeleteDoctor_LEVEL_API";
        public const string APIGetSingleDoctorDetails = "GetSingleDoctorDetails_LEVEL_API";
        public const string APIUpdateDoctorDetails = "UpdateDoctorDetails_LEVEL_API";
        public const string APIAddNewFile = "AddNewFile_LEVEL_API";
        public const string APIAddNewHospital = "AddNewHospital_LEVEL_API";
        public const string APIGetHospitalDetails = "GetHospitalDetails_LEVEL_API";
        public const string APIDeleteHospital = "DeleteHospital_LEVEL_API";
        public const string APIGetSingleHospitalDetails = "GetSingleHospitalDetails_LEVEL_API";
        public const string APIUpdateHospitalDetails = "UpdateHospitalDetails_LEVEL_API";
        public const string APIisAvailable = "isAvailable_LEVEL_API";
        public const string APICheckStatusFromToken = "CheckStatusFromToken_LEVEL_API";
        public const string APICheckEmailFromToken = "CheckEmailFromToken_LEVEL_API";
        public const string APIGetBadgeCount = "GetBadgeCount_LEVEL_API";
        public const string APIAddNewVault = "AddNewVault_LEVEL_API";
        public const string APIGetVaultDetails = "GetVaultDetails_LEVEL_API";
        public const string APIDeleteVault = "DeleteVault_LEVEL_API";
        public const string APIGetSingleVaultDetails = "GetSingleVaultDetails_LEVEL_API";
    }
}