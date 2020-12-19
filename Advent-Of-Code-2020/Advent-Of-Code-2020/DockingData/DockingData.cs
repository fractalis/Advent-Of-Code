using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using Advent_Of_Code_2020.Attributes;
using Advent_Of_Code_2020.Library;

namespace Advent_Of_Code_2020.DockingData
{
    public enum DockDataType {
        Bitmask,
        MemoryReference,
        Invalid
    }

    [StructLayout(LayoutKind.Explicit)]
    struct DockData
    {
        [FieldOffset(0)] public long AndBitmask;
        [FieldOffset(8)] public long OrBitmask;
        [FieldOffset(0)] public int MemoryAddress;
        [FieldOffset(4)] public int MemoryValue;
        [FieldOffset(16)] public DockDataType DataType;

        public DockData(long andBitmask, long orBitmask)
        {
            MemoryAddress = 0;
            MemoryValue = 0;
            AndBitmask = andBitmask;
            OrBitmask = orBitmask;
            DataType = DockDataType.Bitmask;
        }

        public DockData(int memoryAddress, int memoryValue)
        {
            AndBitmask = 0;
            OrBitmask = 0;
            MemoryAddress = memoryAddress;
            MemoryValue = memoryValue;
            DataType = DockDataType.MemoryReference;
        }
    }

    [AdventCalendarAttribute("Docking Data", 14, "input/day14.txt")]
    public class DockingData : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private long SolvePartOne(string[] input)
        {
            var memory = new Dictionary<long, long>();
            var dockData = ParseInput(input);
            var mask = new DockData();
            foreach(var dd in dockData) {
                switch(dd.DataType) {
                    case DockDataType.Bitmask:
                        mask = dd;
                        break;
                    case DockDataType.MemoryReference:
                        memory[dd.MemoryAddress] = dd.MemoryValue & mask.AndBitmask | mask.OrBitmask;
                        break;
                    default:
                        break;
                }
            }
            return memory.Values.Sum();
        }

        private long SolvePartTwo(string[] input)
        {
            var memory = new Dictionary<long, long>();
            var bitMask = "";
            foreach(var line in input)
            {
                if(line.StartsWith("mask"))
                {
                    bitMask = line.Split("=")[1].Trim();
                } else {
                    var memRefs = Regex.Matches(line, "\\d+").Select(m => long.Parse(m.Value)).ToArray();
                    (long memAddr, long memValue) = (memRefs[0], memRefs[1]);
                    var addresses = GetAddresses(bitMask, memAddr, 35);
                    foreach(var address in addresses) {
                        memory[address] = memValue;
                    }
                }
            }
            return memory.Values.Sum();
        }

        IEnumerable<long> GetAddresses(string mask, long memAddr, int bit)
        {
            if(bit == -1) { yield return 0L; }
            else {
                foreach(var address in GetAddresses(mask, memAddr, bit-1)) {
                    if(mask[bit] == 'X')
                    {
                        yield return (address << 1) + 1;
                        yield return (address << 1);
                        
                    } else if(mask[bit] == '1')
                    {
                        yield return (address << 1) + 1;
                    } else {
                        yield return (address << 1) + ((memAddr >> 35-bit) & 1);
                    }
                }
            }
        }

        private DockData[] ParseInput(string[] input)
        {
            var regex = new Regex(@"mask = ([10X]{36})|mem\[([0-9]+)\] = ([0-9]+)");
            var dockData = input.Select(x => {
                var groups = regex.Match(x).Groups;                
                if(groups[1].Value != "")
                {
                    var andMask = Convert.ToInt64(groups[1].Value.Replace("X", "1"), 2);
                    var orMask = Convert.ToInt64(groups[1].Value.Replace("X", "0"), 2);
                    return new DockData(andMask, orMask);
                }
                else if(groups[2].Value != "" && groups[3].Value != "")
                {
                    var memAddress = Convert.ToInt32(groups[2].Value);
                    var memVal = Convert.ToInt32(groups[3].Value);
                    return new DockData(memAddress, memVal);
                }
                else
                {
                    return new DockData();
                }
            });

            return dockData.ToArray();
        }

    }
}