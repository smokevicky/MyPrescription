using System.Configuration;
using System.Data.SqlClient;

namespace MyPrescription.Util
{
    public class SqlUtility
    {
        string conStr = ConfigurationManager.ConnectionStrings["MyPrescriptionConnectionString"].ConnectionString;
        public SqlConnection con;

        public SqlUtility()
        {
            con = new SqlConnection(conStr);
        }
    }
}
