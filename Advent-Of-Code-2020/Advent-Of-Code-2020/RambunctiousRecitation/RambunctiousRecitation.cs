using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Advent_Of_Code_2020.Attributes;
using Advent_Of_Code_2020.Library;

namespace Advent_Of_Code_2020.RambunctiousRecitation
{
    [AdventCalendarAttribute("Rambunctious Recitation", 15, "input/day15.txt")]
    public class RambunctiousRecitation : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private int SolvePartOne(string[] input)
        {
            return Solve(input, 2020);
        }

        private int SolvePartTwo(string[] input)
        {
            return Solve(input, 30000000);
        }

        private int Solve(string[] input, int target)
        {
            var lastSeen = new Dictionary<int, int>();
            var startingNumsCol = input.Aggregate((str1, str2) => str1 + "\n" + str2).Split(",").Select(str => Convert.ToInt32(str));
            var startingNums = startingNumsCol.ToArray();
            

            for( int i = 0; i < startingNums.Length - 1; i++ )
            {
                lastSeen.SafeSet(startingNums[i], i);
            }

            var baseNum = startingNums.Last();

            for(int turn = startingNums.Length; turn < target; turn++)
            {
                if(lastSeen.ContainsKey(baseNum)) 
                {
                    var age = turn - lastSeen[baseNum] - 1;
                    lastSeen.SafeSet(baseNum, turn - 1);
                    baseNum = age;
                } 
                else
                {
                    lastSeen.SafeSet(baseNum, turn - 1);
                    baseNum = 0;
                }
            }            
            return baseNum;
        }
    }
}