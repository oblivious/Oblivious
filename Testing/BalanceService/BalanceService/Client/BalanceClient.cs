using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDTS.DirectTopUpServices.BusinessEntities;
using System.Reflection;
using Utility.LoggingService;

namespace BalanceService.Client
{
    public class BalanceClient : IClient
    {                
        #region IClient Members

        public SendOperatorTopUpPhoneAccountRequestStatus GetBalance(OperatorDistributorTimeout op, OperatorTopUpPhoneAccountRequest request)
        {
            SendOperatorTopUpPhoneAccountRequestStatus status = new SendOperatorTopUpPhoneAccountRequestStatus();
            object response = null;
            
            try
            {
                //Get Operator List
                Type gatewayType = Singleton.Instance.GatewayTable[op.OperatorName];
                
                //Prepare data to send
                object activeObject = Activator.CreateInstance(gatewayType);
                object[] parameters = new object[] { op, request };
                //SendOperatorTopUpPhoneAccountRequestStatus edtsResponse = new SendOperatorTopUpPhoneAccountRequestStatus();

                //Send Balance
                response = gatewayType.InvokeMember(Settings.BALANCE_METHOD, BindingFlags.Default | BindingFlags.InvokeMethod, null, activeObject, parameters);

                //Cast to SendOperatorTopUpPhoneAccountRequestStatus
                status = (SendOperatorTopUpPhoneAccountRequestStatus)response;                                
            }
            catch (Exception ex)
            {
                CLogger.WriteLog(ELogLevel.ERROR, "Balance Client Exception - "+ ex.Message);

                throw (ex);
            }

            return status;
        }

        public Update CheckOperators(string version)
        {
            Update upd = new Update();
            
            if (Singleton.Instance.LatestVersion == version)
            {
                upd.UpToDate = true;
            }
            else
            {               
                upd.UpToDate = false;
                upd.OperatorList = Singleton.Instance.GatewayTable.Keys.ToArray<string>();
                upd.NewVersion = Singleton.Instance.LatestVersion;
            }

            return upd;
        }

        #endregion
    }
}