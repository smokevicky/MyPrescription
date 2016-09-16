using MyPrescription.DAL;
using MyPrescription.Models;

namespace MyPrescription.BL
{
    public class BadgeBL
    {
        public static CountModel GetBadgeCount(int userId)
        {
            return BadgeDAL.GetBadgeCount(userId);
        }
    }
}
