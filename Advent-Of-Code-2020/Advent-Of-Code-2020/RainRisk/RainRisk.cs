using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Formats.Asn1;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Advent_Of_Code_2020.Attributes;
using Advent_Of_Code_2020.Library;

namespace Advent_Of_Code_2020.RainRisk
{
    record Position(int x, int y, int angle);
    record Coordinate(Position pos, Position wayPoint);

    [AdventCalendarAttribute("Rain Risk", 12, "input/day12.txt")]
    public class RainRisk : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private int SolvePartOne(string[] input)
        {
            var parsed = input.Select(str => (str[0], int.Parse(str.Substring(1))));

            var pos = new Position(0, 0, 90);

            var res = parsed.Aggregate(
                new Position(0, 0, 90),
                (pos, item) => item switch {
                    ('E', var mag) => pos with { x = pos.x + mag, y = pos.y, angle = pos.angle },
                    ('W', var mag) => pos with { x = pos.x - mag, y = pos.y, angle = pos.angle },
                    ('N', var mag) => pos with { x = pos.x, y = pos.y + mag, angle = pos.angle },
                    ('S', var mag) => pos with { x = pos.x, y = pos.y - mag, angle = pos.angle },
                    ('L', var mag) => pos with { x = pos.x, y = pos.y, angle = pos.angle - mag },
                    ('R', var mag) => pos with { x = pos.x, y = pos.y, angle = pos.angle + mag },
                    ('F', var mag) => pos with { x = pos.x+(int)Math.Cos(pos.angle * (Math.PI/180)) * mag, y = pos.y+(int)Math.Sin(pos.angle * (Math.PI/180)) * mag, angle = pos.angle },
                    _ => throw new Exception("Invalid expression")
            });

            return Math.Abs(res.x) + Math.Abs(res.y);
        }

        private Coordinate Rotate(Coordinate coordinates, int angle, bool clockwise)
        {
            return (angle, clockwise) switch
            {
                (90, true) => new Coordinate(new Position(coordinates.pos.x, coordinates.pos.y, coordinates.pos.angle),
                                             new Position(coordinates.wayPoint.y, 0 - coordinates.wayPoint.x, coordinates.wayPoint.angle)),
                (180, _) => new Coordinate(new Position(coordinates.pos.x, coordinates.pos.y, coordinates.pos.angle),
                                              new Position(0 - coordinates.wayPoint.x, 0 - coordinates.wayPoint.y, coordinates.wayPoint.angle)),
                (270, true) => new Coordinate(new Position(coordinates.pos.x, coordinates.pos.y, coordinates.pos.angle),
                                              new Position(0 - coordinates.wayPoint.y, coordinates.wayPoint.x, coordinates.wayPoint.angle)),
                (90, false) => new Coordinate(new Position(coordinates.pos.x, coordinates.pos.y, coordinates.pos.angle),
                                              new Position(0 - coordinates.wayPoint.y, coordinates.wayPoint.x, coordinates.wayPoint.angle)),
                (270, false) => new Coordinate(new Position(coordinates.pos.x, coordinates.pos.y, coordinates.pos.angle),
                                               new Position(coordinates.wayPoint.y, 0 - coordinates.wayPoint.x, coordinates.wayPoint.angle)),
                _ => throw new Exception("Invalid angle")
            };
        }

        private int SolvePartTwo(string[] input)
        {
            var parsed = input.Select(str => (str[0], int.Parse(str.Substring(1))));

            var res = parsed.Aggregate(
                new Coordinate(new Position(0, 0, 90), new Position(10, 1, 0)),
                ( coord, item) => item switch {
                    ('E', var mag) => coord with { pos = new Position(coord.pos.x, coord.pos.y, coord.pos.angle), 
                                                   wayPoint = new Position(coord.wayPoint.x + mag, coord.wayPoint.y, coord.wayPoint.angle) 
                                                 },
                    ('W', var mag) => coord with { pos = new Position(coord.pos.x, coord.pos.y, coord.pos.angle), 
                                                   wayPoint = new Position(coord.wayPoint.x - mag, coord.wayPoint.y, coord.wayPoint.angle) 
                                                 },
                    ('N', var mag) => coord with { pos = new Position(coord.pos.x, coord.pos.y, coord.pos.angle), 
                                                   wayPoint = new Position(coord.wayPoint.x, coord.wayPoint.y + mag, coord.wayPoint.angle)
                                                 },
                    ('S', var mag) => coord with { pos = new Position(coord.pos.x, coord.pos.y, coord.pos.angle),
                                                   wayPoint = new Position(coord.wayPoint.x, coord.wayPoint.y - mag, coord.wayPoint.angle)
                                                 },
                    ('L', var mag) => Rotate(coord, mag, false),
                    ('R', var mag) => Rotate(coord, mag, true),
                    ('F', var mag) => coord with { pos = new Position(coord.pos.x + coord.wayPoint.x * mag, coord.pos.y + coord.wayPoint.y * mag, coord.pos.angle) , 
                                                   wayPoint = new Position(coord.wayPoint.x, coord.wayPoint.y, coord.wayPoint.angle)
                                                 },
                    _ => throw new Exception("Invalid expression")
                });
            return Math.Abs(res.pos.x) + Math.Abs(res.pos.y);
        }
    }
}