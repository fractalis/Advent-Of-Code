using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Advent_Of_Code_2020.Attributes;
using Advent_Of_Code_2020.Library;

namespace Advent_Of_Code_2020.EncodingError
{
    [AdventCalendarAttribute("Encoding Error", 9, "input/day09.txt")]
    public class EncodingError : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private long SolvePartOne(string[] input)
        {
            var nums = input.Select(x => long.Parse(x)).ToArray();
            int preambleSize = 25;
            for(int i = preambleSize + 1; i < nums.Length; i++)
            {
                var target = nums[i];
                var preamble = nums.Skip(i - preambleSize).Take(preambleSize);
                var foundValue = from x in preamble
                             let y = target - x
                             where preamble.Contains(y)
                             select 1;
                if(!foundValue.Any()) {
                    return target;
                }
            }
            return -1;
        }

        private long SolvePartTwo(string[] input)
        {
            var target = SolvePartOne(input);
            var nums = input.Select(x => long.Parse(x)).ToArray();
            (int startI, int endI) = (0, 1);
            long[] range;

            while( endI < nums.Length)
            {
                var sum = nums[new Range(startI, endI)].Sum();
                if(sum == target) {
                    range = nums[new Range(startI, endI)];
                    return range.Min() + range.Max();
                }
                else if(sum < target) { endI++; }
                else { startI++; }
            }
            range = nums[new Range(startI, endI)];
            return range.Min() + range.Max();
        }
    }
}