using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BalanceService
{
    public class Update
    {
        public bool UpToDate { get; set; }
        public string NewVersion { get; set; }
        public string[] OperatorList { get; set; }
    }
}