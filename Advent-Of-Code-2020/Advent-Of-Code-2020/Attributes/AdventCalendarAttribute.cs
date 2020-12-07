using System;

namespace Advent_Of_Code_2020.Attributes
{

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class AdventCalendarAttribute : Attribute
    {
        private string _adventDayName;
        private int _adventDay;
        private string _adventInput;

        public AdventCalendarAttribute(string adventDayName, int adventDay, string adventInput)
        {
            this._adventDayName = adventDayName;
            this._adventDay = adventDay;
            this._adventInput = adventInput;
        }

        public virtual string AdventDayName
        {
            get { return _adventDayName; }
        }

        public virtual int AdventDay
        {
            get { return _adventDay; }
        }

        public virtual string AdventInput
        {
            get { return _adventInput; }
        }
    }
}