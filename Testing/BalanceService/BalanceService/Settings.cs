using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BalanceService
{
    public class Settings
    {
        public const string GATEWAY_START_WITH = "EDTS.DirectTopUpServices.OperatorGateWay";
        public const string BALANCE_METHOD = "SendOperatorBalanceQueryRequest";
        public const string VERSION_PATTERN = "MMddHHmm";
        public const string TIMEOUT = "The operation has timed out";
        public const string ISL = "ISL";
        public const string IMPL = "IMPL";
        public const string TESTING = "Testing";

        public const int INVALID_OPERATOR_ERROR_CODE = 0;        
        public const int EXCEPTION_ERROR_CODE = 19;        
        public const int NOT_IMPLEMENTED_ERROR_CODE = 119;
        public const string NOT_IMPLEMENTED_ERROR_CODE_DESCRIPTION = "Balance Not Implemented Error";
        public const string EXCEPTION_ERROR_CODE_DESCRIPTION = "Balance Service Internal Error";
        public const string INVALID_OPERATOR_ERROR_CODE_DESCRIPTION = "Invalid Operator";
    }
}