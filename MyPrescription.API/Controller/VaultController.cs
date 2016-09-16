/**********************************
*Name: Jyoti Prakash Jena
*FileName: VaultController.cs
*CreatedOn: 29/8/2016
*Purpose: Handles all the vault api calls
***********************************/

using MyPrescription.BL;
using MyPrescription.Error;
using MyPrescription.Models;
using MyPrescription.Util;
using System;
using System.IO;
using System.Web;
using System.Web.Http;

namespace MyPrescription.API.Controller
{
    /// <summary>
    /// Handles all the Vault API calls
    /// </summary>
    public class VaultController : ApiController
    {
        /// <summary>
        /// Adds new vault to db
        /// </summary>
        /// <param name="vaultModelObject">Accepts object of type VaultModel</param>
        /// <returns>true/false</returns>
        [HttpPost]
        [Authorize]
        public bool AddNewVault()
        {
            int userId;
            Int32.TryParse(User.Identity.Name, out userId);

            try {
                int hospitalId, doctorId, recordId, vaultId;

                //creating vaultModelObject and pushing all details into the object
                VaultModel vaultModelObject = new VaultModel();
                vaultModelObject.userId = userId;
                vaultModelObject.vaultName = HttpContext.Current.Request.Form["vaultName"];

                Int32.TryParse(HttpContext.Current.Request.Form["hospitalId"], out hospitalId);
                vaultModelObject.hospitalId = hospitalId;

                Int32.TryParse(HttpContext.Current.Request.Form["doctorId"], out doctorId);
                vaultModelObject.doctorId = doctorId;

                vaultModelObject.date = HttpContext.Current.Request.Form["date"];

                Int32.TryParse(HttpContext.Current.Request.Form["recordId"], out recordId);
                vaultModelObject.recordId = recordId;

                //inserting into db and getting the vaultId
                vaultId = VaultBL.AddNewVault(vaultModelObject);

                //saving files physically
                return FileBL.SaveFilePhysically(HttpContext.Current.Request.Files, userId, vaultId);
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIAddNewVault, ex.ToString(), userId);
            }
            return false;
        }

        /// <summary>
        /// Gets list of all vaults for an user
        /// </summary>
        /// /// <param name="VaultRequestModel">Accepts object of type VaultRequestModel</param>
        /// <returns>VaultModel</returns>
        [HttpPost]
        [Authorize]
        public ResponseModel GetVaultDetails(VaultRequestModel vaultRequestModelObject)
        {
            ResponseModel responseModelObject = new ResponseModel();

            int userId;
            Int32.TryParse(User.Identity.Name, out userId);

            try {
                vaultRequestModelObject.userId = userId;

                responseModelObject = VaultBL.GetVaultDetails(vaultRequestModelObject);
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIGetVaultDetails, ex.ToString(), userId);
            }

            return responseModelObject;
        }

        /// <summary>
        /// Handles API request to delete a vault
        /// </summary>
        /// <param name="vaultModelObject">Accepts parameter of type VaultModel</param>
        /// <returns>true/false</returns>
        [HttpPost]
        [Authorize]
        public bool DeleteVault(VaultModel vaultModelObject)
        {
            int userId;
            Int32.TryParse(User.Identity.Name, out userId);

            try {
                vaultModelObject.userId = userId;

                return VaultBL.DeleteVault(vaultModelObject);
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIDeleteVault, ex.ToString(), userId);
            }
            return false;
        }

        /// <summary>
        /// Handles api request to get single vault details
        /// </summary>
        /// <param name="vaultModelObject">Accepts parameters of type vaultModek</param>
        /// <returns>VaultModel Object</returns>
        [HttpPost]
        [Authorize]
        public VaultModel GetSingleVaultDetails(VaultModel vaultModelObject)
        {
            int userId;
            Int32.TryParse(User.Identity.Name, out userId);

            try {
                vaultModelObject.userId = userId;

                return VaultBL.GetSingleVaultDetails(vaultModelObject);
            }

            catch (Exception ex)
            {
                ErrorLog.LogError(ErrorCode.APIGetSingleVaultDetails, ex.ToString(), userId);
            }
            return new VaultModel();
        }

    }
}
