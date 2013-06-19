using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BalanceService.Client
{
    public class ClientFactory
    {
        public IClient GetClient()
        {
            //return new MockClient();
            return new BalanceClient();
        }
    }
}