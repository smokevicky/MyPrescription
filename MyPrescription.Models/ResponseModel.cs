namespace MyPrescription.Models
{
    public class ResponseModel
    {
        public int statusCode;
        public object list;
        public int rowCount;
        public string error { get; set; }

        public ResponseModel()
        {
            statusCode = -1;
            rowCount = -1;            
        }
    }
}
