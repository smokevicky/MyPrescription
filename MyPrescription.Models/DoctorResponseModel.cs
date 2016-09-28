using System.Collections.Generic;

namespace MyPrescription.Models
{
    public class DoctorResponseModel
    {
        public int statusCode { get; set; }
        public List<DoctorModel> doctorModelList = new List<DoctorModel>();
        public int rowCount { get; set; }
        public string error { get; set; }

        public DoctorResponseModel()
        {
            statusCode = -1;
            doctorModelList = null;
            rowCount = -1;
            error = null;
        }
    }
}
