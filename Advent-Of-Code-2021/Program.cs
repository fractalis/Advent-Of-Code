using System;
using Advent_Of_Code_2021.Library;

namespace Advent_Of_Code_2021
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new Runner();
            Console.WriteLine("List of Solutions");
            Console.WriteLine("-----------------");
            runner.ListSolutions();
            Console.WriteLine("-----------------");
            runner.RunSolutionByName("Sonar Sweep");
        }
    }
}
