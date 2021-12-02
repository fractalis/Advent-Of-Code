using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2021.Library
{
    public interface ISolution
    {
        IEnumerable<object> Solve(string[] input);
    }
}
