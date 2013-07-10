// This sample demonstrates how to set code access permissions programmatically. It creates a 
// new parent and child code group pair, and allows the user to optionally delete the child group 
// and/or the parent code group. It also shows the result of a ResolvePolicy call, and displays 
// the permissions for the three security levels; Enterprise, Machine, and User. 
using System;
using System.Collections;
using System.Security;
using System.Security.Policy;
using System.Security.Permissions;
using System.Reflection;
using System.Globalization;

class SecurityManagerSample
{
    static void Main()
    {
        // Gets a value indicating whether code must have execution rights in order to execute.
        if(!SecurityManager.CheckExecutionRights)
            Console.WriteLine("Execution rights are not required to run the assemblies.");
        // Gets a value indicating whether code access security is enabled. 
        if(!SecurityManager.SecurityEnabled)
            Console.WriteLine("Security is not enabled.");
        // Determines whether the right to control policy has been granted to the caller. 
        if(SecurityManager.IsGranted(new SecurityPermission(SecurityPermissionFlag.ControlPolicy)))
        {
            // Define custom named permission sets for Company and Department. 
            // These will be used for the new code groups.
            //CreateCompanyPermission();
            //CreateDepartmentPermission();

            // Create a parent and child code group at the Machine policy level using the 
            // permission sets we created.
            //CreateCodeGroups();

            // Demonstrate the result of a call to ResolvePolicy(). 
            // This is not required for the main thrust of this sample, custom named permissions 
            // and code groups, but allows demonstration of the ResolvePolicy method.
            Console.WriteLine("Current Security Policy:");
            Console.WriteLine("------------------------");
            DisplaySecurityPolicy();

            Console.WriteLine("Resolve Policy demonstration.");
            // Get the evidence for the Local Intranet zone.
            Evidence intranetZoneEvidence = new Evidence(new object[] { new Zone(SecurityZone.Intranet) }, null);
            Console.WriteLine("Show the result of ResolvePolicy for LocalIntranet zone evidence.");
            CheckEvidence(intranetZoneEvidence);

            // Optionally remove the policy elements that were created.
            Console.WriteLine("Would you like to remove the Department code group?");
            Console.WriteLine("Please type 'yes' to delete the Department group, else press the Enter key.");
            string answer = Console.ReadLine();
            if(answer == "yes")
            {
                DeleteCustomChildCodeGroup("MyDepartment");
                SecurityManager.SavePolicy();
            }

            Console.WriteLine("Would you like to remove all new code groups and permission sets?");
            Console.WriteLine("Please type yes to delete all new groups, else press the Enter key.");
            answer = Console.ReadLine();
            if(answer == "yes")
            {
                DeleteCustomCodeGroups();
                DeleteCustomPermissions();
                SecurityManager.SavePolicy();
            }
        }
        else
        {
            Console.Out.WriteLine("ControlPolicy permission is denied.");
        }

        return;
    }
    private static void DisplaySecurityPolicy()
    {
        IEnumerator policyEnumerator = SecurityManager.PolicyHierarchy();
        while(policyEnumerator.MoveNext())
        {
            PolicyLevel currentLevel = (PolicyLevel)policyEnumerator.Current;

            // Display the policy at the current level.
            Console.WriteLine("Policy Level {0}:", currentLevel.Label);
            // To display the policy detail, uncomment the following line: 
            //Console.WriteLine(currentLevel.ToXml().ToString());
            IList namedPermissions = currentLevel.NamedPermissionSets;
            IEnumerator namedPermission = namedPermissions.GetEnumerator();
            while(namedPermission.MoveNext())
            {
                Console.WriteLine("\t" + ((NamedPermissionSet)namedPermission.Current).Name);
            }
        }
    }

    private static void DeleteCustomCodeGroups()
    {
        // Delete the custom code groups that were created.
        IEnumerator policyEnumerator = SecurityManager.PolicyHierarchy();
        while(policyEnumerator.MoveNext())
        {
            PolicyLevel machineLevel = (PolicyLevel)policyEnumerator.Current;
            IList childCodeGroups = machineLevel.RootCodeGroup.Children;
            IEnumerator childGroups = childCodeGroups.GetEnumerator();
            while(childGroups.MoveNext())
            {
                CodeGroup thisCodeGroup = (CodeGroup)childGroups.Current;
                if( thisCodeGroup.Name == "MyCompanyCodeGroup")
                {
                    machineLevel.RootCodeGroup.RemoveChild(thisCodeGroup);
                }
            }
        }
    }

    private static void DeleteCustomChildCodeGroup(string codeGroupName)
    {
        // Delete the custom child group. 
        // Delete the child group by creating a copy of the parent code group, deleting its children, 
        // then adding the copy of the parent code group back to the root code group.
        IEnumerator policyEnumerator = SecurityManager.PolicyHierarchy();
        while(policyEnumerator.MoveNext())
        {
            PolicyLevel machineLevel = (PolicyLevel)policyEnumerator.Current;
            // IList returns copies of the code groups, not the code groups themselves, 
            // so operations on the IList objects do not affect the actual code group.
            IList childCodeGroups = machineLevel.RootCodeGroup.Children;
            IEnumerator childGroups = childCodeGroups.GetEnumerator();
            while(childGroups.MoveNext())
            {
                CodeGroup thisCodeGroup = (CodeGroup)childGroups.Current;
                if(thisCodeGroup.Name == codeGroupName)
                {
                    // Create a new code group from this one, but without it's children. 
                    // Delete the original code group and add the new one just created.
                    CodeGroup newCodeGroup = thisCodeGroup;
                    IList childCodeGroup = newCodeGroup.Children;
                    IEnumerator childGroup = childCodeGroup.GetEnumerator();
                    while(childGroup.MoveNext())
                    {
                        // Remove all the children from the copy.
                        newCodeGroup.RemoveChild((CodeGroup)childGroup.Current);
                    }
                    // Should have a copy of the parent code group with children removed. 
                    // Delete the original parent code group and replace with its childless clone.
                    machineLevel.RootCodeGroup.RemoveChild(thisCodeGroup);
                    machineLevel.RootCodeGroup.AddChild(newCodeGroup);
                    SecurityManager.SavePolicy();
                }
            }
        }
    }

    // Create a custom named permission set based on the LocalIntranet permission set. 
    private static void CreateCompanyPermission()
    {
        IEnumerator policyEnumerator = SecurityManager.PolicyHierarchy();
        // Move through the policy levels to the Machine policy level. 
        while(policyEnumerator.MoveNext())
        {
            PolicyLevel currentLevel = (PolicyLevel)policyEnumerator.Current;
            if(currentLevel.Label == "Machine")
            {
                // Enumerate the permission sets in the Machine policy level.
                IList namedPermissions = currentLevel.NamedPermissionSets;
                IEnumerator namedPermission = namedPermissions.GetEnumerator();
                // Locate the LocalIntranet permission set. 
                while(namedPermission.MoveNext())
                {
                    if(((NamedPermissionSet)namedPermission.Current).Name == "LocalIntranet")
                    {
                        // The current permission set is a copy of the LocalIntranet permission set. 
                        // It can be modified to provide the permissions for the new permission set. 
                        // Rename the copy to the name chosen for the new permission set.
                        ((NamedPermissionSet)namedPermission.Current).Name = "MyCompany";
                        IEnumerator permissions = ((NamedPermissionSet)namedPermission.Current).GetEnumerator();
                        // Remove the current security permission from the permission set and replace it 
                        // with a new security permission that does not have the right to assert permissions. 
                        while(permissions.MoveNext())
                        {
                            if(permissions.Current.GetType().ToString() == "System.Security.Permissions.SecurityPermission")
                            {
                                // Remove the current security permission.
                                ((NamedPermissionSet)namedPermission.Current).RemovePermission(permissions.Current.GetType());
                                // Add a new security permission that only allows execution.
                                ((NamedPermissionSet)namedPermission.Current).AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
                                break;
                            }
                        }
                        try
                        {
                            // If you run this application twice, the following instruction throws 
                            // an exception because the named permission set is already present. 
                            // You can remove the custom named permission set using Caspole.exe or the 
                            // .NET Framework Configuration tool
                            currentLevel.AddNamedPermissionSet(((NamedPermissionSet)namedPermission.Current));
                            SecurityManager.SavePolicy();
                        }
                            // Catch the exception for a duplicate permission set. 
                        catch ( System.ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                            return;
                        }
                        Console.WriteLine(((NamedPermissionSet)namedPermission.Current).ToString());
                        break;
                    }
                }
            }
        }
    }
    // Create new code groups using the custom named permission sets previously created. 
    private static void CreateCodeGroups()
    {
        // Create instances of the named permission sets created earlier to establish the 
        // permissions for the new code groups.
        NamedPermissionSet companyCodeSet = new NamedPermissionSet("MyCompany",PermissionState.Unrestricted);
        NamedPermissionSet departmentCodeSet = new NamedPermissionSet("MyDepartment",PermissionState.Unrestricted);
        // Create new code groups using the named permission sets.
        PolicyStatement policyMyCompany = new PolicyStatement(companyCodeSet,PolicyStatementAttribute.LevelFinal);
        PolicyStatement policyMyDepartment = new PolicyStatement(departmentCodeSet,PolicyStatementAttribute.Exclusive);
        // Create new code groups using UnionCodeGroup.
        CodeGroup myCompanyZone = new UnionCodeGroup(new ZoneMembershipCondition(SecurityZone.Intranet), policyMyCompany);
        myCompanyZone.Name = "MyCompanyCodeGroup";

        byte[] b1 = { 0, 36, 0, 0, 4, 128, 0, 0, 148, 0, 0, 0, 6, 2, 0, 0, 0, 36, 0, 0, 82, 83, 65, 49, 0, 4, 0, 0, 1, 0, 1, 0, 237, 146, 145, 51, 34, 97, 123, 196, 90, 174, 41, 170, 173, 221, 41, 193, 175, 39, 7, 151, 178, 0, 230, 152, 218, 8, 206, 206, 170,84, 111, 145, 26, 208, 158, 240, 246, 219, 228, 34, 31, 163, 11, 130, 16, 199, 111, 224, 4, 112, 46, 84, 0, 104, 229, 38, 39, 63, 53, 189, 0, 157, 32, 38, 34, 109, 0, 171, 114, 244, 34, 59, 9, 232, 150, 192, 247, 175, 104, 143, 171, 42, 219, 66, 66, 194, 191, 218, 121, 59, 92, 42, 37, 158, 13, 108, 210, 189, 9, 203, 204, 32, 48, 91, 212, 101, 193, 19, 227, 107, 25, 133, 70, 2, 220, 83, 206, 71, 102, 245, 104, 252, 87, 109, 190, 56, 34, 180};
        StrongNamePublicKeyBlob blob = new StrongNamePublicKeyBlob(b1);

        CodeGroup myDepartmentZone = new UnionCodeGroup(new StrongNameMembershipCondition(blob, null, null), policyMyDepartment);
        myDepartmentZone.Name = "MyDepartmentCodeGroup";

        // Move through the policy levels looking for the Machine policy level. 
        // Create two new code groups at that level.
        IEnumerator policyEnumerator = SecurityManager.PolicyHierarchy();
        while(policyEnumerator.MoveNext())
        {
            // At the Machine level delete already existing copies of the custom code groups, 
            // then create the new code groups.
            PolicyLevel currentLevel = (PolicyLevel)policyEnumerator.Current;
            if (currentLevel.Label == "Machine")
            {

                // Remove old instances of the custom groups.
                DeleteCustomCodeGroups();
                // Add the new code groups. 
                //******************************************************* 
                // To add a child code group, add the child to the parent prior to adding 
                // the parent to the root.
                myCompanyZone.AddChild(myDepartmentZone);
                // Add the parent to the root code group.
                currentLevel.RootCodeGroup.AddChild(myCompanyZone);
                SecurityManager.SavePolicy();
            }
        }
        // Save the security policy.
        SecurityManager.SavePolicy();
        Console.WriteLine("Security policy modified.");
        Console.WriteLine("New code groups added at the Machine policy level.");
    }
    private static void CreateDepartmentPermission()
    {
        IEnumerator policyEnumerator = SecurityManager.PolicyHierarchy();
        // Move through the policy levels to the Machine policy level. 

        while(policyEnumerator.MoveNext())
        {
            PolicyLevel currentLevel = (PolicyLevel)policyEnumerator.Current;
            if(currentLevel.Label == "Machine")
            {
                // Enumerate the permission sets in the Machine level.
                IList namedPermissions = currentLevel.NamedPermissionSets;
                IEnumerator namedPermission = namedPermissions.GetEnumerator();
                // Locate the Everything permission set. 
                while(namedPermission.MoveNext())
                {
                    if(((NamedPermissionSet)namedPermission.Current).Name == "Everything")
                    {
                        // The current permission set is a copy of the Everything permission set. 
                        // It can be modified to provide the permissions for the new permission set. 
                        // Rename the copy to the name chosen for the new permission set.
                        ((NamedPermissionSet)namedPermission.Current).Name = "MyDepartment";
                        IEnumerator permissions = ((NamedPermissionSet)namedPermission.Current).GetEnumerator();
                        // Modify security permission by removing and replacing with a new permission. 
                        while(permissions.MoveNext())
                        {
                            if(permissions.Current.GetType().ToString() == "System.Security.Permissions.SecurityPermission")
                            {
                                ((NamedPermissionSet)namedPermission.Current).RemovePermission(permissions.Current.GetType());
                                // Add a new security permission with limited permissions.
                                SecurityPermission limitedPermission = new SecurityPermission(SecurityPermissionFlag.Execution |
                                    SecurityPermissionFlag.RemotingConfiguration |
                                    SecurityPermissionFlag.ControlThread);
                                ((NamedPermissionSet)namedPermission.Current).AddPermission(limitedPermission);

                                break;
                            }
                        }

                        try
                        {
                            // If you run this application twice, the following instruction throws 
                            // an exception because the named permission set is already present. 
                            // You can remove the custom named permission set using Caspole.exe or the 
                            // .NET Framework Configuration tool
                            currentLevel.AddNamedPermissionSet(((NamedPermissionSet)namedPermission.Current));
                            SecurityManager.SavePolicy();
                        }
                        catch (System.ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Console.WriteLine(((NamedPermissionSet)namedPermission.Current).ToString());
                        break;
                    }
                }
            }
        }
    }
    private static void DeleteCustomPermissions()
    {
        IEnumerator policyEnumerator = SecurityManager.PolicyHierarchy();
        // Move through the policy levels to the Machine policy level. 
        while(policyEnumerator.MoveNext())
        {
            PolicyLevel currentLevel = (PolicyLevel)policyEnumerator.Current;
            if(currentLevel.Label == "Machine")
            {
                try
                {
                    currentLevel.RemoveNamedPermissionSet("MyCompany");
                    currentLevel.RemoveNamedPermissionSet("MyDepartment");
                }
                catch(System.ArgumentException e)
                {
                    // An exception is thrown if the named permission set cannot be found.
                    Console.WriteLine(e.Message);
                }

            }
        }
    }

    // Demonstrate the use of ResolvePolicy. 
    private static void CheckEvidence(Evidence evidence)
    {
        // Display the code groups to which the evidence belongs.
        Console.WriteLine("ResolvePolicy for the given evidence.");
        Console.WriteLine("Current evidence belongs to the following code groups:");
        IEnumerator policyEnumerator = SecurityManager.PolicyHierarchy();
        while(policyEnumerator.MoveNext())
        {

            PolicyLevel currentLevel = (PolicyLevel)policyEnumerator.Current;
            CodeGroup cg1 = currentLevel.ResolveMatchingCodeGroups(evidence);
            Console.WriteLine(currentLevel.Label + " Level" );
            Console.WriteLine("\tCodeGroup = " + cg1.Name);
            Console.WriteLine("StoreLocation = " + currentLevel.StoreLocation);
            IEnumerator cgE1 = cg1.Children.GetEnumerator();
            while(cgE1.MoveNext())
            {
                Console.WriteLine("\t\tGroup = " + ((CodeGroup)cgE1.Current).Name);
            }
        }

        // Show how ResolvePolicy is used to determine the set of permissions that would be granted 
        // by the security system to code, based on the evidence and the permission sets requested. 
        // The permission sets require Execute permission; allow optional Read access permission 
        // to C:\temp; and deny the code permission to control security policy.
        Console.WriteLine("\nCreate permission sets requiring Execute permission, requesting optional " +
            "\nRead permission for 'C:\\temp', and dening permission to control policy.");
        PermissionSet requiredSet = new PermissionSet(PermissionState.None);
        requiredSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));

        PermissionSet optionalSet = new PermissionSet(PermissionState.None);
        optionalSet.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read, new string[] { @"c:\temp" }));

        PermissionSet deniedSet = new PermissionSet(PermissionState.None);
        deniedSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.ControlPolicy));

        // Show the granted permissions.
        Console.WriteLine("\nCurrent permissions granted:");

        PermissionSet permsDenied = null;
        foreach(IPermission perm in SecurityManager.ResolvePolicy(evidence, requiredSet, optionalSet, deniedSet, out permsDenied))
            Console.WriteLine(perm.ToXml().ToString());

        // Show the denied permissions.
        Console.WriteLine("Current permissions denied:");
        foreach(IPermission perm in permsDenied)
            Console.WriteLine(perm.ToXml().ToString());

        return;
    }
}