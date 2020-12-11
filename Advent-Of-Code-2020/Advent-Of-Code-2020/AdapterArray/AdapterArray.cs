using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Transactions;
using Advent_Of_Code_2020.Attributes;
using Advent_Of_Code_2020.Library;

namespace Advent_Of_Code_2020.AdapterArray
{
    [AdventCalendarAttribute("Adapter Array", 10, "input/day10.txt")]
    public class AdapterArray : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private int SolvePartOne(string[] input)
        {
            var joltages = CreateJoltageList(input);

            var differences = joltages.Select( (joltage, index) => (index + 1) >= joltages.Count ? 0 : joltages[index+1] - joltage);

            
            return differences.Count(difference => difference == 1) * differences.Count(difference => difference == 3);
        }

        private long SolvePartTwo(string[] input)
        {
            var joltages = CreateJoltageList(input);
            return CountArrangements(new Dictionary<int, long>(), joltages.ToHashSet(), joltages.First(), joltages.Max());
        }

        private long CountArrangements(Dictionary<int, long> arrangements, HashSet<int> joltages, int currentJoltage, int target)
        {
            if(currentJoltage == target) { return 1L; }
            else if(arrangements.ContainsKey(currentJoltage)) { return arrangements[currentJoltage]; }

            var count = 0L;
            var nextThree = joltages.Take(3).ToArray();

            if(joltages.Contains(currentJoltage+1))
            {
                count += CountArrangements(arrangements, joltages, currentJoltage+1, target);
            }
            if(joltages.Contains(currentJoltage+2))
            {
                count += CountArrangements(arrangements, joltages, currentJoltage+2, target);
            }
            if(joltages.Contains(currentJoltage+3))
            {
                count += CountArrangements(arrangements, joltages, currentJoltage+3, target);
            }
            arrangements.Add(currentJoltage, count);

            return arrangements[currentJoltage];
        }

        private List<int> CreateJoltageList(string[] input)
        {
            var joltages = input.Select(str => int.Parse(str)).OrderBy(x => x).ToArray();
            var fullJoltages = new List<int>
            {
                0
            };
            fullJoltages.AddRange(joltages);
            fullJoltages.Add(joltages.Max()+3);
            return fullJoltages;
        }
    }
}