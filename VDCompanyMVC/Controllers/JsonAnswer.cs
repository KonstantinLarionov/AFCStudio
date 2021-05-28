using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDCompany.Controllers
{
    public static class JsonAnswer
    {
        #region AdminController
        public static string A_NotAuthorized()
        {
            return "{\"status\":\"not_authorized\"}";
        }
        public static string A_AddLawyer_SuccessAded(int id_lawyer, int id_case, string json)
        {
            return "{\"status\":\"success\", \"data\":\"Lawyer id=" + id_lawyer + " success added to case id=" + id_case + "\", \"object\":" + json + "}";
        }
        public static string A_AddLawyer_AlreadyAdded(int id_lawyer, int id_case)
        {
            return "{\"status\":\"isset\", \"data\":\"Lawyer id=" + id_lawyer + " already added to case id=" + id_case + "\"}";
        }
        public static string A_AddLawyer_NotFound(int id_lawyer, int id_case)
        {
            return "{\"status\":\"error \"data\":\"Lawyer id=" + id_lawyer + " or case id=" + id_case + " not found\"}";
        }
        public static string A_DelLawyer_SuccessDeleted(int id_lawyer, int id_case)
        {
            return "{\"status\":\"success\", \"data\":\"Lawyer id=" + id_lawyer + " was success deleted from case id=" + id_case + "\"}";
        }
        public static string A_DelLawyer_NotFoundCase(int id_lawyer, int id_case)
        {
            return "{\"status\":\"error\", \"data\":\"Lawyer id=" + id_lawyer + " not found case id=" + id_case + "\"}";
        }
        public static string A_NewLayer(int new_id)
        {
            return "{\"status\":\"success\", \"data\":\"success added new lawyer\", \"id\":" + new_id + "}";
        }
        public static string A_EditLayer_SuccessEditLayer(int id)
        {
            return "{\"status\":\"success\", \"data\":\"success edit lawyer with id = " + id + "\"}";
        }
        public static string A_NotFoundLayer(int id)
        {
            return "{\"status\":\"error\", \"data\":\"not found lawyer with id = " + id + "\"}";
        }
        public static string A_RemoveLawyer(int id)
        {
            return "{\"status\":\"success\", \"data\":\"success remove lawyer with id = " + id + "\"}";
        }
        public static string A_Success(string json)
        {
            return "{\"status\":\"success\", \"data\":" + json + "}";
        }
        public static string A_EditUser_SuccessEditUser(int id)
        {
            return "{\"status\":\"success\", \"data\":\"success edit user with id = " + id + "\"}"; ;
        }
        public static string A_AddUserBill_SuccessAddedNewBill(int new_id)
        {
            return "{\"status\":\"success\", \"data\":\"success added new bill id = " + new_id + "\", \"id\":" + new_id + "}";
        }
        public static string A_UserBillChangeStatus_SuccessChangeBill(int bill_id)
        {
            return "{\"status\":\"success\", \"data\":\"success change status bill id = " + bill_id + "\"}";
        }
        #endregion

        #region LawyerController
        public static string L_Report(string reps)
        {
            return "{\"status\":\"success\", \"data\":\"Загружено файлов: " + reps + "\"}";
        }
        #endregion

        #region UserControllers
        public static string U_Unauthorized()
        {
            return "{\"info\":\"unauthorized\"}";
        }
        public static string U_Error()
        {
            return "{\"info\":\"error\"}";
        }
        public static string U_Success()
        {
            return "{\"info\":\"success\"}";
        }
        #endregion

        #region HomeControllers
        public static string H_Success()
        {
            return "{ \"status\": \"success\" }";
        }
        public static string H_Error()
        {
            return "{ \"status\": \"error\" }";
        }
        public static string H_()
        {
            return "{ \"status\": \"success\" }";
        }
        public static string H_Gallery_FilesUploaded(string files)
        {
            return "{\"status\":\"success\", \"data\":\"Загружено файлов: " + files + "\"}";
        }
        public static string H_Gallery_Error(string exp)
        {
            return "{\"status\":\"error\", \"data\": \"" + exp + "\"}";
        }
        #endregion

    }
}
