using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDTS.DirectTopUpServices.BusinessEntities;

namespace BalanceService.Client
{
    public interface IClient
    {
        SendOperatorTopUpPhoneAccountRequestStatus GetBalance(OperatorDistributorTimeout op, OperatorTopUpPhoneAccountRequest request);
        Update CheckOperators(string version);
    }
}