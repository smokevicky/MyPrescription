namespace MyPrescription.Models
{
    public class CountModel
    {
        public int statusCode { get; set; }
        public int hospitalCount { get; set; }
        public int doctorCount { get; set; }
        public int vaultCount { get; set; }
        public string error { get; set; }
    }
}
