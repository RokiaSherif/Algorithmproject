using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSortt
{
    internal class Program
    {
       
class HeapSort
    {
        public static void Sort(int[] array)
        {
            int n = array.Length;

            // Build max heap
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(array, n, i);
            }

            // Extract elements from heap one by one
            for (int i = n - 1; i > 0; i--)
            {
                // Swap the current root (largest) with the last element
                Swap(array, 0, i);

                // Call Heapify on the reduced heap
                Heapify(array, i, 0);
            }
        }

        private static void Heapify(int[] array, int n, int i)
        {
            int largest = i; // Initialize largest as root
            int left = 2 * i + 1; // Left child index
            int right = 2 * i + 2; // Right child index

            // If the left child is larger than root
            if (left < n && array[left] > array[largest])
            {
                largest = left;
            }

            // If the right child is larger than the largest so far
            if (right < n && array[right] > array[largest])
            {
                largest = right;
            }

            // If the largest is not the root
            if (largest != i)
            {
                Swap(array, i, largest);

                // Recursively heapify the affected sub-tree
                Heapify(array, n, largest);
            }
        }

        private static void Swap(int[] array, int a, int b)
        {
            int temp = array[a];
            array[a] = array[b];
            array[b] = temp;
        }

        public static void Main(string[] args)
        {
            int[] array = { 192, 151, 13, 25, 66, 97 };

            Console.WriteLine("Original array:");
            Console.WriteLine(string.Join(" ", array));

            Sort(array);

            Console.WriteLine("\nSorted array:");
            Console.WriteLine(string.Join(" ", array));
        }
    }

}
}
