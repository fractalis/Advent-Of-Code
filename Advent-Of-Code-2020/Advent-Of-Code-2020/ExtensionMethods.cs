using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Advent_Of_Code_2020
{
    public static class ExtensionMethods
    {
        public static void SafeSet<TKey, TValue>(this Dictionary<TKey,TValue> dict, TKey key, TValue value)
        {
            if(dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
            }
        }
    }
}