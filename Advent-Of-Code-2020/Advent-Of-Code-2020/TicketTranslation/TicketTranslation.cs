using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Markup;
using Advent_Of_Code_2020.Attributes;
using Advent_Of_Code_2020.Library;
using Advent_Of_Code_2020.RainRisk;

namespace Advent_Of_Code_2020.TicketTranslation
{
    record Field(string FieldName, Func<int, bool> Validate);
    record DataSet(Field[] fields, int[][] ticketValues);

    [AdventCalendarAttribute("Ticket Translation", 16, "input/day16.txt")]
    public class TicketTranslation : ISolution
    {
        public IEnumerable<object> Solve(string[] input)
        {
            yield return SolvePartOne(input);
            yield return SolvePartTwo(input);
        }

        private int SolvePartOne(string[] input)
        {
            var dataSet = ParseInput(input);

            var errorRate = (from ticketGroup in dataSet.ticketValues
                            from ticket in ticketGroup
                            where dataSet.fields.All( field => !field.Validate(ticket))
                            select ticket).Sum();
            return errorRate;
        }

        private long SolvePartTwo(string[] input)
        {
            var dataSet = ParseInput(input);

            var filteredTickets = (from ticketGroup in dataSet.ticketValues
                                   where dataSet.fields.Where(field => ticketGroup.All(field.Validate)).ToArray().Any()
                                   select ticketGroup).ToArray();
            var fields = (from field in dataSet.fields
                          from ticketGroup in dataSet.ticketValues
                          from ticket in ticketGroup
                          select field);
            

            var fieldMapping = new Dictionary<string, int>();

            var byCol = filteredTickets.SelectMany((row, ri) => row
                                        .Select((col, ci) => new { cell = col, ci, ri}))
                                       .GroupBy(z => z.ci)
                                       .Select( g => g.Select( z => z.cell));
            
            var fieldsToCheck = dataSet.fields.ToHashSet();
            var columnsToCheck = Enumerable.Range(0, fieldsToCheck.Count);
            var res = 1L;
            while(fieldsToCheck.Count > 0)
                foreach(var col in byCol)
                {
                    var checkedFields = fieldsToCheck.Where(field => col.All(field.Validate)).ToArray();
                    if(checkedFields.Length == 1) {
                        var field = checkedFields.Single();
                        fieldsToCheck.Remove(field);
                        if(field.FieldName.StartsWith("departure"))
                        {
                            res *= col.First();
                        }
                    }
                }
            
            return res;
        }
            

        DataSet ParseInput(string[] input)
        {
            var aggInput = input.Aggregate((str1, str2) => str1 + "\n" + str2);

            var sections = (from section in aggInput.Split("\n\n")
                            select section.Split("\n")).ToArray();

            var fields = (from row in sections.First()
                          let ranges = (from m in Regex.Matches(row, "\\d+")
                                        select int.Parse(m.Value)).ToArray()
                          select new Field(row.Split(":")[0], x => x >= ranges[0] && x <= ranges[1] || x >= ranges[2] && x <= ranges[3]))
                          .ToArray();
            
            var ticketVals = (from tickets in sections.Skip(1)
                           let ticketValues = tickets.Skip(1)
                           from row in ticketValues
                           let parsedNumbers = (from m in Regex.Matches(row, "\\d+")
                                                select int.Parse(m.Value)).ToArray()
                           select parsedNumbers).ToArray();
            return new DataSet(fields, ticketVals);
        }
    }
}