using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Security.Permissions;
using System.DirectoryServices.AccountManagement;

namespace PrincipalPermissionsImperative
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            string r = System.Environment.MachineName + @"\VS Developers";

            try
            {
                PrincipalPermission p = new PrincipalPermission(null, r, true);
                p.Demand();

                Console.WriteLine("Access Allowed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Access Denied: " + e.Message);
            }
            Console.ReadKey();

            List<GroupPrincipal> groupPrincipals = GetGroups(@"Amphion\Donal");
            foreach (GroupPrincipal gp in groupPrincipals)
            {
                Console.WriteLine(gp.DisplayName);
            }
        }

        public static List<GroupPrincipal> GetGroups(string userName)
        {
            List<GroupPrincipal> result = new List<GroupPrincipal>();

            // establish domain context
            PrincipalContext yourDomain = new PrincipalContext(ContextType.Domain);

            // find your user
            UserPrincipal user = UserPrincipal.FindByIdentity(yourDomain, userName);

            // if found - grab its groups
            if (user != null)
            {
                PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();

                // iterate over all groups
                foreach (Principal p in groups)
                {
                    // make sure to add only group principals
                    if (p is GroupPrincipal)
                    {
                        result.Add((GroupPrincipal)p);
                    }
                }
            }

            return result;
        }
    }
}
