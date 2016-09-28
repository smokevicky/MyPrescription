namespace MyPrescription.Models
{
    public class ErrorLogModel
    {
        public int userId { get; set; }
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
    }
}
