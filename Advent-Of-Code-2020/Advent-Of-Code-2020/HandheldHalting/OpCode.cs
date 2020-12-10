using System;

namespace Advent_Of_Code_2020.HandheldHalting
{
    public abstract class OpCode
    {
        public OpCode(int Argument)
        {
            this.Argument = Argument;
        }
        public int Argument {get; set;}

        public virtual OpCode Patch()
        {
            return this;
        }

        public bool Equals(OpCode op)
        {
            return this.Argument == op.Argument && this.GetType() == op.GetType();
        }
    }

    public class Acc : OpCode
    {
        public Acc(int Argument) : base(Argument) {}
    }

    public class Jmp : OpCode
    {
        public Jmp(int Argument) : base(Argument) {}

        public override OpCode Patch()
        {
            return new Nop(Argument);
        }
    }

    public class Nop : OpCode
    {
        public Nop(int Argument) : base(Argument) {}

        public override OpCode Patch()
        {
            return new Jmp(Argument);
        }
    }
}