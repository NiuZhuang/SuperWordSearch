using System;

namespace SuperWordSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please enter the file name:");
                var fileName = Console.ReadLine();

                var input = new InputProcessor(fileName);

                input.DisplayResult();
            }
        }
    }
}
