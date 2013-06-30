using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeProjectReflection
{
    class GetParameterInfoDemo
    {
        internal static void Run()
        {
            var t = typeof (Car);
            GetParametersInfo(t);
        }

        private static void GetParametersInfo(Type t)
        {
            Console.WriteLine("***** GetParametersInfo *****");
            var mi = t.GetMethods();
            foreach (var m in mi)
            {
                var retVal = m.ReturnType.FullName;
                var paramInfo = new StringBuilder();
                paramInfo.Append("(");

                var parameters = m.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    paramInfo.Append(string.Format("{0} {1}", parameters[i].ParameterType, parameters[i].Name));
                    if (i + 1 != parameters.Length)
                        paramInfo.Append(", ");
                }
                paramInfo.Append(")");

                Console.WriteLine("->{0} {1} {2}", retVal, m.Name, paramInfo);
            }
            Console.WriteLine();
        }
    }
}
