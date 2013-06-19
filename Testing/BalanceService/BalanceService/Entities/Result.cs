using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BalanceService
{
    public class Result
    {
        public string OperatorName { get; set; }
        public string Balance { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorCodeDescription { get; set; }
    }
}