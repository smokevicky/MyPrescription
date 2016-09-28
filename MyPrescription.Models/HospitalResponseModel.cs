using System.Collections.Generic;

namespace MyPrescription.Models
{
    public class HospitalResponseModel
    {
        public int statusCode { get; set; }
        public List<HospitalModel> hospitalModelList = new List<HospitalModel>();
        public int rowCount { get; set; }
        public string error { get; set; }

        public HospitalResponseModel()
        {
            statusCode = -1;
            hospitalModelList = null;
            rowCount = -1;
            error = null;
        }
    }
}
