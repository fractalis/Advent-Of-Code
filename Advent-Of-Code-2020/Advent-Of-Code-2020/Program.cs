﻿using System;
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
            runner.RunSolutionByName("Handheld Halting");
            runner.RunSolutionByName("Encoding Error");
            runner.RunSolutionByName("Adapter Array");
            runner.RunSolutionByName("Seating System");
            runner.RunSolutionByName("Rain Risk");
            runner.RunSolutionByName("Shuttle Search");
            runner.RunSolutionByName("Docking Data");
            runner.RunSolutionByName("Rambunctious Recitation");
            runner.RunSolutionByName("Ticket Translation");
            runner.RunSolutionByName("Conway Cubes");
            runner.RunSolutionByName("Operation Order");
        }
    }
}
