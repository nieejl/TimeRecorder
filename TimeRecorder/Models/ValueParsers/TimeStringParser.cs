﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TimeRecorder.Models.ValueValidators
{
    public static class TimeStringParser
    {
        //private static Regex TimeStringRegex = new Regex(@"(?<hours>\d{1,2})([:.]?(?<minutes>\d{1,2}))?([:.]?(?<seconds>\d{1,2}))?[:.]?");
        static char[] delimiters = { '.', ':', ',' };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool isWithin(this int arg, int lower, int upper)
        {
            return lower <= arg && arg <= upper;
        }
        public static bool IsValidTime(int hours, int minutes = 0, int seconds = 0)
        {
            return hours.isWithin(0, 999) && 
                minutes.isWithin(0, 60) && 
                seconds.isWithin(0, 60);
        }

        private static void ConvertOrSetToZero(string[] numbers, int index, Func<int,int,bool> comparer, out int value)
        {
            if (comparer(index, numbers.Length -1) && 
                int.TryParse(numbers[index], out value)) {
                return;
            }
            value = 0;
        }
        /// <summary>
        /// Parses string to its TimeSpan respresentation. Accepts normal HH:MM:SS 
        /// format, as well as H:M:S, or any mix of the two, such as H:MM:SS.
        /// A return value indicates whether the conversion was successful.
        /// </summary>
        public static bool TryParseStringToTime(string timeAsString, out TimeSpan? result)
        {
            var numbers = timeAsString.Split(delimiters);
            var isEqual = new Func<int, int, bool>(
                (i1, i2) => i1 == i2);
            var isLessOrEqual = new Func<int, int, bool>(
                (i1, i2) => i1 <= i2);

            ConvertOrSetToZero(numbers, 2, isEqual, out int seconds);
            ConvertOrSetToZero(numbers, 1, isLessOrEqual, out int minutes);
            ConvertOrSetToZero(numbers, 0, isLessOrEqual, out int hours);

            result = IsValidTime(hours, minutes, seconds) ?
                new TimeSpan?(new TimeSpan(hours, minutes, seconds)) :
                null;
            return result != null;
        }
    }
}
