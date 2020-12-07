using System;
using System.Collections.Generic;
using System.Linq;
using Advent_Of_Code_2020.Library;
using Advent_Of_Code_2020.Attributes;
using System.Linq.Expressions;
using System.Data.Common;
using System.Xml;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;

namespace Advent_Of_Code_2020.TobogganTrajectory
{

    [AdventCalendarAttribute("Toboggan Trajectory", 3, "input/day03.txt")]
    public class TobogganTrajectory : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private long SolvePartOne(string [] input)
        {
            return Solver(new (int, int)[] { (1, 3) }, input);
        }

        private long SolvePartTwo(string [] input)
        {
            return Solver(new (int, int)[] { (1,1), (1, 3), (1, 5), (1, 7), (2, 1) }, input);
        }

        private long Solver((int, int)[] slopes, string[] input)
        {
            long muliple = 1L;

            (int irows, int icols) = (input.Length, input[0].Length);

            foreach( var (drow, dcol) in slopes)
            {
                (int crow, int ccol) = (drow, dcol);
                int trees = 0;
                while (crow < irows)
                {
                    if(input[crow][ccol] == '#')
                    {
                        trees += 1;
                    }
                    (crow, ccol) = (crow + drow, ccol + dcol);
                    if(ccol >= icols) { ccol = ccol - icols; }
                }
                muliple *= trees;
            }

            return muliple;
        }
    }
}