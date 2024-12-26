using System;

namespace LearningCSharp
{
    #region Base Class: Animal

    /// <summary>
    /// The base class representing a generic Animal.
    /// </summary>
    public class Animal
    {
        /// <summary>
        /// Makes a sound. This method will be overridden in derived classes.
        /// </summary>
        public virtual void MakeSound()
        {
            Console.WriteLine("Some generic animal sound...");
        }
    }

    #endregion

    #region Derived Class: Dog

    /// <summary>
    /// The Dog class inherits from the Animal class.
    /// </summary>
    public class Dog : Animal
    {
        /// <summary>
        /// Overriding the MakeSound method to provide Dog-specific implementation.
        /// </summary>
        public override void MakeSound()
        {
            Console.WriteLine("Bhaaavvv! Bhaaaav!");
        }
    }

    #endregion

    #region Derived Class: Cat

    /// <summary>
    /// The Cat class inherits from the Animal class.
    /// </summary>
    public class Cat : Animal
    {
        /// <summary>
        /// Overriding the MakeSound method to provide Cat-specific implementation.
        /// </summary>
        public override void MakeSound()
        {
            Console.WriteLine("Meow! Meow!");
        }
    }

    #endregion

    #region Derived Class: Bird

    /// <summary>
    /// The Bird class inherits from the Animal class.
    /// </summary>
    public class Bird : Animal
    {
        /// <summary>
        /// Overriding the MakeSound method to provide Bird-specific implementation.
        /// </summary>
        public override void MakeSound()
        {
            Console.WriteLine("Chiiiii ! Chiiiii!");
        }
    }

    #endregion

    internal class PolymorphismDemo
    {
        public static void RunPolymorphismDemo()
        {
            #region Demonstrating Runtime Polymorphism (Method Overriding)

            // Polymorphism in action - we are calling the base class method
            // but the derived class implementation is executed.
            Console.WriteLine("=== Demonstrating Runtime Polymorphism ===");

            Animal myAnimal = new Animal(); // Base class object
            Animal myDog = new Dog();       // Derived class object
            Animal myCat = new Cat();       // Derived class object
            Animal myBird = new Bird();     // Derived class object

            myAnimal.MakeSound();  // Output: Some generic animal sound...
            myDog.MakeSound();     // Output: Bhaaav! Bhaaav!
            myCat.MakeSound();     // Output: Meow! Meow!
            myBird.MakeSound();    // Output: Chiiii! Chiiii!

            #endregion

            #region Demonstrating Compile-time Polymorphism (Method Overloading)

            Console.WriteLine("\n=== Demonstrating Compile-time Polymorphism ===");

            // Call overloaded methods with different parameters
            Console.WriteLine($"Addition of 2 and 3: {Add(2, 3)}");
            Console.WriteLine($"Addition of 2, 3, and 4: {Add(2, 3, 4)}");
            Console.WriteLine($"Addition of 2.5 and 3.5: {Add(2.5, 3.5)}");

            #endregion
        }

        #region Method Overloading: Add Methods

        /// <summary>
        /// Adds two integers.
        /// </summary>
        public static int Add(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// Adds three integers.
        /// </summary>
        public static int Add(int a, int b, int c)
        {
            return a + b + c;
        }

        /// <summary>
        /// Adds two floating-point numbers.
        /// </summary>
        public static double Add(double a, double b)
        {
            return a + b;
        }

        #endregion
    }
}
