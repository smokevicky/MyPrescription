namespace MyPrescription.Models
{
    public class HospitalRequestModel
    {
        public int pageStart { get; set;}
        public int pageSize { get; set; }
        public int userId { get; set; }
        public string sortBy { get; set; }

        public HospitalRequestModel()
        {
            pageStart = -1;
            pageSize = -1;
            userId = -1;
            sortBy = null;
        }
    }
}
