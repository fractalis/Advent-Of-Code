using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;
using Advent_Of_Code_2020.Attributes;
using Advent_Of_Code_2020.Library;

namespace Advent_Of_Code_2020.HandheldHalting
{
    [AdventCalendarAttribute("Handheld Halting", 8, "input/day08.txt")]
    public class HandheldHalting : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private int SolvePartOne(string[] input)
        {
            var computer = new Computer(input);
            computer.Execute();
            return computer.Accumulator;
        }

        private int SolvePartTwo(string[] input)
        {
            var computer = new Computer(input);
            computer.PatchCode();
            return computer.Accumulator;
        }
    }
}