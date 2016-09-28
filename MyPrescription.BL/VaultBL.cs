using MyPrescription.DAL;
using MyPrescription.Models;

namespace MyPrescription.BL
{
    /// <summary>
    /// BL Level class for Vault
    /// </summary>
    public class VaultBL
    {
        /// <summary>
        /// Adds a vault to db
        /// </summary>
        /// <param name="vaultModelObject">Accepts object of type VaultModel</param>
        /// <returns>VaultId</returns>
        public static int AddNewVault(VaultModel vaultModelObject)
        {
            return VaultDAL.AddNewVault(vaultModelObject);
        }

        /// <summary>
        /// Sends request to VaultDAL to get list of all vaults
        /// </summary>
        /// <param name="vaultRequestModelObject">Accepts parameter of type VaultRequestModel</param>
        /// <returns>ResponseModel</returns>
        public static ResponseModel GetVaultDetails(VaultRequestModel vaultRequestModelObject)
        {
            return VaultDAL.GetVaultDetails(vaultRequestModelObject);
        }

        /// <summary>
        /// Sends request to VaultDAL to delete a vault
        /// </summary>
        /// <param name="vaultModelObject">Accepts parameter of type VaultModel</param>
        /// <returns>true/false</returns>
        public static bool DeleteVault(VaultModel vaultModelObject)
        {
            return VaultDAL.DeleteVault(vaultModelObject);
        }

        /// <summary>
        /// Calls VaultDAL to get single vault details
        /// </summary>
        /// <param name="vaultModelObject">Accepts parameters of type VaultModel</param>
        /// <returns>VaultModel Object</returns>
        public static VaultModel GetSingleVaultDetails(VaultModel vaultModelObject)
        {
            return VaultDAL.GetSingleVaultDetails(vaultModelObject);
        }
    }
}
