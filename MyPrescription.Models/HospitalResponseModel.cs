using System.Collections.Generic;

namespace MyPrescription.Models
{
    public class HospitalResponseModel
    {
        public int statusCode { get; set; }
        public List<HospitalModel> hospitalModelList = new List<HospitalModel>();
        public int rowCount { get; set; }
        public string error { get; set; }
        public int pageStart { get; set; }
        public int pageSize { get; set; }
        public string sortBy { get; set; }
        public int page { get; set; }
    }
}
