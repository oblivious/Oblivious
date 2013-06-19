using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using BalanceService.Client;
using EDTS.DirectTopUpServices.BusinessEntities;
using Utility.LoggingService;

namespace BalanceService
{
    public class Util
    {
        public Result[] resultsArray;
        public ManualResetEvent[] doneEvents;
        private IClient client;
        private SendOperatorTopUpPhoneAccountRequestStatusType responseCodes;
        private ClientFactory clientFactory = new ClientFactory();

        public Util() { }

        public Util(int operatorNumber)
        {
            this.resultsArray = new Result[operatorNumber];
            this.doneEvents = new ManualResetEvent[operatorNumber];

            //initialize doneEvents array
            for (int i = 0; i < doneEvents.Length; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
            }
        }

        public Result InvokeGetBalance(string operatorName)
        {
            client = clientFactory.GetClient();

            SendOperatorTopUpPhoneAccountRequestStatus responseStatus = new SendOperatorTopUpPhoneAccountRequestStatus();
            Result balance = new Result()
                {
                    OperatorName = operatorName
                };

            #region Create Request Items

            Operator o = new Operator()
            {
                OperatorName = operatorName
            };

            OperatorDistributorTimeout op = new OperatorDistributorTimeout(o)
            {
                DistributorTimeout = 120
            };

            OperatorTopUpPhoneAccountRequest request = new OperatorTopUpPhoneAccountRequest()
            {
                TransactionID2 = GetNewTransationId()
            };

            #endregion

            try
            {
                responseStatus = client.GetBalance(op, request);
                balance.ErrorCode = responseStatus.StatusID;
                balance.ErrorCodeDescription = Enum.GetName(responseCodes.GetType(), responseStatus.StatusID);

                if (balance.ErrorCode == (int)SendOperatorTopUpPhoneAccountRequestStatusType.Success)
                {
                    balance.Balance = responseStatus.ReceiptNumber;
                }

                CLogger.WriteLog(ELogLevel.INFO, "Balance Request Succeed for - " + operatorName + " Balance: " + responseStatus.ReceiptNumber);
            }
            catch (Exception e)
            {
                CLogger.WriteLog(ELogLevel.ERROR, e.Message);

                if (e.GetType() == typeof(NotImplementedException))
                {
                    balance.ErrorCode = Settings.NOT_IMPLEMENTED_ERROR_CODE;
                    balance.ErrorCodeDescription = Settings.NOT_IMPLEMENTED_ERROR_CODE_DESCRIPTION;
                }
                else if (e.InnerException != null)
                {
                    if (e.InnerException.Message == Settings.TIMEOUT)
                    {
                        balance.ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorTimedOut;
                        balance.ErrorCodeDescription = Enum.GetName(responseCodes.GetType(), responseStatus.StatusID);
                    }
                }
                else
                {
                    balance.ErrorCode = Settings.EXCEPTION_ERROR_CODE;
                    balance.ErrorCodeDescription = Settings.EXCEPTION_ERROR_CODE_DESCRIPTION;
                }
            }

            return balance;
        }

        public void InvokeGetBalanceThread(object balanceThreadParameter)
        {
            BalanceThreadParameter b = (BalanceThreadParameter)balanceThreadParameter;

            this.resultsArray[b.ThreadNumber] = InvokeGetBalance(b.OperatorName);

            this.doneEvents[b.ThreadNumber].Set();
        }

        private int GetNewTransationId()
        {
            return 123456789 + new Random().Next(1000000);
        }

    }
}