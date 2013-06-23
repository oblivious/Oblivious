using System;
using System.Collections;
using System.Security;
using System.Security.Policy;
using System.Security.Permissions;
using System.Reflection;
using System.Globalization;

namespace PolicyLevel_Class
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("********************************************************************************");
            Console.WriteLine(" Create an AppDomain policy level.");
            Console.WriteLine("Use the AppDomain to demonstrate PolicyLevel methods and properties.");
            Console.WriteLine("********************************************************************************");
            CreateAPolicyLevel();
        }

        private static void CreateAPolicyLevel()
        {
            try
            {
                // Create an AppDomain policy level.
                PolicyLevel pLevel = PolicyLevel.CreateAppDomainLevel();

                // The root code group of the policy level combines all permissions of its children.
                UnionCodeGroup rootCodeGroup;
                PermissionSet ps = new PermissionSet(PermissionState.None);
                ps.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));

                rootCodeGroup = new UnionCodeGroup(
                    new AllMembershipCondition(),
                    new PolicyStatement(ps, PolicyStatementAttribute.Nothing));

                // This code group grants FullTrust to assemblies with the strong name key from this assembly.
                UnionCodeGroup myCodeGroup = new UnionCodeGroup(
                    new StrongNameMembershipCondition(
                        new StrongNamePublicKeyBlob(GetKey()),
                        null,
                        null),
                    new PolicyStatement(new PermissionSet(PermissionState.Unrestricted),
                        PolicyStatementAttribute.Nothing)
                    );
                myCodeGroup.Name = "My CodeGroup";

                // Add the code groups to the policy level.
                rootCodeGroup.AddChild(myCodeGroup);
                pLevel.RootCodeGroup = rootCodeGroup;
                Console.WriteLine("Permissions granted to all code running in this AppDomain level: ");
                Console.WriteLine(rootCodeGroup.ToXml());
                Console.WriteLine("Child code groups in RootCodeGroup: ");
                IList codeGroups = pLevel.RootCodeGroup.Children;
                IEnumerator codeGroup = codeGroups.GetEnumerator();
                while (codeGroup.MoveNext())
                {
                    Console.WriteLine("\t" + ((CodeGroup)codeGroup.Current).Name);
                }
                Console.WriteLine("Demonstrate adding and removing named permission sets.");
                Console.WriteLine("Original named permissions sets:");
                ListPermissionSets(pLevel);
                NamedPermissionSet myInternet = pLevel.GetNamedPermissionSet("Internet");
                    

            }
            catch
            {
            }
        }

        private static void ListPermissionSets(PolicyLevel pLevel)
        {
            IList namedPermissions = pLevel.NamedPermissionSets;
            IEnumerator namedPermission = namedPermissions.GetEnumerator();
            while (namedPermission.MoveNext())
            {
                Console.WriteLine("\t" + ((NamedPermissionSet)namedPermission.Current).Name);
            }
        }

        private static byte[] GetKey()
        {
            return Assembly.GetCallingAssembly().GetName().GetPublicKey();
        }

        private static void CheckEvidence(Evidence evidence)
        {
            // Display the code groups to which the evidence belongs.
            Console.WriteLine("ResolvePolicy for the given evidence.");
            Console.WriteLine("\tCurrent evidence belongs to the following code groups: ");
            IEnumerator policyEnumerator = SecurityManager.PolicyHierarchy();
            // Resolve the evidence at all the policy levels.
            while (policyEnumerator.MoveNext())
            {
                PolicyLevel currentLevel = (PolicyLevel)policyEnumerator.Current;
                CodeGroup cg1 = currentLevel.ResolveMatchingCodeGroups(evidence);
                Console.WriteLine("\n\t" + currentLevel.Label + " Level");
                Console.WriteLine("\t\tCodeGroup = " + cg1.Name);
                IEnumerator cgE1 = cg1.Children.GetEnumerator();
                while (cgE1.MoveNext())
                {
                    Console.WriteLine("\t\t\tGroup = " + ((CodeGroup)cgE1.Current).Name);
                }
                Console.WriteLine("\tStoreLocation = " + currentLevel.StoreLocation);
            }
            return;
        }
        
    }
}
