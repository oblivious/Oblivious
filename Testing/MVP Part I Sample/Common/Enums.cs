using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public enum EnumEmployeeType
    {
        Sales,
        Manager,
        Technical,
        MIS,
        Admin,
        HumanResources
    }

    public static class Enums
    {
        public static EnumEmployeeType GetEmployeeTypeByName(string name)
        {
            return (EnumEmployeeType) Enum.Parse(typeof (EnumEmployeeType),
                                                 name,
                                                 true);
        }
    }
}
