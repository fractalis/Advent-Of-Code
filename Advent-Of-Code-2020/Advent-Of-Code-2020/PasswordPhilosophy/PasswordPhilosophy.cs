using System.Collections.Generic;
using System.Linq;
using System;

using Advent_Of_Code_2020.Attributes;
using Advent_Of_Code_2020.Library;
using System.ComponentModel.DataAnnotations;

namespace Advent_Of_Code_2020.PasswordPhilosophy
{
    record PasswordEntry (int Min, int Max, char Letter, string Entry);

    [AdventCalendarAttribute("Password Philosophy", 2, "input/day02.txt")]
    public class PasswordPhilosophy : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private int SolvePartOne(string[] input)
        {
            return CreatePasswordEntries(input).Count( entry => {
                var letterCount = entry.Entry.Count(ch => ch == entry.Letter);
                return letterCount >= entry.Min && letterCount <= entry.Max;
            });
        }

        private int SolvePartTwo(string[] input)
        {
            return CreatePasswordEntries(input).Where( entry => {
                return entry.Entry[entry.Min-1] == entry.Letter ^ entry.Entry[entry.Max-1] == entry.Letter;
            }).Count();
        }

        private IEnumerable<PasswordEntry> CreatePasswordEntries(string[] input)
        {
            return input.Select( line => {
                var parts = line.Split(" ");
                var range = parts[0].Split("-").Select(int.Parse).ToArray();
                var ch = parts[1][0];
                return new PasswordEntry(range[0], range[1], ch, parts[2]);
            });
        }
    }
}