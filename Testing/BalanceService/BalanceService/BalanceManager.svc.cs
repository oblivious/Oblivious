using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BalanceService.Client;
using Utility.LoggingService;
using EDTS.DirectTopUpServices.BusinessEntities;
using System.Threading;

namespace BalanceService
{   
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class BalanceManager : IBalanceService
    {
        private ClientFactory clientFactory = new ClientFactory();        
        private SendOperatorTopUpPhoneAccountRequestStatusType responseCodes;

        #region IBalanceService Members
        
        public Result GetBalance(string operatorName)
        {
            Util util = new Util();

            Result balance = util.InvokeGetBalance(operatorName);

            return balance;                        
        }

        public Result [] GetBalances(string [] operatorNames)
        {
            Util util = new Util(operatorNames.Length);
            
            //Start all threads
            for(int i = 0; i < operatorNames.Length; i++)
            {
                 BalanceThreadParameter b = new BalanceThreadParameter()
                {
                    OperatorName = operatorNames[i],                    
                    ThreadNumber = i
                };
                
                ThreadPool.QueueUserWorkItem(util.InvokeGetBalanceThread,b);                               
            }
            
            //Wait all threads to finish
     
        foreach (WaitHandle handle in util.doneEvents)
        {
            handle.WaitOne();
        }
            //WaitHandle.WaitAll(util.doneEvents);

            return util.resultsArray;
        }

        public Update CheckOperators(string version)
        {
            IClient client = clientFactory.GetClient();   
            
            Update upd = client.CheckOperators(version);

            return upd;
        }

        #endregion        
       
    }
}
