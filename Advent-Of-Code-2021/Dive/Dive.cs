using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Advent_Of_Code_2021.Library;
using Advent_Of_Code_2021.Attributes;

namespace Advent_Of_Code_2021.Dive
{
    public struct Position
    {
        public int Depth;
        public int HorizontalPos;
        public int Aim;

        public Position(int depth, int horizontalPos, int aim)
        {
            Depth = depth;
            HorizontalPos = horizontalPos;
            Aim = aim;
        }

    }
    [AdventCalendar("Dive", 2, "input/day02.txt")]
    class Dive : ISolution
    {
        public Dictionary<String, int> directionToPosition = new Dictionary<string, int>
        {
            {"forward", 1 },
            {"up", -1 },
            {"down", 1 }
        };

        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private long SolvePartOne(string[] input)
        {
            var parsedInput = input
                .Select(x => new { direction = x.Split(" ")[0], distance = Int32.Parse(x.Split(" ")[1]) });

            var finalPos = parsedInput.Aggregate(new Position(0, 0, 0),
                (curPos, nextElem) =>
                {
                    switch (nextElem.direction)
                    {
                        case "forward":
                            curPos.HorizontalPos += directionToPosition[nextElem.direction] * nextElem.distance;
                            break;
                        case "up":
                            curPos.Depth += directionToPosition[nextElem.direction] * nextElem.distance;
                            break;
                        case "down":
                            curPos.Depth += directionToPosition[nextElem.direction] * nextElem.distance;
                            break;
                    }
                    return curPos;
                });
            return finalPos.Depth * finalPos.HorizontalPos;
        }

        private long SolvePartTwo(string[] input)
        {
            var parsedInput = input
                .Select(x => new { direction = x.Split(" ")[0], distance = Int32.Parse(x.Split(" ")[1]) });

            var finalPos = parsedInput.Aggregate(new Position(0, 0, 0),
                (curPos, nextElem) =>
                {
                    switch (nextElem.direction)
                    {
                        case "forward":
                            curPos.HorizontalPos += directionToPosition[nextElem.direction] * nextElem.distance;
                            curPos.Depth += curPos.Aim * nextElem.distance;
                            break;
                        case "up":
                            curPos.Aim += directionToPosition[nextElem.direction] * nextElem.distance;
                            break;
                        case "down":
                            curPos.Aim += directionToPosition[nextElem.direction] * nextElem.distance;
                            break;
                    }
                    return curPos;
                });
            return finalPos.Depth * finalPos.HorizontalPos;
        }
    }
}
