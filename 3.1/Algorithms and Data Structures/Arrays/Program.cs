using System;

namespace Arrays
{
    static class Program
    {
        static StringWrapper Indent = new StringWrapper("  ");

        static void Main(string[] args)
        {
            OneDimensionalArrays();
            Console.WriteLine("\n");
            MultiDimensionalArrays();
            Console.WriteLine("\n");
            ArrayPointerIteration();
            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();
        }

        private static void OneDimensionalArrays()
        {
            Console.WriteLine("One Dimensional Arrays");
            // Method 1
            int[] a1;                                       // Variable declaration, memory not allocated
            a1 = new int[5];                                // Memory allocation
            ConsoleArray(a1, variableName: nameof(a1));

            // Method 2
            int[] a2 = new int[5];                          // Variable declaration and memory allocation
            ConsoleArray(a2, variableName: nameof(a2));

            // Method 3
            int[] a3 = new int[5] { 1, 2, 3, 4, 5 };        // Variable declaration and memory allocation with values
            ConsoleArray(a3, true, nameof(a3));

            // Method 4
            int[] a4 = { 6, 7, 8, 9, 10 };                  // Size allocated at variable declaration
            ConsoleArray(a4, variableName: nameof(a4), withIndex: true);

            // Method 5
            // Array myArray = new int[5];
            // which translates to
            Array myArray = Array.CreateInstance(typeof(int), 5);
            myArray.SetValue(11, 0);
            myArray.SetValue(12, 1);
            myArray.SetValue(13, 2);
            myArray.SetValue(14, 3);
            myArray.SetValue(15, 4);
            ConsoleArray(myArray as int[], withIndex: true, variableName: nameof(myArray));
        }

        private static void MultiDimensionalArrays()
        {
            Console.WriteLine("Multi Dimensional Arrays");

            // Method 1
            int[,] mdArr1 = new int[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };
            Console2DArray(mdArr1, nameof(mdArr1));

            // Method 2
            int[,] mdArr2 = { { 7, 8, 9 }, { 10, 11, 12 } };
            Console2DArray(mdArr2, nameof(mdArr2));

            // Method 3 : Jagged Arrays
            int[][] jaggedArray = new int[3][];
            jaggedArray[0] = new int[] { 1, 2, 3, 4 };
            jaggedArray[1] = new int[2] { 5, 6 };
            jaggedArray[2] = new int[3] { 7, 8, 9 };
            Console2DArray(jaggedArray, nameof(jaggedArray), true);

            jaggedArray = new int[][] { new int[] { 1, 2, 3, 4 }, new int[] { 5, 6 }, new int[] { 7, 8, 9 } };
            Console2DArray(jaggedArray, nameof(jaggedArray));
        }

        private static unsafe void ArrayPointerIteration()
        {
            Console.WriteLine("Pointer Iteration on Integer Arrays");

            int[] iterIntArray = new int[] { int.MinValue, int.MaxValue, int.MinValue };
            // fixed keyword tells the garbage collector to fix the address
            fixed (int* array = iterIntArray)
            {
                int* p = array;
                for (int i = 0; i < iterIntArray.Length; i++)
                {
                    Console.WriteLine($"{Indent}Value at address {(long)p} = {*p}");
                    p++;
                }
            }

            Console.WriteLine("\nPointer Iteration on Byte Arrays");

            byte[] iterByteArray = new byte[] { byte.MinValue, byte.MaxValue, byte.MinValue };
            // fixed keyword tells the garbage collector to fix the address
            fixed (byte* array = iterByteArray)
            {
                byte* p = array;
                for (int i = 0; i < iterByteArray.Length; i++)
                {
                    Console.WriteLine($"{Indent}Value at address {(long)p} = {*p}");
                    p++;
                }
            }

            Console.WriteLine("\nPointer Iteration on Char Arrays");

            char[] iterCharArray = new char[] { 'a', 'z', 'A', 'Z', char.MinValue, char.MaxValue, char.MinValue };
            // fixed keyword tells the garbage collector to fix the address
            fixed (char* array = iterCharArray)
            {
                char* p = array;
                for (int i = 0; i < iterCharArray.Length; i++)
                {
                    Console.WriteLine($"{Indent}Value at address {(long)p} = {*p}");
                    p++;
                }
            }
        }

        private static void ConsoleArray(int[] array,bool withIndex = false, string variableName = "array", string multiIndent = "")
        {
            Console.Write(multiIndent + Indent + variableName + " = ");
            foreach (var item in array)
            {
                Console.Write($"{item} ");
            }
            Console.Write("\n");

            if (withIndex)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    Console.Write(multiIndent + (Indent * 2));
                    Console.WriteLine($"{variableName}[{i}] = {array[i]}");
                }
                Console.Write("\n");
            }
        }

        private static void Console2DArray(int[,] array, string variableName = "2Darray")
        {
            Console.WriteLine(Indent + variableName + " :");
            for (int i = 0; i < array.GetLength(0); i++)
            {
                Console.Write(Indent * 2);
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write($"{array[i, j]} ");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        private static void Console2DArray(int[][] array, string variableName = "2Darray", bool withIndex = false)
        {
            Console.WriteLine(Indent + variableName + " :");
            for (int i = 0; i < array.GetLength(0); i++)
            {
                ConsoleArray(array[i], withIndex, $"{variableName}[{i}]", multiIndent: Indent);
            }
            Console.Write("\n");
        }
    }
}
