using System;
using System.Collections;
using System.Collections.Generic;

namespace LearningCSharp
{
    internal class CollectionsDemo
    {
        public static void RunCollectionsDemo()
        {
            #region ArrayList (Non-Generic)
            /*
             * ArrayList
             * When to use: Use for dynamically resizing collections where type safety is not a concern.
             * Pros:
             *   - Allows mixed data types.
             *   - Resizable.
             * Cons:
             *   - Lacks type safety.
             *   - Requires boxing/unboxing for value types.
             * Common Methods:
             *   - Add(object item)
             *   - Remove(object item)
             *   - Insert(int index, object item)
             *   - RemoveAt(int index)
             *   - Contains(object item)
             *   - IndexOf(object item)
             */

            ArrayList arrayList = new ArrayList { 1, "Two", 3.0 };
            arrayList.Add(4);
            arrayList.Remove("Two");
            Console.WriteLine("ArrayList Elements:");
            //PrintCollection(arrayList);
            Console.WriteLine("Capacity : "+ arrayList.Capacity);//starts with initial or 0 and keeps doubling the capacity to match needs
            Console.WriteLine("Count : "+ arrayList.Count);// number of elements in array
           
            #endregion

            #region List<T> (Generic)
            /*
             * List<T>
             * When to use: For type-safe, dynamically resizable collections.
             * Pros:
             *   - Type-safe and no boxing/unboxing.
             *   - Rich methods for manipulation.
             * Cons:
             *   - Fixed to one type, unlike ArrayList.
             * Common Methods:
             *   - Add(T item), Remove(T item), Insert(int index, T item)
             *   - Sort(), Contains(T item), IndexOf(T item)
             */

            List<int> list = new List<int> { 1, 2, 3 };
            list.Add(4);
            list.Remove(2);
            Console.WriteLine("List<T> Elements:");
            //PrintCollection(list);
            #endregion

            #region LinkedList<T>
            /*
             * LinkedList<T>
             * When to use: For frequent insertions/deletions at both ends or in the middle.
             * Pros:
             *   - Efficient insertion/deletion.
             * Cons:
             *   - Slower access to elements (no index-based access).
             * Common Methods:
             *   - AddFirst(T item), AddLast(T item)
             *   - Remove(T item), Find(T item)
             */

            LinkedList<int> linkedList = new LinkedList<int>();
            linkedList.AddLast(1);
            linkedList.AddFirst(0);
            linkedList.AddLast(2);
            linkedList.Remove(1);
            Console.WriteLine("LinkedList<T> Elements:");
            //PrintCollection(linkedList);
            #endregion

            #region HashSet<T>
            /*
             * HashSet<T>
             * When to use: For collections with unique elements and no duplicates.
             * Pros:
             *   - Fast lookup and insertion.
             *   - Ensures uniqueness.
             * Cons:
             *   - No order of elements.
             * Common Methods:
             *   - Add(T item), Remove(T item)
             *   - Contains(T item), UnionWith(IEnumerable<T>), IntersectWith(IEnumerable<T>)
             */

            HashSet<int> hashSet = new HashSet<int> { 1, 2, 3, 4 };
            hashSet.Add(5);
            hashSet.Remove(3);
            Console.WriteLine("HashSet<T> Elements:");
            //PrintCollection(hashSet);
            #endregion

            #region SortedSet<T>
            /*
             * SortedSet<T>
             * When to use: For unique elements that must be in sorted order.
             * Pros:
             *   - Automatically sorted.
             * Cons:
             *   - Slower than HashSet for insertion and lookup.
             * Common Methods:
             *   - Add(T item), Remove(T item), Contains(T item)
             */

            SortedSet<int> sortedSet = new SortedSet<int> { 3, 1, 2 };
            sortedSet.Add(4);
            sortedSet.Remove(1);
            Console.WriteLine("SortedSet<T> Elements:");
            //PrintCollection(sortedSet);
            #endregion

            #region Dictionary<TKey, TValue>
            /*
             * Dictionary<TKey, TValue>
             * When to use: For key-value pairs with fast lookups.
             * Pros:
             *   - Fast retrieval by key.
             * Cons:
             *   - No guaranteed order.
             * Alternative : Hashtable hashtable = new Hashtable(); // non generic , stores key and value in objects and its slow and not thread safe.
             * Common Methods:
             *   - Add(TKey key, TValue value)
             *   - Remove(TKey key), ContainsKey(TKey key), TryGetValue(TKey key, out TValue value)
             */

            Dictionary<string, int> dictionary = new Dictionary<string, int>

            {
                { "One", 1 },
                { "Two", 2 }
            };
            dictionary["Three"] = 3; // Add or update
            dictionary.Remove("Two");
            Console.WriteLine("Dictionary Elements:");
            foreach (var kvp in dictionary)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
            #endregion

            #region SortedList<TKey, TValue>
            /*
             * SortedList<TKey, TValue>
             * When to use: For key-value pairs that need to be sorted.
             * Pros:
             *   - Automatically sorted by key.
             * Cons:
             *   - Slower insertion and removal compared to Dictionary.
             * Common Methods:
             *   - Add(TKey key, TValue value)
             *   - Remove(TKey key), ContainsKey(TKey key)
             */

            SortedList<int, string> sortedList = new SortedList<int, string>
            {
                { 2, "Two" },
                { 1, "One" }
            };
            sortedList.Add(3, "Three");
            sortedList.Remove(1);
            Console.WriteLine("SortedList Elements:");
            foreach (var kvp in sortedList)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
            #endregion

            #region Queue<T> and Stack<T>
            /*
             * Queue<T>
             * When to use: For first-in, first-out (FIFO) operations.
             * Stack<T>
             * When to use: For last-in, first-out (LIFO) operations.
             */

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            Console.WriteLine($"Queue Front: {queue.Dequeue()}");

            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            Console.WriteLine($"Stack Top: {stack.Pop()}");
            #endregion
        }

        // Helper method to print any collection
        //private static void PrintCollection<T>(IEnumerable<T> collection)
        //{
        //    foreach (var item in collection)
        //        Console.WriteLine(item);
        //}

    }
}
