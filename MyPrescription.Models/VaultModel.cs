using System.Collections.Generic;

namespace MyPrescription.Models
{
    public class VaultModel
    {
        public int statusCode { get; set; }
        public int row { get; set; }
        public int userId { get; set; }
        public int vaultId { get; set; }
        public string vaultName { get; set; }
        public int hospitalId { get; set; }
        public string hospitalName { get; set; }
        public int doctorId { get; set; }
        public string doctorName { get; set; }
        public string date { get; set; }
        public int recordId { get; set; }        
        public string recordType { get; set; }
        public string createdDate { get; set; }
        public string status { get; set; }
        public int noOfFiles { get; set; }
        public List<FileModel> filesList = new List<FileModel>();
        public string error;
    }

    public class VaultRequestModel
    {
        public int userId { get; set; }
        public int pageStart { get; set; }
        public int pageSize { get; set; }
    }
}
