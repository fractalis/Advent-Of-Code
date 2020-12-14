using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml;
using Advent_Of_Code_2020.Attributes;
using Advent_Of_Code_2020.Library;

namespace Advent_Of_Code_2020.SeatingSystem
{
    [AdventCalendarAttribute("Seating System", 11, "input/day11.txt")]
    public class SeatingSystem : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private int SolvePartOne(string[] input)
        {
            return Solve(input, 4, (_ => true));
        }

        private int SolvePartTwo(string[] input)
        {
            return Solve(input, 5, ch => ch != '.');
        }

        int Solve(string[] input, int maxSeats, Func<char, bool> isOccupied)
        {
            var prevState = new string[] {};
            var state = input.Select(str => str.Replace("L", "#")).ToArray();
            while(!prevState.SequenceEqual(state))
            {
                prevState = state;
                state = state.Select((str, row) => {
                    return str.Select((ch, col) => {
                        return ch == '#' && OccupiedSeatsAroundPoint(state, row, col) >= maxSeats ? 'L'.ToString() :
                               ch == 'L' && OccupiedSeatsAroundPoint(state, row, col) == 0 ? '#'.ToString() :
                               ch.ToString();
                    }).Aggregate((str1, str2) => str1 + str2);
                }).ToArray();
            }

            int OccupiedSeatsAroundPoint(string[] state, int row, int col)
            {
                var directions = DirectionsToCheck(row, col, state.Length, state[0].Length);
                int occupiedSeats = 0;
                foreach(var direction in directions) {
                    if(PlaceAtOffset(state, row, col, direction.Item1, direction.Item2) == '#')
                    {
                        occupiedSeats++;
                    }
                }
                return occupiedSeats;
            }

            char PlaceAtOffset(string[] state, int row, int col, int x_off, int y_off)
            {
                var (trow, tcol) = (state.Length, state[0].Length);
                var (crow, ccol) = (row + x_off, col + y_off);
                while(true)
                {
                    if(crow >= trow || ccol >= tcol || crow < 0 || ccol < 0) return 'L';
                    var place = state[crow][ccol];
                    if( isOccupied(place))
                    {
                        return place;
                    }
                    crow += x_off;
                    ccol += y_off;
                }
            }

            return state.Sum(str => str.Count(ch => ch == '#'));
        }

        (int, int)[] DirectionsToCheck(int row, int col, int numRows, int numCols)
        {
            var Directions = new List<(int, int)>() { (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1), (-1, 0) };
            var DirectionsToReturn = new List<(int, int)>();

            foreach((int x, int y) in Directions)
            {
                if( (x+row) >= numRows || (y+col) >= numCols || (x+row) < 0 || (y+col) < 0) {
                    continue;
                } else {
                    DirectionsToReturn.Add((x, y));
                }
            }
            return DirectionsToReturn.ToArray();
        }
    }
}