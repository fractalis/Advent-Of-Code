using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Advent_Of_Code_2020.Attributes;
using Advent_Of_Code_2020.Library;

namespace Advent_Of_Code_2020.PassportProcessing
{
    [AdventCalendarAttribute("Passport Processing", 4, "input/day04.txt")]
    public class PassportProcessing : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private Dictionary<String, Func<String, Boolean>> PassportCodes = new Dictionary<String, Func<String, Boolean>> {
            { "byr", (year) => Convert.ToInt32(year) >= 1920 && Convert.ToInt32(year) <= 2002},
            { "iyr", (year) => Convert.ToInt32(year) >= 2010 && Convert.ToInt32(year) <= 2020},
            { "eyr", (year) => Convert.ToInt32(year) >= 2020 && Convert.ToInt32(year) <= 2030},
            { "hgt", (height) => new Regex("^(1[5-8][0-9]cm|19[0-3]cm|59in|6[0-9]in|7[0-6]in)$").IsMatch(height)},
            { "hcl", (hcl) => new Regex("^(#[0-9a-f]{6})$").IsMatch(hcl) },
            { "ecl", (ecl) => new Regex("^(amb|blu|brn|gry|grn|hzl|oth)$").IsMatch(ecl) },
            { "pid", (pid) => new Regex("^([0-9]{9})$").IsMatch(pid) },
        };

        private int SolvePartOne(string[] input)
        {
            var validEntries = input.Aggregate( (str1, str2) => str1 + "\n" + str2)
                               .Split("\n\n")
                               .Select(passport => passport
                                                 .Split("\n ".ToCharArray())
                                                 .Select(fields => fields.Split(":"))
                                                 .ToDictionary(field => field[0], field => field[1]))
                                .Count( (entry) => PassportCodes.Keys.All(key => entry.ContainsKey(key)));
            return validEntries;
        }

        private int SolvePartTwo(string[] input)
        {
            var validEntries = input.Aggregate( (str1, str2) => str1 + "\n" + str2)
                                    .Split("\n\n")
                                    .Select(passport => passport
                                                    .Split("\n ".ToCharArray())
                                                    .Select(fields => fields.Split(":"))
                                                    .ToDictionary(field => field[0], field => field[1]))
                                    .Count( (entry) => PassportCodes.All( kvp => entry.ContainsKey(kvp.Key) && kvp.Value(entry[kvp.Key])));
            return validEntries;
        }
    }
}