using System;

namespace ClassDemo
{
    #region Instance Class
    /// <summary>
    /// This is an instance class which can be instantiated multiple times.
    /// This is the class that we normally use in our code most of times. 
    /// </summary>
    public class InstanceClass
    {
        public string Name { get; set; }

        public InstanceClass(string name)
        {
            Name = name;
        }

        public void Display()
        {
            Console.WriteLine("Instance Class: " + Name);
        }
    }
    #endregion

    #region Static Class
    /// <summary>
    /// This is a static class. It cannot be instantiated, and all members must be static.
    /// </summary>
    public static class StaticClass
    {
        public static int someStaticMember = 5;
        public static void DisplayMessage()
        {
            Console.WriteLine("Static Class: This is a static method.");
            Console.WriteLine("Static Class Member: " + someStaticMember);
        }
    }
    #endregion

    #region Singleton Class
    /// <summary>
    /// Singleton class ensures that only one instance of the class exists.
    /// </summary>
    public class SingletonClass
    {
        // step1 : create only single obj of class here we have created instance in following line
        //         here we have just defined the variable of a class not given any ref as value
        //         so default it's null

        private static SingletonClass instance;

        // step2: make constructor private so no one can create obj of it
        private SingletonClass() { }

        // step3: define getter and setter 
        public static SingletonClass Instance
        {
            get
            {
                // checking if thier are any obj of the class or not
                if (instance == null)
                {
                    // if not we create single obj and return it
                    instance = new SingletonClass();
                }
                return instance;
            }
        }

        public void ShowMessage()
        {
            Console.WriteLine("Singleton Class: Only one instance of this class exists.");
        }
    }
    #endregion

    #region Partial Class
    /// <summary>
    /// Partial class is used to split the class definition across multiple files.
    /// </summary>
    public partial class PartialClass
    {
        public void MethodOne()
        {
            Console.WriteLine("Partial Class Method One");
        }
    }

    // Another part of the PartialClass (this would typically be in a different file)
    public partial class PartialClass
    {
        public void MethodTwo()
        {
            Console.WriteLine("Partial Class Method Two");
        }
    }
    
    #endregion

    #region Nested Class
    /// <summary>
    /// Nested class is defined inside another class.
    /// </summary>
    public class OuterClass
    {
        public class NestedClass
        {
            public void DisplayMessage()
            {
                Console.WriteLine("Nested Class: Inside OuterClass.");
            }
        }
    }
    #endregion

    #region Abstract Class
    /// <summary>
    /// Abstract class cannot be instantiated directly and may contain abstract methods
    /// that must be implemented by derived classes.
    /// </summary>
    public abstract class AbstractClass
    {
        // non abstract member
        public string Name { get; set; }

        // constructor
        public AbstractClass(string name)
        {
            Name = name;
        }

        public abstract void Display(); // Abstract method

        // non abstract method
        public void ShowName()
        {
            Console.WriteLine("Abstract Class: " + Name);
        }
    }

    public class DerivedClass : AbstractClass
    {
        // base class constructor calling constructor of parent class
        public DerivedClass(string name) : base(name) { }

        public override void Display() 
        {
            Console.WriteLine("Derived Class Display: " + Name);
        }
    }
    #endregion

    #region Sealed Class
    /// <summary>
    /// Sealed class cannot be inherited.
    /// </summary>
    public sealed class SealedClass
    {
        public string Message { get; set; }

        public SealedClass(string message)
        {
            Message = message;
        }

        public void ShowMessage()
        {
            Console.WriteLine("Sealed Class: " + Message);
        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            #region Demonstrating Instance Class
            Console.WriteLine("---- Instance Class ----");
            InstanceClass instance = new InstanceClass("Instance Class Example");
            instance.Display();
            #endregion

            #region Demonstrating Static Class
            Console.WriteLine("---- Static Class ----");
            StaticClass.DisplayMessage();
            #endregion

            #region Demonstrating Singleton Class
            Console.WriteLine("---- Singleton Class ----");
            SingletonClass singleton1 = SingletonClass.Instance;
            singleton1.ShowMessage();
            SingletonClass singleton2 = SingletonClass.Instance;
            singleton2.ShowMessage();
            Console.WriteLine($"Are both instances the same? {ReferenceEquals(singleton1, singleton2)}");
            #endregion

            #region Demonstrating Partial Class
            Console.WriteLine("---- Partial Class ----");
            PartialClass partial = new PartialClass();
            partial.MethodOne();
            partial.MethodTwo();
            #endregion

            #region Demonstrating Nested Class
            Console.WriteLine("---- Nested Class ----");
            OuterClass.NestedClass nested = new OuterClass.NestedClass();
            nested.DisplayMessage();
            #endregion

            #region Demonstrating Abstract Class
            Console.WriteLine("---- Abstract Class ----");
            DerivedClass derived = new DerivedClass("Abstract Example");
            derived.Display();
            derived.ShowName();
            #endregion

            #region Demonstrating Sealed Class
            Console.WriteLine("---- Sealed Class ----");
            SealedClass sealedObj = new SealedClass("Sealed Class Message");
            sealedObj.ShowMessage();
            #endregion
        }
    }
}
