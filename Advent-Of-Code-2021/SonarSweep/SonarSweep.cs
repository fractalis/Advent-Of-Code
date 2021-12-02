using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Advent_Of_Code_2021.Library;
using Advent_Of_Code_2021.Attributes;

using MoreLinq;

namespace Advent_Of_Code_2021.SonarSweep
{
    [AdventCalendar("Sonar Sweep", 1, "input/day01.txt")]
    public class SonarSweep : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private long SolvePartOne(string[] input)
        {
            var inputAsInt = input.Select(x => Convert.ToInt32(x));

            var count = inputAsInt
                .Zip(inputAsInt.Skip(1), (x, y) => new { current = x, next = y })
                .Where(x => x.next > x.current)
                .Count();
            
            return count;
        }

        private long SolvePartTwo(string[] input)
        {
            var inputAsInt = input.Select(x => Convert.ToInt32(x));

            var windowedSum = inputAsInt.Window(3)
                .Select(x => x[0] + x[1] + x[2]);

            var count = windowedSum
                .Zip(windowedSum.Skip(1), (x, y) => new { current = x, next = y })
                .Where(x => x.next > x.current)
                .Count();

            return count;
        }
    }
}
