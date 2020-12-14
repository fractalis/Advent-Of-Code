using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Threading;
using Advent_Of_Code_2020.Attributes;
using Advent_Of_Code_2020.Library;

namespace Advent_Of_Code_2020.ShuttleSearch
{
    [AdventCalendarAttribute("Shuttle Search", 13, "input/day13.txt")]
    public class ShuttleSearch : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private long SolvePartOne(string[] input)
        {  
            var domain = ParseBuses(input);

            var res = domain.buses.Aggregate(
                (wait: long.MaxValue, period: long.MaxValue),
                (min, bus) => {
                    var wait = bus.period - (domain.timestamp % bus.period);
                    return wait < min.wait ? (wait, bus.period) : min;
                },
                min => min.wait * min.period
            );
            
            return res;
        }

        private long SolvePartTwo(string[] input)
        {
            var (timestamp, buses) = ParseBuses(input);
            return ChineseRemainderTheorem(buses.Select(item => (mod: (long)item.period, b: (long)(item.period - item.index))).ToArray());
        }

        private long ChineseRemainderTheorem((long mod, long b)[] nums) {
            var prod = nums.Aggregate(1L, (acc, item) => acc * item.mod);
            var sum = nums.Select( item => {
                var p = prod / item.mod;
                return item.b * ModulusInverse(p, item.mod) * p;
            }).Sum();

            return sum % prod;
        }

        private long ModulusInverse(long a, long m) {
            return (long)BigInteger.ModPow(a, m-2, m);
        }

        private (int timestamp, (int period, int index)[] buses) ParseBuses(string[] input)
        {
            var earliestTimestamp = int.Parse(input[0]);

            var buses = input[1].Split(",").Select((part, idx) => (part, idx))
                                           .Where(item => item.part != "x")
                                           .Select(x => (period: int.Parse(x.part), index: x.idx))
                                           .ToArray();
            return (earliestTimestamp, buses);
        }
    }
}