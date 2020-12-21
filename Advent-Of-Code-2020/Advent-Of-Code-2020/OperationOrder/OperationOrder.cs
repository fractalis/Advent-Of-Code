using System;
using System.Collections.Generic;
using System.Linq;
using Advent_Of_Code_2020.Attributes;
using Advent_Of_Code_2020.Library;

namespace Advent_Of_Code_2020.OperationOrder
{
    [AdventCalendarAttribute("Operation Order", 18, "input/day18.txt")]
    public class OperationOrder : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private long SolvePartOne(string[] input)
        {
            return input.Select(e => Solve(e, false)).Sum();
        }

        private long SolvePartTwo(string[] input)
        {
            return input.Select(e => Solve(e, true)).Sum();
        }

        private long Solve(string line, Boolean isPart2)
        {
            Equation equation = Equation.ParseEquation(line);
            return equation.Solve(isPart2);
        }
    }
}