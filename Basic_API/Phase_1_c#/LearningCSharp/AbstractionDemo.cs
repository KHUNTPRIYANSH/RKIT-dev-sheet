using System;

namespace LearningCSharp
{
    #region Abstract Class Shape

    /// <summary>
    /// Abstract class Shape to demonstrate abstraction using abstract methods and non-abstract methods.
    /// </summary>
    public abstract class Shape
    {
        #region Abstract Method

        /// <summary>
        /// Abstract method to be implemented by derived classes.
        /// </summary>
        public abstract void Draw();

        #endregion

        #region Non-Abstract Method

        /// <summary>
        /// Displays common details for all shapes.
        /// </summary>
        public void ShowDetails()
        {
            Console.WriteLine("This is a shape.");
        }

        #endregion
    }

    #endregion

    #region Derived Classes (Abstract Class Implementation)

    /// <summary>
    /// Derived class Circle, which implements the abstract method Draw.
    /// </summary>
    public class Circle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Drawing a Circle.");
        }
    }

    /// <summary>
    /// Derived class Rectangle, which implements the abstract method Draw.
    /// </summary>
    public class Rectangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Drawing a Rectangle.");
        }
    }

    /// <summary>
    /// Derived class Triangle, which implements the abstract method Draw.
    /// </summary>
    public class Triangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Drawing a Triangle.");
        }
    }

    #endregion

    #region Interface IShape

    /// <summary>
    /// Interface IShape to demonstrate abstraction using interfaces.
    /// </summary>
    public interface IShape
    {
        #region Abstract Method

        /// <summary>
        /// Abstract method to be implemented by implementing classes.
        /// </summary>
        void Draw();

        #endregion
    }

    #endregion

    #region Classes Implementing Interface

    /// <summary>
    /// Circle class implementing the IShape interface.
    /// </summary>
    public class CircleByInterface : IShape
    {
        public void Draw()
        {
            Console.WriteLine("Drawing a Circle using Interface.");
        }
    }

    /// <summary>
    /// Rectangle class implementing the IShape interface.
    /// </summary>
    public class RectangleByInterface : IShape
    {
        public void Draw()
        {
            Console.WriteLine("Drawing a Rectangle using Interface.");
        }
    }

    /// <summary>
    /// Triangle class implementing the IShape interface.
    /// </summary>
    public class TriangleByInterface : IShape
    {
        public void Draw()
        {
            Console.WriteLine("Drawing a Triangle using Interface.");
        }
    }

    #endregion

    #region AbstractionDemo Class

    /// <summary>
    /// Class to demonstrate abstraction using abstract classes and interfaces.
    /// </summary>
    internal class AbstractionDemo
    {
        public static void RunAbstractionDemo()
        {
            #region Demonstration of Abstraction using Abstract Classes

            Console.WriteLine("=== Abstraction using Abstract Classes ===");

            // Create objects of derived classes
            Shape myCircle = new Circle();
            Shape myRectangle = new Rectangle();
            Shape myTriangle = new Triangle();

            // Call the Draw method of each derived class object
            myCircle.Draw();       // Output: Drawing a Circle.
            myRectangle.Draw();    // Output: Drawing a Rectangle.
            myTriangle.Draw();     // Output: Drawing a Triangle.

            // Call the ShowDetails method (inherited from Shape)
            myCircle.ShowDetails(); // Output: This is a shape.
            myRectangle.ShowDetails(); // Output: This is a shape.
            myTriangle.ShowDetails(); // Output: This is a shape.

            #endregion

            #region Demonstration of Abstraction using Interfaces

            Console.WriteLine("\n=== Abstraction using Interfaces ===");

            // Create objects of classes implementing the interface
            IShape circleInterface = new CircleByInterface();
            IShape rectangleInterface = new RectangleByInterface();
            IShape triangleInterface = new TriangleByInterface();

            // Call the Draw method
            circleInterface.Draw();       // Output: Drawing a Circle using Interface.
            rectangleInterface.Draw();    // Output: Drawing a Rectangle using Interface.
            triangleInterface.Draw();     // Output: Drawing a Triangle using Interface.

            #endregion
        }
    }

    #endregion

}
