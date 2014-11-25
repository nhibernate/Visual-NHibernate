

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
namespace Slyce.IntelliMerge.UnitTesting.Resources.CSharp
{

    class PerformanceTest
    {

        //Copyright (C) Microsoft Corporation.  All rights reserved.

        class TheClass
        {
            public int x;
        }

        struct TheStruct
        {
            public int x;
        }

        class TestClass
        {
            public static void structtaker(TheStruct s)
            {
                s.x = 5;
            }
            public static void classtaker(TheClass c)
            {
                c.x = 5;
            }
            public static void Main()
            {
                TheStruct a = new TheStruct();
                TheClass b = new TheClass();
                a.x = 1;
                b.x = 1;
                structtaker(a);
                classtaker(b);
                Console.WriteLine("a.x = {0}", a.x);
                Console.WriteLine("b.x = {0}", b.x);
            }



        }

        struct SimpleStruct
        {
            private int xval;
            public int X
            {
                get
                {
                    return xval;
                }
                set
                {
                    if (value < 100)
                        xval = value;
                }
            }
            public void DisplayX()
            {
                Console.WriteLine("The stored value is: {0}", xval);
            }
        }

        class TestClass2
        {
            public static void Main()
            {
                SimpleStruct ss = new SimpleStruct();
                ss.X = 5;
                ss.DisplayX();
            }
        }

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
            }
        }





        // The partial keyword allows additional methods, fields, and
        // properties of this class to be defined in other .cs files.
        // This file contains private methods defined by CharValues.
        partial class CharValues
        {
            private static bool IsAlphabetic(char ch)
            {
                if (ch >= 'a' && ch <= 'z')
                    return true;
                if (ch >= 'A' && ch <= 'Z')
                    return true;
                return false;
            }

            private static bool IsNumeric(char ch)
            {
                if (ch >= '0' && ch <= '9')
                    return true;
                return false;
            }
        }

        public class TraceClient
        {
            public static void Main(string[] args)
            {

                if (args.Length == 0)
                {
                    Console.WriteLine("No arguments have been passed");
                }
                else
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        Console.WriteLine("Arg[{0}] is [{1}]", i, args[i]);
                    }
                }

            }
        }

        // Declare the Tokens class:
        public class Tokens : IEnumerable
        {
            private string[] elements;

            Tokens(string source, char[] delimiters)
            {
                // Parse the string into tokens:
                elements = source.Split(delimiters);
            }

            // IEnumerable Interface Implementation:
            //   Declaration of the GetEnumerator() method 
            //   required by IEnumerable
            public IEnumerator GetEnumerator()
            {
                return new TokenEnumerator(this);
            }

            // Inner class implements IEnumerator interface:
            private class TokenEnumerator : IEnumerator
            {
                private int position = -1;
                private Tokens t;

                public TokenEnumerator(Tokens t)
                {
                    this.t = t;
                }

                // Declare the MoveNext method required by IEnumerator:
                public bool MoveNext()
                {
                    if (position < t.elements.Length - 1)
                    {
                        position++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                // Declare the Reset method required by IEnumerator:
                public void Reset()
                {
                    position = -1;
                }

                // Declare the Current property required by IEnumerator:
                public object Current
                {
                    get
                    {
                        return t.elements[position];
                    }
                }
            }

            // Test Tokens, TokenEnumerator

            static void Main()
            {
                // Testing Tokens by breaking the string into tokens:
                Tokens f = new Tokens("This is a well-done program.",
                   new char[] { ' ', '-' });
                foreach (string item in f)
                {
                    Console.WriteLine(item);
                }
            }
        }
            class Yield
            {
                public static class NumberList
                {
                    // Create an array of integers.
                    public static int[] ints = { 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377 };

                    // Define a property that returns only the even numbers.
                    public static System.Collections.Generic.IEnumerable<int> GetEven()
                    {
                        // Use yield to return the even numbers in the list.
                        foreach (int i in ints)
                            if (i % 2 == 0)
                                yield return i;
                    }

                    // Define a property that returns only the even numbers.
                    public static IEnumerable<int> GetOdd()
                    {
                        // Use yield to return only the odd numbers.
                        foreach (int i in ints)
                            if (i % 2 == 1)
                                yield return i;
                    }
                }

                static void Main(string[] args)
                {

                    // Display the even numbers.
                    Console.WriteLine("Even numbers");
                    foreach (int i in NumberList.GetEven())
                        Console.WriteLine(i);

                    // Display the odd numbers.
                    Console.WriteLine("Odd numbers");
                    foreach (int i in NumberList.GetOdd())
                        Console.WriteLine(i);
                }
            }

            // The partial keyword allows additional methods, fields, and
            // properties of this class to be defined in other .cs files.
            // This file contains the public methods defined by CharValues.
            partial class CharValues
            {
                public static int CountAlphabeticChars(string str)
                {
                    int count = 0;
                    foreach (char ch in str)
                    {
                        // IsAlphabetic is defined in CharTypesPrivate.cs
                            count++;
                    }
                    return count;
                }
                public static int CountNumericChars(string str)
                {
                    int count = 0;
                    foreach (char ch in str)
                    {
                        // IsNumeric is defined in CharTypesPrivate.cs
                            count++;
                    }
                    return count;
                }

            }

           
            public enum BaseConstructType
            {
                ConstructorDeclaration,
                DelegateDeclaration,
                DestructorDeclaration,
                FieldDeclaration,
                EventDeclaration,
                MethodDeclaration,
                OperatorDeclaration,
                PropertyDeclaration,
                ClassDeclaration,
                EnumerationDeclaration,
                EnumerationMemberDeclaration,
                InterfaceDeclaration,
                NamespaceDeclaration,
                StructureDeclaration,
                UsingDirective,
                InterfaceEventDeclaration,
                InterfaceMethodDeclaration,
                InterfacePropertyDeclaration
            }
        }

    // Define the delegate method.
    delegate decimal CalculateBonus(decimal sales);

    // Define an Employee type.
    class Employee
    {
        public string name;
        public decimal sales;
        public decimal bonus;
        public CalculateBonus calculation_algorithm;
    }

    class Program
    {

        // This class will define two delegates that perform a calculation.
        // The first will be a named method, the second an anonymous delegate.

        // This is the named method.
        // It defines one possible implementation of the Bonus Calculation algorithm.

        static decimal CalculateStandardBonus(decimal sales)
        {
            return sales / 10;
        }

        static void Main(string[] args)
        {

            // A value used in the calculation of the bonus.
            // Note: This local variable will become a "captured outer variable".
            decimal multiplier = 2;

            // This delegate is defined as a named method.
            CalculateBonus standard_bonus = new CalculateBonus(CalculateStandardBonus);

            // This delegate is anonymous - there is no named method.
            // It defines an alternative bonus calculation algorithm.
            CalculateBonus enhanced_bonus = delegate(decimal sales) { return multiplier * sales / 10; };

            // Declare some Employee objects.
            Employee[] staff = new Employee[5];

            // Populate the array of Employees.
            for (int i = 0; i < 5; i++)
                staff[i] = new Employee();

            // Assign initial values to Employees.
            staff[0].name = "Mr Apple";
            staff[0].sales = 100;
            staff[0].calculation_algorithm = standard_bonus;

            staff[1].name = "Ms Banana";
            staff[1].sales = 200;
            staff[1].calculation_algorithm = standard_bonus;

            staff[2].name = "Mr Cherry";
            staff[2].sales = 300;
            staff[2].calculation_algorithm = standard_bonus;

            staff[3].name = "Mr Date";
            staff[3].sales = 100;
            staff[3].calculation_algorithm = enhanced_bonus;

            staff[4].name = "Ms Elderberry";
            staff[4].sales = 250;
            staff[4].calculation_algorithm = enhanced_bonus;

            // Calculate bonus for all Employees
            foreach (Employee person in staff)
                PerformBonusCalculation(person);

            // Display the details of all Employees
            foreach (Employee person in staff)
                DisplayPersonDetails(person);


        }

        public class Trace
        {
            [Conditional("DEBUG")]
            public static void Message(string traceMessage)
            {
                Console.WriteLine("[TRACE] - " + traceMessage);
            }
        } 

        public static void PerformBonusCalculation(Employee person)
        {

            // This method uses the delegate stored in the person object
            // to perform the calculation.
            // Note: This method knows about the multiplier local variable, even though
            // that variable is outside the scope of this method.
            // The multipler varaible is a "captured outer variable".
            person.bonus = person.calculation_algorithm(person.sales);
        }

        public static void DisplayPersonDetails(Employee person)
        {
            Console.WriteLine(person.name);
            Console.WriteLine(person.bonus);
            Console.WriteLine("---------------");
        }
    }

    }