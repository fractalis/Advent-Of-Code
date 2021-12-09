using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Advent_Of_Code_2021.Library;
using Advent_Of_Code_2021.Attributes;

namespace Advent_Of_Code_2021.BinaryDiagonostic
{
    [AdventCalendar("Binary Diagnostic", 3, "input/day03.txt")]
    class BinaryDiagnostic : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        public long SolvePartOne(string[] input)
        {
            return gammaRate(input) * epsilonRate(input);
        }

        public long SolvePartTwo(string[] input)
        {
            return oxygenGeneratorRating(input) * co2scrubberRating(input);
        }

        public int oxygenGeneratorRating(string[] input) => ApplyBitFunction(input, mostCommonBit);
        public int co2scrubberRating(string[] input) => ApplyBitFunction(input, leastCommonBit);

        public int ApplyBitFunction(string[] input, Func<string[], int, char> bitFunc)
        {
            var bitLength = input[0].Length;

            for (int i = 0; input.Length > 1 && i < bitLength; i++)
            {
                var cb = bitFunc(input, i);
                input = input.Where(x => x[i] == cb).ToArray();
            }

            return Convert.ToInt32(input[0], 2);
        }

        public int gammaRate(string[] input)
        {
            var gammaRateStr = Enumerable.Range(0, input[0].Length)
                .Select(x => mostCommonBit(input, x))
                .Aggregate("", (acc, val) => acc = acc + val);
            return Convert.ToInt32(gammaRateStr, 2);
        }

        public int epsilonRate(string[] input)
        {
            var epsilonRateStr = Enumerable.Range(0, input[0].Length)
                .Select(x => leastCommonBit(input, x))
                .Aggregate("", (acc, val) => acc = acc += val);
            return Convert.ToInt32(epsilonRateStr, 2);
        }

        public char mostCommonBit(string[] input, int bitIdx)
        {
            var onesCount = input.Count(x => x[bitIdx] == '1');
            var zerosCount = input.Count(x => x[bitIdx] == '0');

            return onesCount >= zerosCount ? '1' : '0';   
        }

        public char leastCommonBit(string[] input, int bitIdx)
        {
            return mostCommonBit(input, bitIdx) == '1' ? '0' : '1';
        }
    }
}
