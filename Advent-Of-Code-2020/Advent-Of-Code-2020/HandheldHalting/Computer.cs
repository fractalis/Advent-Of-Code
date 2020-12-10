using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Advent_Of_Code_2020.HandheldHalting
{
    public class Computer
    {
        public int Ip { get; private set;}
        public int Accumulator {get; private set;}
        private HashSet<int> Visited {get; set; } = new HashSet<int>();

        public bool InfiniteLoop {get; private set;}
        public bool Terminated {get; private set;}

        private OpCode[] Instructions { get; set; }

        public Computer(string[] rawInstructions)
        {
            ParseInstructions(rawInstructions);
        }
        
        public void Execute()
        {
            (Ip, Accumulator) = (0, 0);
            Visited = new HashSet<int>();
            InfiniteLoop = false;
            Terminated = false;

            while(true)
            {                
                if(Ip >= Instructions.Length)
                {
                    Terminated = true;
                    break;
                }
                else if(Visited.Contains(Ip))
                {
                    InfiniteLoop = true;
                    break;
                }
                var opCode = Instructions[Ip];
                Visited.Add(Ip);

                switch(opCode)
                {
                    case Nop nop:
                        Ip++;
                        break;
                    case Acc acc:
                        Accumulator += acc.Argument;
                        Ip++;
                        break;
                    case Jmp jmp:
                        Ip += jmp.Argument;
                        break;
                }
            }
            return;
        }

        public void PatchCode()
        { 
            var originalProgram = Instructions;
            
            var programs = Enumerable.Range(0, originalProgram.Length)
                                     .Where(idx => originalProgram[idx].GetType() != typeof(Acc))
                                     .Select(idx => 
                                        originalProgram.Select((op, line) => {
                                            return line != idx ? op : op.Patch();
                                        }).ToArray());

            foreach(var program in programs)
            {
                Instructions = program;
                Execute();
                if(Terminated)
                {
                    break;
                }
            }
        }

        private void ParseInstructions(string[] rawInstructions)
        {
            Instructions = rawInstructions.Select(line => line.Split(" "))
                                          .Select(field => {
                                              return field[0] switch {
                                                  "acc" => (OpCode)new Acc(int.Parse(field[1])),
                                                  "jmp" => (OpCode)new Jmp(int.Parse(field[1])),
                                                  "nop" => (OpCode)new Nop(int.Parse(field[1])),
                                                  _ => throw new System.Exception("Invalid instruction"),
                                              };
                                          })
                                          .ToArray();
        }
    }
}