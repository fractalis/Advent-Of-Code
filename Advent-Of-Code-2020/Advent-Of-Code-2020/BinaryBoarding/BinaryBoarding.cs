using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Advent_Of_Code_2020.Attributes;
using Advent_Of_Code_2020.Library;

namespace Advent_Of_Code_2020.BinaryBoarding
{
    [AdventCalendarAttribute("Binary Boarding", 5, "input/day05.txt")]
    public class BinaryBoarding : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        public int SolvePartOne(string[] input)
        {
            var maxBoardingId = input
                                    .Select( row => 
                                        Convert.ToInt32(new string(row.Select( ch => ch == 'B' || ch == 'R' ? '1' : '0').ToArray()), 2))
                                    .Max();
            return maxBoardingId;
        }

        public int SolvePartTwo(string[] input)
        {
            var seatIds = input
                .Select( row =>
                    Convert.ToInt32(new string(row.Select( ch => ch == 'B' || ch == 'R' ? '1' : '0').ToArray()), 2))
                .OrderBy(seatId => seatId);
            (int minId, int maxId) = (seatIds.Min(), seatIds.Max());
            return Enumerable.Range(minId, maxId - minId + 1).Single(s => !seatIds.Contains(s));
        }
    }
}