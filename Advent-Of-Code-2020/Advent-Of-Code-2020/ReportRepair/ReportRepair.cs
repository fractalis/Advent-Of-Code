using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using Advent_Of_Code_2020.Attributes;
using Advent_Of_Code_2020.Library;

namespace Advent_Of_Code_2020.ReportRepair
{
    [AdventCalendar("Report Repair", 1, "input/day01.txt")]
    public class ReportRepair : ISolution
    {
        public IEnumerable<object> Solve(string[] input) 
        {
            HashSet<int> expenses = input.Select(x => Convert.ToInt32(x)).ToHashSet();
            yield return SolvePartOne(expenses);
            yield return SolvePartTwo(expenses);
        }

        private long SolvePartOne(HashSet<int> expenses)
        {
            return (
                from x in expenses
                let y = 2020 - x
                where expenses.Contains(y)
                select x * y
            ).First();
        }

        private long SolvePartTwo(HashSet<int> expenses)
        {
            return (
                from x in expenses
                from y in expenses
                let z = 2020 - x - y
                where expenses.Contains(z)
                select x * y * z
            ).First();
        }

        private (int?, int?) TwoSum(int targetSum, List<int> inputArr)
        {
            inputArr.Sort();
            int lhs = 0, rhs = inputArr.Count - 1;

            while(lhs < rhs)
            {
                if(inputArr[lhs] + inputArr[rhs] == targetSum)
                {
                    return (inputArr[lhs], inputArr[rhs]);
                }
                else if(inputArr[lhs] + inputArr[rhs] < targetSum)
                {
                    lhs++;
                }
                else
                {
                    rhs--;
                }
            }
            return (null, null);
        }

        public (int?, int?, int?) ThreeSum(int targetSum, List<int> inputArr)
        {
            inputArr.Sort();

            for(int i = 0; i < inputArr.Count - 2; i++ )
            {
                int firstTriplet = inputArr[i];

                int lhs = i+1;
                int rhs = inputArr.Count - 1;

                while (lhs < rhs)
                {
                    int secondTriplet = inputArr[lhs];
                    int thirdTriplet = inputArr[rhs];

                    int tripletSum = firstTriplet + secondTriplet + thirdTriplet;

                    if(tripletSum == targetSum)
                    {
                        return (firstTriplet, secondTriplet, thirdTriplet);
                    }
                    else if(tripletSum < targetSum)
                    {
                        lhs++;
                    }
                    else
                    {
                        rhs--;
                    }
                }
            }
            return (null, null, null);
        }
    }
}