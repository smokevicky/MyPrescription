namespace MyPrescription.Models
{
    public class DoctorModel
    {
        public int statusCode { get; set; }
        public int row { get; set; }
        public int doctorId { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phoneNo { get; set; }
        public string phoneNo2 { get; set; }
        public string email { get; set; }
        public int hospitalId { get; set; }
        public int userId { get; set; }
        public string createdOn { get; set; }
        public string updatedOn { get; set; }
        public string status { get; set; }
        public int isPrimary { get; set; }
        public string error { get; set; }

        public DoctorModel()
        {
            statusCode = -1;
            row = -1;
            doctorId = -1;            
            name = null;
            address = null;
            phoneNo = null;
            phoneNo2 = null;
            email = null;
            hospitalId = -1;
            userId = -1;
            createdOn = null;
            updatedOn = null;
            status = "passive";
            isPrimary = -1;
            error = null;
        }
    }
}
