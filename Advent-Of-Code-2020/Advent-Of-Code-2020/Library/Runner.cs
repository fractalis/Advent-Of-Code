using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

using Advent_Of_Code_2020.Attributes;
using System.IO;

namespace Advent_Of_Code_2020.Library
{
    public class Runner
    {
        private Dictionary<(String, int), ISolution> _solutions = new Dictionary<(String, int), ISolution>();

        public Runner()
        {
            LoadSolutions();
        }

        public void ListSolutions()
        {
            foreach(var key in _solutions.Keys) {
                Console.WriteLine($"Advent Calendar Name: {key.Item1} - Advent Calendar Day: {key.Item2}");
            }
        }

        public void RunSolutionByName(string name)
        {
            if(_solutions.Keys.Select(x => x.Item1).Contains(name))
            {
                ISolution solution = (ISolution)_solutions[_solutions.Keys.Where(x => x.Item1 == name).First()];
                AdventCalendarAttribute calendarAttribute = (AdventCalendarAttribute)solution.GetType().GetCustomAttributes(typeof(AdventCalendarAttribute), true).First();
                String dataInput = calendarAttribute.AdventInput;
                var solutions = solution.Solve(File.ReadAllLines(dataInput));

                Console.WriteLine($"Advent Calendar Name: {calendarAttribute.AdventDayName} - Advent Day: {calendarAttribute.AdventDay}");
                foreach(var s in solutions)
                {
                    Console.WriteLine(s);
                }
            }
        }

        private void LoadSolutions()
        {
            var solutions =
                from a in AppDomain.CurrentDomain.GetAssemblies()
                from t in a.GetTypes()
                let attributes = t.GetCustomAttributes(typeof(AdventCalendarAttribute), true)
                where attributes != null && attributes.Length > 0
                select new { Type = Activator.CreateInstance(t) as ISolution, Attributes = attributes.Cast<AdventCalendarAttribute>() };

            foreach(var solution in solutions)
            {
                var attribute = solution.Attributes.First();
                _solutions.Add((attribute.AdventDayName, attribute.AdventDay), solution.Type);
            }
        }
    }
}