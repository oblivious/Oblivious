//Copyright (C) Microsoft Corporation.  All rights reserved.

// AttributesTutorial.cs
// This example shows the use of class and method attributes.

using System;
using System.IO;
using System.Reflection;
using System.Collections;

// The IsTested class is a user-defined custom attribute class.
// It can be applied to any declaration including
//  - types (struct, class, enum, delegate)
//  - members (methods, fields, events, properties, indexers)
// It is used with no arguments.
public class IsTestedAttribute : Attribute
{
    public override string ToString()
    {
        return "Is Tested";
    }
}

// The AuthorAttribute class is a user-defined attribute class.
// It can be applied to classes and struct declarations only.
// It takes one unnamed string argument (the author's name).
// It has one optional named argument Version, which is of type int.
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class AuthorAttribute : Attribute
{
    // This constructor specifies the unnamed arguments to the attribute class.
    public AuthorAttribute(string name)
    {
        this.name = name;
        this.version = 0;
    }

    // This property is readonly (it has no set accessor)
    // so it cannot be used as a named argument to this attribute.
    public string Name 
    {
        get 
        {
            return name;
        }
    }

    // This property is read-write (it has a set accessor)
    // so it can be used as a named argument when using this
    // class as an attribute class.
    public int Version
    {
        get 
        {
            return version;
        }
        set 
        {
            version = value;
        }
    }

    public override string ToString()
    {
        string value = "Author : " + Name;
        if (version != 0)
        {
            value += " Version : " + Version.ToString();
        }
        return value;
    }

    private string name;
    private int version;
}

// Here you attach the AuthorAttribute user-defined custom attribute to 
// the Account class. The unnamed string argument is passed to the 
// AuthorAttribute class's constructor when creating the attributes.
[Author("Joe Programmer")]
class Account
{
    // Attach the IsTestedAttribute custom attribute to this method.
    [IsTested]
    public void AddOrder(Order orderToAdd)
    {
        orders.Add(orderToAdd);
    }

    private ArrayList orders = new ArrayList();
}

// Attach the AuthorAttribute and IsTestedAttribute custom attributes 
// to this class.
// Note the use of the 'Version' named argument to the AuthorAttribute.
[Author("Jane Programmer", Version = 2), IsTested()]
class Order
{
    // add stuff here ...
}

class MainClass
{
   private static bool IsMemberTested(MemberInfo member)
   {
        foreach (object attribute in member.GetCustomAttributes(true))
        {
            if (attribute is IsTestedAttribute)
            {
               return true;
            }
        }
      return false;
   }

    private static void DumpAttributes(MemberInfo member)
    {
        Console.WriteLine("Attributes for : " + member.Name);
        foreach (object attribute in member.GetCustomAttributes(true))
        {
            Console.WriteLine(attribute);
        }
    }

    public static void Main()
    {
        // display attributes for Account class
        DumpAttributes(typeof(Account));

        // display list of tested members
        foreach (MethodInfo method in (typeof(Account)).GetMethods())
        {
            if (IsMemberTested(method))
            {
               Console.WriteLine("Member {0} is tested!", method.Name);
            }
            else
            {
               Console.WriteLine("Member {0} is NOT tested!", method.Name);
            }
        }
        Console.WriteLine();

        // display attributes for Order class
        DumpAttributes(typeof(Order));

        // display attributes for methods on the Order class
        foreach (MethodInfo method in (typeof(Order)).GetMethods())
        {
           if (IsMemberTested(method))
           {
               Console.WriteLine("Member {0} is tested!", method.Name);
           }
           else
           {
               Console.WriteLine("Member {0} is NOT tested!", method.Name);
           }
        }
        Console.WriteLine();

        Console.WriteLine("\nAdditional:\n");
        Assembly a = typeof(object).Module.Assembly;

        Assembly b = Assembly.LoadFrom("GuineaPig.exe");

        Type[] types2 = b.GetTypes();
        foreach (Type t in types2)
            Console.WriteLine(t.FullName);

        Type t2 = typeof(System.String);
        Console.WriteLine("Listing all the public constructors of the {0} type", t2);
        ConstructorInfo[] ci = t2.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
        PrintMembers(ci);

        Console.WriteLine("\nReflection.MemberInfo");
        Type myType = Type.GetType("System.IO.File");
        MemberInfo[] myMemberInfoArray = myType.GetMembers();
        Console.WriteLine("\nThere are {0} members in {1}.", myMemberInfoArray.Length, myType.FullName);
        if (myType.IsPublic)
            Console.WriteLine("{0} is public.", myType.FullName);
        /*
        foreach (MemberInfo m in myMemberInfoArray)
        {
            Console.WriteLine("    {0}:\n\t{1}\n\t{2}\n", m.Name, m.ReflectedType, m.ToString());
        }
         * */

        Console.WriteLine("\nReflection.MethodInfo");
        Type fiType = Type.GetType("System.Reflection.FieldInfo");
        MethodInfo mimi = fiType.GetMethod("GetValue");
        PropertyInfo pipi = fiType.GetProperty("Name");
        Console.WriteLine(fiType.FullName + "." + mimi.Name);
        MemberTypes myMemberTypes = mimi.MemberType;
        MemberTypes otherMemberType = pipi.MemberType;
        if (MemberTypes.Constructor == myMemberTypes)
            Console.WriteLine("MemberType is of type All");
        else if (MemberTypes.Custom == myMemberTypes)
            Console.WriteLine("MemberType is of type Custom");
        else if (MemberTypes.Event == myMemberTypes)
            Console.WriteLine("MemberType is of type Event");
        else if (MemberTypes.Field == myMemberTypes)
            Console.WriteLine("MemberType is of type Field");
        else if (MemberTypes.Method == myMemberTypes)
            Console.WriteLine("MemberType is of type Method");
        else if (MemberTypes.Property == myMemberTypes)
            Console.WriteLine("MemberType is of type Property");
        else if (MemberTypes.TypeInfo == myMemberTypes)
            Console.WriteLine("MemberType is of type TypeInfo");
        Console.WriteLine("The other member type is: {0}", otherMemberType);

        //Listing Members
        Type bs = typeof(System.IO.BufferedStream);
        Console.WriteLine("\nListing all the members (public and non public) of the {0} type", bs);

        FieldInfo[] fieldInfo = bs.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        Console.WriteLine("// Static Fields");
        PrintMembers(fieldInfo);

        PropertyInfo[] propertyInfo = bs.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        Console.WriteLine("// Static Properties");
        PrintMembers(propertyInfo);

        EventInfo[] eventInfo = bs.GetEvents(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        Console.WriteLine("// Static Events");
        PrintMembers(eventInfo);

        MethodInfo[] methodInfo = bs.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        Console.WriteLine("// Static Methods");
        PrintMembers(methodInfo);

        ConstructorInfo[] constructorInfo = bs.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        Console.WriteLine("// Constructors");
        PrintMembers(methodInfo);

        fieldInfo = bs.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        Console.WriteLine("// Instance Fields");
        PrintMembers(fieldInfo);

        propertyInfo = bs.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        Console.WriteLine("// Instance Properties");
        PrintMembers(propertyInfo);

        eventInfo = bs.GetEvents(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        Console.WriteLine("// Instance Events");
        PrintMembers(eventInfo);

        methodInfo = bs.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        Console.WriteLine("// Instance Methods");
        PrintMembers(methodInfo);
    }

    public static void PrintMembers(MemberInfo[] ms)
    {
        foreach (MemberInfo m in ms)
            Console.WriteLine("\t{0}", m);
        Console.WriteLine();
    }
}


