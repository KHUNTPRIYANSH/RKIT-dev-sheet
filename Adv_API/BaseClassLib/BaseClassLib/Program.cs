using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseClassLib
{
    /// <summary>
    /// A custom implementation of List<T> with additional constraints and logging features.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class CustomList<T> : List<T>
    {
        #region Fields

        /// <summary>
        /// Maximum capacity for the list.
        /// </summary>
        private readonly int _capacity;

        /// <summary>
        /// Threshold percentage for warning when the list is nearly full.
        /// </summary>
        private const double ThresholdPercentage = 0.9;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomList{T}"/> class with a specified capacity.
        /// </summary>
        /// <param name="capacity">The maximum number of elements that the list can hold.</param>
        public CustomList(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");
            }

            _capacity = capacity;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a unique item to the list and logs the operation.
        /// </summary>
        /// <param name="item">The item to add to the list.</param>
        public new void Add(T item)
        {
            if (Count >= _capacity)
            {
                throw new InvalidOperationException("Cannot add item: List has reached its maximum capacity.");
            }

            if (Contains(item))
            {
                Console.WriteLine("Item already exists. Only unique items are allowed.");
                return;
            }

            base.Add(item);
            Console.WriteLine($"Item added: {item}");

            if (Count >= _capacity * ThresholdPercentage)
            {
                Console.WriteLine("Warning: List is 90% full.");
            }
        }

        /// <summary>
        /// Removes the specified item from the list after confirmation.
        /// </summary>
        /// <param name="item">The item to remove from the list.</param>
        /// <returns>True if the item was successfully removed; otherwise, false.</returns>
        public new bool Remove(T item)
        {
            if (!Contains(item))
            {
                Console.WriteLine("Item not found in the list.");
                return false;
            }

            Console.WriteLine($"Are you sure you want to remove the item: {item}? (y/n)");
            var confirmation = Console.ReadLine();
            if (confirmation?.ToLower() != "y")
            {
                Console.WriteLine("Remove operation canceled.");
                return false;
            }

            bool result = base.Remove(item);
            if (result)
            {
                Console.WriteLine($"Item removed: {item}");
            }
            return result;
        }

        /// <summary>
        /// Inserts a unique item at the specified index and logs the operation.
        /// </summary>
        /// <param name="index">The zero-based index at which the item should be inserted.</param>
        /// <param name="item">The item to insert.</param>
        public new void Insert(int index, T item)
        {
            if (Count >= _capacity)
            {
                throw new InvalidOperationException("Cannot insert item: List has reached its maximum capacity.");
            }

            if (Contains(item))
            {
                Console.WriteLine("Item already exists. Only unique items are allowed.");
                return;
            }

            base.Insert(index, item);
            Console.WriteLine($"Item inserted at index {index}: {item}");
        }

        #endregion
    }

    #region Test Program

    class Program
    {
        static void Main(string[] args)
        {
            CustomList<int> customList = new CustomList<int>(5);

            customList.Add(1);
            customList.Add(2);
            customList.Add(3);
            customList.Add(3);  // Duplicate test
            customList.Add(4);
            customList.Add(5);  // Capacity test

            customList.Remove(2);

            customList.Insert(1, 6);
            customList.Remove(10); // Non-existent item test
        }
    }

    #endregion
}
