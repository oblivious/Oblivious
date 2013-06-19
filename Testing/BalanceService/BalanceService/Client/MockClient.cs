using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using EDTS.DirectTopUpServices.BusinessEntities;

namespace BalanceService.Client
{
    public class MockClient : IClient
    {
        private SendOperatorTopUpPhoneAccountRequestStatusType responseCodes;               
      
        #region IClient Members

        public SendOperatorTopUpPhoneAccountRequestStatus GetBalance(EDTS.DirectTopUpServices.BusinessEntities.OperatorDistributorTimeout op, EDTS.DirectTopUpServices.BusinessEntities.OperatorTopUpPhoneAccountRequest request)
        {
            SendOperatorTopUpPhoneAccountRequestStatus responseStatus = new SendOperatorTopUpPhoneAccountRequestStatus();

            switch (op.OperatorName)
            {
                case "Operator0":
                    responseStatus.StatusID = Settings.INVALID_OPERATOR_ERROR_CODE;                    
                    break;
                case "Operator1":
                    responseStatus.StatusID = (int)SendOperatorTopUpPhoneAccountRequestStatusType.Success;
                    responseStatus.ReceiptNumber = "123.09";
                    break;
                case "Operator2":
                    responseStatus.StatusID = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorAccessDenied;
                    break;
                case "Operator3":
                    responseStatus.StatusID = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorAuthenticationError;
                    break;
                case "Operator4":
                    responseStatus.StatusID = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorSubscriberNotValid;
                    break;
                case "Operator5":
                    responseStatus.StatusID = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorSubscriberTemporarilyBlocked;
                    break;
                case "Operator6":
                    responseStatus.StatusID = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorRefillNotAllowed;
                    break;
                case "Operator7":
                    responseStatus.StatusID = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorInvalidRequestParameters;
                    break;
                case "Operator8":
                    responseStatus.StatusID = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorSystemInternalError;
                    break;
                case "Operator9":
                    responseStatus.StatusID = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorThrewException;
                    break;
                case "Operator10":
                    responseStatus.StatusID = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorTimedOut;
                    break;
                case "Operator11":
                    throw new NotImplementedException();
                    break;
                case "Operator12":
                    responseStatus.StatusID = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorGatewayInvalid;
                    break;
                case "Operator13":
                    throw new Exception("Exception calling the assembly.");
                    break;
            }

            return responseStatus;
        }

        public Update CheckOperators(string version)
        {
            Update upd = new Update();          

            switch (version)
            {
                case "V07051430":
                    upd.NewVersion = DateTime.Now.ToString(Settings.VERSION_PATTERN);
                    upd.OperatorList = new string[] { "SmartBelize", "CubacelCuba", "MobitelSriLanka" };
                    upd.UpToDate = false;
                    break;
                case "V06071434":
                    upd.UpToDate = true;
                    break;
                case "V05061020":
                    upd.NewVersion = DateTime.Now.ToString(Settings.VERSION_PATTERN);
                    upd.OperatorList = new string[] { "SmartBelize", "CubacelCuba", "OrangeDominicanRepublic" };
                    upd.UpToDate = false;
                    break;
            }

            return upd;
        }

        #endregion
    }
}