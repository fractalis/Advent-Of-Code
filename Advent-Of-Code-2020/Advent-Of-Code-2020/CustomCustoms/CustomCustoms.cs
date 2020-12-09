using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Advent_Of_Code_2020.Attributes;
using Advent_Of_Code_2020.Library;

namespace Advent_Of_Code_2020.CustomCustoms
{
    [AdventCalendarAttribute("Custom Customs", 6, "input/day06.txt")]
    public class CustomCustoms : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private int SolvePartOne(string[] input)
        {
            var sum = input.Aggregate((str1, str2) => str1 + "\n" + str2)
                                 .Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
                                 .Select(i => i.Replace("\n", "").Distinct().Count())
                                 .Sum();
            
            return sum;
        }

        private int SolvePartTwo(string[] input)
        {
            var sum = input.Aggregate((str1, str2) => str1 + "\n" + str2)
                           .Split("\n\n").Select(str => str
                                                .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                                                .Aggregate((str1, str2) => String.Concat(str1.Intersect(str2))).Length).Sum();
            Console.WriteLine(sum);
            return 0;
        }
    }
}