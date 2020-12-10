using System;
using Advent_Of_Code_2020.Library;

namespace Advent_Of_Code_2020
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
            runner.RunSolutionByName("Report Repair");
            runner.RunSolutionByName("Password Philosophy");
            runner.RunSolutionByName("Toboggan Trajectory");
            runner.RunSolutionByName("Passport Processing");
            runner.RunSolutionByName("Binary Boarding");
            runner.RunSolutionByName("Custom Customs");
            runner.RunSolutionByName("Handy Haversacks");
        }
    }
}
