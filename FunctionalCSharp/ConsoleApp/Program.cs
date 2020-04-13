using System;

using FunctionalCSharp;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TestStack();
        }

        static void TestStack()
        {
            var stack = ImmutableStack<int>.Empty
                        .Push(1)
                        .Push(3)
                        .Push(5);

            foreach(var value in stack)
            {
                Console.WriteLine(value);
            }
        }
    }
}
