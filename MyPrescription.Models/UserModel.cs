namespace MyPrescription.Models
{
    public class UserModel
    {
        public int statusCode { get; set; }
        public int userId { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public bool isActive { get; set; }
        public string status { get; set; }
        public bool isDeleted { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public int hospitalPrimaryMark { get; set; }
        public int doctorPrimaryMark { get; set; }
        public string createdOn { get; set; }
        public string updatedOn { get; set; }
        public string error { get; set; }

        public UserModel()
        {
            statusCode = -1;
            userId = -1;
            email = null;
            password = null;
            token = null;
            isActive = false;
            status = "passive";
            isDeleted = false;
            firstName = null;
            lastName = null;
            address = null;
            phone = null;
            hospitalPrimaryMark = -1;
            doctorPrimaryMark = -1;
            createdOn = null;
            updatedOn = null;
            error = null;
        }

    }

    public class TesUserModel
    {
        public string email { get; set; }
    }
}
