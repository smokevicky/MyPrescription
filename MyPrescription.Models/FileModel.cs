namespace MyPrescription.Models
{
    public class FileModel
    {
        public int statusCode { get; set; }
        public int fileId { get; set; }
        public string fileName { get; set; }
        public int vaultId { get; set; }
        public int userId { get; set; }
        public string createdOn { get; set; }
        public string status { get; set; }
        public string error { get; set; }
    }
}
