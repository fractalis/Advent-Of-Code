using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Advent_Of_Code_2020.Attributes;
using Advent_Of_Code_2020.Library;

namespace Advent_Of_Code_2020.HandyHaversacks
{
    [AdventCalendarAttribute("Handy Haversacks", 7, "input/day07.txt")]
    public class HandyHaversacks : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private int SolvePartOne(string[] input)
        {
            var normalized = input.Select(str => str.Replace("bags", "bag")
                                                    .Replace(".", "")
                                                    .Split("bag contain"));
            Dictionary<string, List<(int, string)>> Rules = ParseRules(normalized);
            Dictionary<string, HashSet<string>> targetContainer = new Dictionary<string, HashSet<string>>();

            var target = "shiny gold";
            var count = 0;
            targetContainer[target] = new HashSet<string>();

            foreach(KeyValuePair<string, List<(int, string)>> kvp in Rules)
            {
                if(CanHoldTargetBag(target,kvp.Key).Any(x => x == true)) {
                    count++;
                }
            }

            IEnumerable<bool> CanHoldTargetBag(string target, string key)
            {
                if(Rules[key].Count == 0)
                {
                    yield return false;
                }
                else if(CanHoldBag(target, Rules[key]))
                {
                    yield return true;
                    foreach((int count, string bag) in Rules[key])
                    {
                        foreach(var canHoldT in CanHoldTargetBag(target, bag))
                        {
                            yield return canHoldT;
                        }
                    }
                }
                else
                {
                    foreach((int count, string bag) in Rules[key])
                    {
                        foreach(var canHoldT in CanHoldTargetBag(target, bag))
                        {
                            yield return canHoldT;
                        }
                    }
                }
            }
            return count;
        }

        private int SolvePartTwo(string [] input)
        {
            var normalized = input.Select(str => str.Replace("bags", "bag")
                                                    .Replace(".", "")
                                                    .Split("bag contain"));
            Dictionary<string, List<(int, string)>> Rules = ParseRules(normalized);

            var target = "shiny gold";
            var count = CountBag(target);

            int CountBag(string bag)
            {
                var count = 1 + (from item in Rules[bag] select item.Item1 * CountBag(item.Item2)).Sum();
                return count;
            }
            return count - 1;
        }

        private bool CanHoldBag(string targetBag, List<(int, string)> bags)
        {
            foreach(var bag in bags)
            {
                if(targetBag == bag.Item2)
                {
                    return true;
                }
            }
            return false;
        }

        private Dictionary<string, List<(int, string)>> ParseRules(IEnumerable<string[]> normalizedInput)
        {
            Dictionary<string, List<(int, string)>> Rules = new Dictionary<string, List<(int, string)>>();
            foreach(var entry in normalizedInput)
            {
                var key = entry[0].Trim();
                var values = entry[1].Trim().Split(",");
                foreach(var value in values)
                {
                    string pattern = @"(\d+) ([a-z]+ [a-z]+) bag";
                    var matches = new Regex(pattern).Matches(value.Trim());
                    if(!Rules.ContainsKey(key))
                    {
                            Rules.Add(key, new List<(int, string)>());
                    }
                    foreach(Match match in matches)
                    {
                        Rules[key].Add((int.Parse(match.Groups[1].Value), match.Groups[2].Value));
                    }
                }
            }
            return Rules;
        }

        
    }
}