using System.Collections;
using System.Collections.Generic;

namespace Advent_Of_Code_2020.Library
{
    public interface ISolution
    {
        IEnumerable<object> Solve(string[] input);
    }
}