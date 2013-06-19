using BalanceService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using EDTS.DirectTopUpServices.BusinessEntities;

namespace BalanceService.Testing
{
    
    
    /// <summary>
    ///This is a test class for BalanceServiceTest and is intended
    ///to contain all BalanceServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BalanceServiceTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        private const string INVALID_OPERATOR = "Operator0";
        private const string SUCCESS_OPERATOR = "Operator1";
        private const string ACCESS_DENIED_OPERATOR = "Operator2";
        private const string AUTHENTICATION_ERROR_OPERATOR = "Operator3";
        private const string SUBSCRIBER_NOT_VALID_OPERATOR = "Operator4";
        private const string SUBSCRIBER_TEMPORARILY_BLOCKED_OPERATOR = "Operator5";
        private const string REFILL_NOT_ALLOWED_OPERATOR = "Operator6";
        private const string INVALID_REQUEST_PARAMETERS_OPERATOR = "Operator7";
        private const string SYSTEM_INTERNAL_ERROR_OPERATOR = "Operator8";
        private const string THREW_EXCEPTION_OPERATOR = "Operator9";
        private const string TIME_OUT_OPERATOR = "Operator10";
        private const string NOT_IMPLEMENTED_EXCEPTION_OPERATOR = "Operator11";
        private const string ANY_OTHER_ERROR_OPERATOR = "Operator12";
        private const string ANY_OTHER_EXCEPTION_OPERATOR = "Operator13";

        private const string CHECK_OPERATORS_NEWER_CASE = "V07051430";
        private const string CHECK_OPERATOR_CURRENT_CASE = "V06071434";
        private const string CHECK_OPERATOR_OLDER_CASE = "V05061020";
        #endregion


        /// <summary>
        ///A test for GetBalance
        ///</summary>
        // Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.

        #region GetBalance

        [TestMethod] 
        public void TestOperator_InvalidOperator()
        {
            Result expectedResult = new Result()
            {
                ErrorCode = Settings.INVALID_OPERATOR_ERROR_CODE
            };

            this.GetBalanceTest(INVALID_OPERATOR, expectedResult);
        }

        [TestMethod] 
        public void TestOperator_Success()
        {
            Result expectedResult = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.Success
            };

            this.GetBalanceTest(SUCCESS_OPERATOR,expectedResult);
        }

        [TestMethod] 
        public void TestOperator_AccessDenied()
        {
            Result expectedResult = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorAccessDenied
            };

            this.GetBalanceTest(ACCESS_DENIED_OPERATOR, expectedResult);
        }

        [TestMethod] 
        public void TestOperator_AuthenticationError()
        {
            Result expectedResult = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorAuthenticationError
            };

            this.GetBalanceTest(AUTHENTICATION_ERROR_OPERATOR, expectedResult);
        }

        [TestMethod] 
        public void TestOperator_SubscriberNotValid()
        {
            Result expectedResult = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorSubscriberNotValid
            };

            this.GetBalanceTest(SUBSCRIBER_NOT_VALID_OPERATOR, expectedResult);
        }

        [TestMethod] 
        public void TestOperator_SubscriberTemporarilyBlocked()
        {
            Result expectedResult = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorSubscriberTemporarilyBlocked
            };

            this.GetBalanceTest(SUBSCRIBER_TEMPORARILY_BLOCKED_OPERATOR, expectedResult);
        }

        [TestMethod] 
        public void TestOperator_RefillNotAllowed()
        {
            Result expectedResult = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorRefillNotAllowed
            };

            this.GetBalanceTest(REFILL_NOT_ALLOWED_OPERATOR, expectedResult);
        }

        [TestMethod] 
        public void TestOperator_InvalidRequestParameters()
        {
            Result expectedResult = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorInvalidRequestParameters
            };

            this.GetBalanceTest(INVALID_REQUEST_PARAMETERS_OPERATOR, expectedResult);
        }

        [TestMethod] 
        public void TestOperator_SystemInternalError()
        {
            Result expectedResult = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorSystemInternalError
            };

            this.GetBalanceTest(SYSTEM_INTERNAL_ERROR_OPERATOR, expectedResult);
        }

        [TestMethod] 
        public void TestOperator_ThrewException()
        {
            Result expectedResult = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorThrewException
            };

            this.GetBalanceTest(THREW_EXCEPTION_OPERATOR, expectedResult);
        }

        [TestMethod] 
        public void TestOperator_TimeOut()
        {
            Result expectedResult = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorTimedOut
            };

            this.GetBalanceTest(TIME_OUT_OPERATOR, expectedResult);
        }

        [TestMethod] 
        public void TestOperator_NotImplementedException()
        {
            Result expectedResult = new Result()
            {
                ErrorCode = Settings.NOT_IMPLEMENTED_ERROR_CODE
            };

            this.GetBalanceTest(NOT_IMPLEMENTED_EXCEPTION_OPERATOR, expectedResult);
        }

        [TestMethod] 
        public void TestOperator_AnyOtherError()
        {
            Result expectedResult = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorGatewayInvalid
            };

            this.GetBalanceTest(ANY_OTHER_ERROR_OPERATOR, expectedResult);
        }

        [TestMethod]        
        public void TestOperator_AnyOtherException()
        {
            Result expectedResult = new Result()
            {
                ErrorCode = Settings.EXCEPTION_ERROR_CODE
            };

            this.GetBalanceTest(ANY_OTHER_EXCEPTION_OPERATOR, expectedResult);
        }

        #endregion

        #region GetBalances (string [] operatorNames)

        [TestMethod]
        public void TestMultipleOperators_InvalidOperator()
        {
            string[] operatorNames = new string[]
            {
                INVALID_OPERATOR, INVALID_OPERATOR, INVALID_OPERATOR
            };

            Result[] expectedResults = new Result[3];

            expectedResults [0] = new Result()
            {
                ErrorCode = Settings.INVALID_OPERATOR_ERROR_CODE
            };

            expectedResults[1] = new Result()
            {
                ErrorCode = Settings.INVALID_OPERATOR_ERROR_CODE
            };

            expectedResults[2] = new Result()
            {
                ErrorCode = Settings.INVALID_OPERATOR_ERROR_CODE
            };

            this.GetBalancesTest(operatorNames, expectedResults);
        }

        [TestMethod]
        public void TestMultipleOperators_Success()
        {
            string[] operatorNames = new string[]
            {
                INVALID_OPERATOR, SUCCESS_OPERATOR, SUCCESS_OPERATOR
            };

            Result[] expectedResults = new Result[3];

            expectedResults[0] = new Result()
            {
                ErrorCode = Settings.INVALID_OPERATOR_ERROR_CODE
            };

            expectedResults[1] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.Success
            };

            expectedResults[2] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.Success
            };

            this.GetBalancesTest(operatorNames, expectedResults);                                  
        }

        [TestMethod]
        public void TestMultipleOperators_AccessDenied()
        {
            string[] operatorNames = new string[]
            {
                INVALID_OPERATOR, ACCESS_DENIED_OPERATOR, ACCESS_DENIED_OPERATOR
            };

            Result[] expectedResults = new Result[3];

            expectedResults[0] = new Result()
            {
                ErrorCode = Settings.INVALID_OPERATOR_ERROR_CODE
            };

            expectedResults[1] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorAccessDenied
            };

            expectedResults[2] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorAccessDenied
            };

            this.GetBalancesTest(operatorNames, expectedResults);                            
        }

        [TestMethod]
        public void TestMultipleOperator_AuthenticationError()
        {
            string[] operatorNames = new string[]
            {
                INVALID_OPERATOR, AUTHENTICATION_ERROR_OPERATOR, AUTHENTICATION_ERROR_OPERATOR
            };

            Result[] expectedResults = new Result[3];

            expectedResults[0] = new Result()
            {
                ErrorCode = Settings.INVALID_OPERATOR_ERROR_CODE
            };

            expectedResults[1] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorAuthenticationError
            };

            expectedResults[2] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorAuthenticationError
            };

            this.GetBalancesTest(operatorNames, expectedResults);                  
        }

        [TestMethod]
        public void TestMultipleOperator_SubscriberNotValid()
        {
            string[] operatorNames = new string[]
            {
                INVALID_OPERATOR, SUBSCRIBER_NOT_VALID_OPERATOR, SUBSCRIBER_NOT_VALID_OPERATOR
            };

            Result[] expectedResults = new Result[3];

            expectedResults[0] = new Result()
            {
                ErrorCode = Settings.INVALID_OPERATOR_ERROR_CODE
            };

            expectedResults[1] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorSubscriberNotValid
            };

            expectedResults[2] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorSubscriberNotValid
            };

            this.GetBalancesTest(operatorNames, expectedResults);              
        }

        [TestMethod]
        public void TestMultipleOperators_SubscriberTemporarilyBlocked()
        {
            string[] operatorNames = new string[]
            {
                INVALID_OPERATOR, SUBSCRIBER_TEMPORARILY_BLOCKED_OPERATOR, SUBSCRIBER_TEMPORARILY_BLOCKED_OPERATOR
            };

            Result[] expectedResults = new Result[3];

            expectedResults[0] = new Result()
            {
                ErrorCode = Settings.INVALID_OPERATOR_ERROR_CODE
            };

            expectedResults[1] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorSubscriberTemporarilyBlocked
            };

            expectedResults[2] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorSubscriberTemporarilyBlocked
            };

            this.GetBalancesTest(operatorNames, expectedResults);              
        }

        [TestMethod]
        public void TestMultipleOperators_RefillNotAllowed()
        {
            string[] operatorNames = new string[]
            {
                INVALID_OPERATOR, REFILL_NOT_ALLOWED_OPERATOR, REFILL_NOT_ALLOWED_OPERATOR
            };

            Result[] expectedResults = new Result[3];

            expectedResults[0] = new Result()
            {
                ErrorCode = Settings.INVALID_OPERATOR_ERROR_CODE
            };

            expectedResults[1] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorRefillNotAllowed
            };

            expectedResults[2] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorRefillNotAllowed
            };

            this.GetBalancesTest(operatorNames, expectedResults);                          
        }

        [TestMethod]
        public void TestMultipleOperator_InvalidRequestParameters()
        {
            string[] operatorNames = new string[]
            {
                INVALID_OPERATOR, INVALID_REQUEST_PARAMETERS_OPERATOR, INVALID_REQUEST_PARAMETERS_OPERATOR
            };

            Result[] expectedResults = new Result[3];

            expectedResults[0] = new Result()
            {
                ErrorCode = Settings.INVALID_OPERATOR_ERROR_CODE
            };

            expectedResults[1] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorInvalidRequestParameters
            };

            expectedResults[2] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorInvalidRequestParameters
            };

            this.GetBalancesTest(operatorNames, expectedResults);             
        }

        [TestMethod]
        public void TestMultipleOperators_SystemInternalError()
        {
            string[] operatorNames = new string[]
            {
                INVALID_OPERATOR, SYSTEM_INTERNAL_ERROR_OPERATOR, SYSTEM_INTERNAL_ERROR_OPERATOR
            };

            Result[] expectedResults = new Result[3];

            expectedResults[0] = new Result()
            {
                ErrorCode = Settings.INVALID_OPERATOR_ERROR_CODE
            };

            expectedResults[1] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorSystemInternalError
            };

            expectedResults[2] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorSystemInternalError
            };

            this.GetBalancesTest(operatorNames, expectedResults);                              
        }

        [TestMethod]
        public void TestMultipleOperator_ThrewException()
        {
            string[] operatorNames = new string[]
            {
                INVALID_OPERATOR, THREW_EXCEPTION_OPERATOR, THREW_EXCEPTION_OPERATOR
            };

            Result[] expectedResults = new Result[3];

            expectedResults[0] = new Result()
            {
                ErrorCode = Settings.INVALID_OPERATOR_ERROR_CODE
            };

            expectedResults[1] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorThrewException
            };

            expectedResults[2] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorThrewException
            };

            this.GetBalancesTest(operatorNames, expectedResults);                        
        }

        [TestMethod]
        public void TestMultipleOperator_TimeOut()
        {
            string[] operatorNames = new string[]
            {
                INVALID_OPERATOR, TIME_OUT_OPERATOR, TIME_OUT_OPERATOR
            };

            Result[] expectedResults = new Result[3];

            expectedResults[0] = new Result()
            {
                ErrorCode = Settings.INVALID_OPERATOR_ERROR_CODE
            };

            expectedResults[1] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorTimedOut
            };

            expectedResults[2] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorTimedOut
            };

            this.GetBalancesTest(operatorNames, expectedResults);
        }

        [TestMethod]
        public void TestMultipleOperator_NotImplementedException()
        {
            string[] operatorNames = new string[]
            {
                INVALID_OPERATOR, NOT_IMPLEMENTED_EXCEPTION_OPERATOR, NOT_IMPLEMENTED_EXCEPTION_OPERATOR
            };

            Result[] expectedResults = new Result[3];

            expectedResults[0] = new Result()
            {
                ErrorCode = Settings.INVALID_OPERATOR_ERROR_CODE
            };

            expectedResults[1] = new Result()
            {
                ErrorCode = Settings.NOT_IMPLEMENTED_ERROR_CODE
            };

            expectedResults[2] = new Result()
            {
                ErrorCode = Settings.NOT_IMPLEMENTED_ERROR_CODE
            };

            this.GetBalancesTest(operatorNames, expectedResults);                        
        }

        [TestMethod]
        public void TestMultipleOperator_AnyOtherError()
        {
            string[] operatorNames = new string[]
            {
                INVALID_OPERATOR, ANY_OTHER_ERROR_OPERATOR, ANY_OTHER_ERROR_OPERATOR
            };

            Result[] expectedResults = new Result[3];

            expectedResults[0] = new Result()
            {
                ErrorCode = Settings.INVALID_OPERATOR_ERROR_CODE
            };

            expectedResults[1] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorGatewayInvalid
            };

            expectedResults[2] = new Result()
            {
                ErrorCode = (int)SendOperatorTopUpPhoneAccountRequestStatusType.OperatorGatewayInvalid
            };

            this.GetBalancesTest(operatorNames, expectedResults);                        
        }

        [TestMethod]
        public void TestMultipleOperator_AnyOtherException()
        {
            string[] operatorNames = new string[]
            {
                INVALID_OPERATOR, ANY_OTHER_EXCEPTION_OPERATOR, ANY_OTHER_EXCEPTION_OPERATOR
            };

            Result[] expectedResults = new Result[3];

            expectedResults[0] = new Result()
            {
                ErrorCode = Settings.INVALID_OPERATOR_ERROR_CODE
            };

            expectedResults[1] = new Result()
            {
                ErrorCode = Settings.EXCEPTION_ERROR_CODE
            };

            expectedResults[2] = new Result()
            {
                ErrorCode = Settings.EXCEPTION_ERROR_CODE
            };

            this.GetBalancesTest(operatorNames, expectedResults);              
        }

        [TestMethod]
        public void TestVersion_NewerVersion()
        {
            string version = CHECK_OPERATORS_NEWER_CASE;

            Update expectedUpdate = new Update()
            {
                NewVersion = DateTime.Now.ToString(Settings.VERSION_PATTERN),
                OperatorList = new string[] { "SmartBelize", "CubacelCuba", "MobitelSriLanka" },
                UpToDate = false,
            };

            this.CheckOperatorTest(version, expectedUpdate);
        }

        [TestMethod]
        public void TestVersion_CurrentVersion()
        {
            string version = CHECK_OPERATOR_CURRENT_CASE;

            Update expectedUpdate = new Update()
            {                
                UpToDate = true,
            };

            this.CheckOperatorTest(version, expectedUpdate);
        }

        [TestMethod]
        public void TestVersion_OlderVersion()
        {
            string version = CHECK_OPERATOR_OLDER_CASE;

            Update expectedUpdate = new Update()
            {
                NewVersion = DateTime.Now.ToString(Settings.VERSION_PATTERN),
                OperatorList = new string[] { "SmartBelize", "CubacelCuba", "OrangeDominicanRepublic" },
                UpToDate = false,
            };

            this.CheckOperatorTest(version, expectedUpdate);
        }
        
        #endregion

        #region Helper
        public void GetBalanceTest(string mockOperatorName, Result expectedResult)
        {
            BalanceManager target = new BalanceManager();            
           
            Result actualResult = target.GetBalance(mockOperatorName);
            Assert.AreEqual(expectedResult.ErrorCode, actualResult.ErrorCode);
        }

        public void CheckOperatorTest(string version, Update expectedUpdate)
        {
            BalanceManager target = new BalanceManager();
            Update actualUpdate = target.CheckOperators(version);

            Assert.IsTrue(expectedUpdate.NewVersion == actualUpdate.NewVersion
                & CheckOperatorList(expectedUpdate.OperatorList,actualUpdate.OperatorList)
                & expectedUpdate.UpToDate == actualUpdate.UpToDate);
        }

        public void GetBalancesTest(string [] mockOperatorNames, Result [] expectedResults)
        {
            BalanceManager target = new BalanceManager();

            Result [] actualResults = target.GetBalances(mockOperatorNames);
            Assert.IsTrue(expectedResults[0].ErrorCode == actualResults[0].ErrorCode                
                & expectedResults[1].ErrorCode == actualResults[1].ErrorCode                
                & expectedResults[2].ErrorCode == actualResults[2].ErrorCode);                
        }

        private bool CheckOperatorList(string[] operatorListA, string[] operatorListB)
        {
            bool result = false;
            
            if (operatorListA == operatorListB)
            {
                result =  true;
            }
            else if (operatorListA != null & operatorListB != null)
            {
                if (operatorListA.Length == operatorListB.Length)
                {                    
                    for (int i = 0; i < operatorListA.Length; i++)
                    {
                        if (operatorListA[i] != operatorListB[i])
                        {
                            result = false;
                            return result;
                        }
                    }
                    result = true;
                }
                else
                {
                    result =  false;
                }

            }
            else
            {
                result =  false;
            }
            return result;
        }
        #endregion
    }
}
