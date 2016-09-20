using MyPrescription.DAL;
using MyPrescription.EntityFramework;
using MyPrescription.Models;

namespace MyPrescription.BL
{
    public class HospitalBL
    {
        /// <summary>
        /// Adds a new hospital.
        /// </summary>
        /// <param name="hospitalModelObject">The hospital model object.</param>
        /// <returns></returns>
        public static bool AddNewHospital(HospitalModel hospitalModelObject)
        {
            //return HospitalDAL.AddNewHospital(hospitalModelObject);

            return HospitalEF.AddNewHospital(hospitalModelObject);
        }

        /// <summary>
        /// Gets the list of all hospitals
        /// </summary>
        /// <param name="hospitalRequestModelObject">The hospital request model object.</param>
        /// <returns></returns>
        public static HospitalResponseModel GetHospitalDetails(HospitalRequestModel hospitalRequestModelObject)
        {
            //return HospitalDAL.GetHospitalDetails(hospitalRequestModelObject);

            return HospitalEF.GetHospitalDetails(hospitalRequestModelObject);
        }

        /// <summary>
        /// Deletes the hospital.
        /// </summary>
        /// <param name="hospitalModelObject">The hospital model object.</param>
        /// <returns></returns>
        public static bool DeleteHospital(HospitalModel hospitalModelObject)
        {
            return HospitalDAL.DeleteHospital(hospitalModelObject);
        }

        /// <summary>
        /// Gets the single hospital details.
        /// </summary>
        /// <param name="hospitalModelObject">The hospital model object.</param>
        /// <returns></returns>
        public static HospitalModel GetSingleHospitalDetails(HospitalModel hospitalModelObject)
        {
            return HospitalDAL.GetSingleHospitalDetails(hospitalModelObject);
        }

        /// <summary>
        /// Updates the hospital details.
        /// </summary>
        /// <param name="hospitalModelObject">The hospital model object.</param>
        /// <returns></returns>
        public static bool UpdateHospitalDetails(HospitalModel hospitalModelObject)
        {
            return HospitalDAL.UpdateHospitalDetails(hospitalModelObject);
        }
    }
}
