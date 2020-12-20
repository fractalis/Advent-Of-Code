using Advent_Of_Code_2020.Attributes;
using Advent_Of_Code_2020.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace Advent_Of_Code_2020.ConwayCubes
{
    public enum ActiveStatus {
        Active,
        Inactive
    }

    record Location(int X, int Y, int Z, int W, ActiveStatus Status);

    [AdventCalendarAttribute("Conway Cubes", 17, "input/day17.txt")]
    public class ConwayCubes : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private int SolvePartOne(string[] input)
        {
            var directions = (from dx in new[] { -1, 0, 1 }
                              from dy in new[] { -1, 0, 1 }
                              from dz in new[] { -1, 0, 1 }
                              from dw in new[] { 0 }
                              where dx != 0 || dy != 0 || dz != 0
                              select (dx, dy, dz, dw)).ToArray();
            var Cubes = ParseInput(input);
            
            return Solve(Cubes, directions);
        }

        private int SolvePartTwo(string[] input)
        {
            var directions = (from dx in new[] { -1, 0, 1}
                              from dy in new[] { -1, 0, 1}
                              from dz in new[] { -1, 0, 1}
                              from dw in new[] { -1, 0, 1}
                              where dx != 0 || dy != 0 || dz != 0 || dw != 0
                              select (dx, dy, dz, dw)).ToArray();
            var Cubes = ParseInput(input);

            return Solve(Cubes, directions);
        }

        private int Solve(Dictionary<(int, int, int), Location> Cubes, (int dx, int dy, int dz, int dw)[] directions)
        {
            var activeCubes = (from cube in Cubes.Values
                               where cube.Status == ActiveStatus.Active
                               select cube).ToHashSet();
            for(int i = 0; i < 6; i++)
            {
                var newActiveCubes = new HashSet<Location>();
                var inactiveCubes = new Dictionary<Location, int>();

                foreach(var cube in activeCubes)
                {
                    var activeNeighbors = 0;
                    var potentialActiveNeighbors = directions.Select( (pos) => {
                        return new Location(cube.X + pos.dx, cube.Y + pos.dy, cube.Z + pos.dz, cube.W + pos.dw, ActiveStatus.Active);
                    });

                    foreach( var pan in potentialActiveNeighbors)
                    {
                        if(activeCubes.Contains(pan))
                        {
                            activeNeighbors++;
                        }
                        else
                        {
                            var inactiveCube = pan with { Status = ActiveStatus.Inactive };
                            inactiveCubes[inactiveCube] = inactiveCubes.GetValueOrDefault(inactiveCube)+1;
                        }
                    }

                    if( activeNeighbors == 2 || activeNeighbors == 3)
                    {
                            newActiveCubes.Add(cube);
                    }
                }

                foreach(var (inactive, neighbors) in inactiveCubes)
                {
                    if(neighbors == 3)
                    {
                        var activeCube = inactive with { Status = ActiveStatus.Active };
                        newActiveCubes.Add(activeCube);
                    }
                }
                activeCubes = newActiveCubes;
            }
            return activeCubes.Count;
        }
        
        Dictionary<(int, int, int), Location> ParseInput(string[] input)
        {
            var (width, height) = (input[0].Length, input.Length);
            var Cubes = new Dictionary<(int, int, int), Location>();
            var locations = from x in Enumerable.Range(0, width)
                            from y in Enumerable.Range(0, height)
                            let status = input[x][y]=='#' ? ActiveStatus.Active : ActiveStatus.Inactive
                            select new Location(x, y, 0, 0, status);
            foreach(var location in locations)
            {
                Cubes.Add((location.X, location.Y, location.Z), location);
            }
            
            return Cubes;
        }
    }
}