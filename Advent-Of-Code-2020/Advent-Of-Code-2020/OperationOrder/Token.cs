using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Advent_Of_Code_2020.OperationOrder
{
    public abstract class Token { }

    public abstract class Operator : Token {
        public abstract long Evalulate(long a, long b);
    }

    public class Multiply : Operator
    {
        public override long Evalulate(long a, long b)
        {
            return a * b;
        }
    }

    public class Addition : Operator
    {
        public override long Evalulate(long a, long b)
        {
            return a + b;
        }
    }

    public class Number : Token {
        public long Value {get; }

        public Number(long value)
        {
            Value = value;
        }
    }

    public class SubExpression : Token {
        public List<Token> Tokens {get; set;}

        public SubExpression()
        {
            Tokens = new List<Token>();
        }

        public SubExpression(List<Token> tokens)
        {
            Tokens = tokens;
        }

        public void AddToken(Token token)
        {
            Tokens.Add(token);
        }
    }

    public class Equation : Token {

        public List<Token> Tokens { get; set; }

        public Equation()
        {
            Tokens = new List<Token>();
        }

        public long Solve(Boolean part2)
        {
            var opStack = new Stack<Operator>();
            var valStack = new Stack<Number>();
            for(var i = 0; i < Tokens.Count; i++)
            {
                if( Tokens[i].GetType() == typeof(Equation))
                {
                    Tokens[i] = new Number(((Equation)Tokens[i]).Solve(part2));
                }
            }

            if(part2) {
                for(var i = 0; i < Tokens.Count; i++)
                {
                    if(Tokens[i] is Addition a)
                    {
                        Tokens[i-1] = new Number(a.Evalulate(((Number)Tokens[i-1]).Value, ((Number)Tokens[i+1]).Value));
                        Tokens.RemoveRange(i,2);
                        i = 0;
                    }
                }
                for(var i = 0; i < Tokens.Count; i++)
                {
                    if(Tokens[i] is Multiply m)
                    {
                        Tokens[i-i] = new Number(m.Evalulate(((Number)Tokens[i-1]).Value, ((Number)Tokens[i+1]).Value));
                        Tokens.RemoveRange(i,2);
                        i = 0;
                    }
                }
            } else
            {
                for(var i = 0; i < Tokens.Count; i++)
                {
                    if (Tokens[i] is Operator op)
                    {
                        Tokens[i - 1] = new Number(op.Evalulate(((Number)Tokens[i - 1]).Value, ((Number)Tokens[i + 1]).Value));
                        Tokens.RemoveRange(i, 2);
                        i = 0;
                    }
                }
            }

            var result = ((Number)Tokens[0]).Value;
            return result;
        }

        public void AddToken(Token token)
        {
            Tokens.Add(token);
        }

        public static Equation ParseEquation(string rawEquation)
        {
            var subEquations = new Stack<Equation>();
            Equation equation = new Equation();
            for(int i = 0; i < rawEquation.Length; i++)
            {
                var ch = rawEquation[i];
                switch(ch)
                {
                    case '(':
                        subEquations.Push(equation);
                        equation = new Equation();
                        break;
                    case '*':
                        equation.AddToken(new Multiply());
                        break;
                    case '+':
                        equation.AddToken(new Addition());
                        break;
                    case ')':
                        var origSubEquation = subEquations.Pop();
                        origSubEquation.AddToken(equation);
                        equation = origSubEquation;
                        break;
                    case ' ':
                        break;
                    default:
                        var num = Convert.ToInt64(ch);
                        equation.AddToken(new Number(num - '0'));
                        break;
                }
            }
            return equation;
        }
    }
}