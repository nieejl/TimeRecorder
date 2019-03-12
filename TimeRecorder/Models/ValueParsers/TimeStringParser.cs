using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TimeRecorder.Models.Extensions;

namespace TimeRecorder.Models.ValueParsers
{
    /// <summary>
    /// Class to parse strings into TimeSpan. Accepts format with leading 0,
    /// eg. 5:3:1 . 
    /// </summary>
    public class TimeStringParser : ITimeStringParser
    {

        static readonly char[] delimiters = { '.', ':', ',' };
        /// <summary>
        /// Parses string to its TimeSpan respresentation. Accepts normal HH:MM:SS 
        /// format, as well as H:M:S, or any mix of the two, such as H:MM:S, 5:25:2.
        /// A return value indicates whether the conversion was successful.
        /// </summary>
        public bool TryParse(string timeAsString, out TimeSpan result, bool limitHours24=false)
        {
            if (timeAsString == null)
                return Fail(out result);

            var numbers = timeAsString.Split(delimiters);
            var isEqual = new Func<int, int, bool>(
                (i1, i2) => i1 == i2);
            var isLessOrEqual = new Func<int, int, bool>(
                (i1, i2) => i1 <= i2);

            int seconds = ConvertOrSetToZero(numbers, 2, isEqual);
            int minutes = ConvertOrSetToZero(numbers, 1, isLessOrEqual);
            int hours = ConvertOrSetToZero(numbers, 0, isLessOrEqual);

            bool timeIsValid = IsValidTime(hours, minutes, seconds, limitHours24);
            if (!timeIsValid)
                return Fail(out result);
            result = new TimeSpan(hours, minutes, seconds);
            return true;
        }

        private bool Fail(out TimeSpan result)
        {
            result = default(TimeSpan);
            return false;
        }

        private int ConvertOrSetToZero(string[] numbers, int index, Func<int,int,bool> comparer)
        {
            if (comparer(index, numbers.Length -1) && 
                int.TryParse(numbers[index], out int result)) {
                return result;
            }
            return 0;
        }

        public bool IsValidTime(int hours, int minutes = 0, int seconds = 0, bool limitHours24 = false)
        {
            int hourLimit = !limitHours24 ? 24 : 999;
            return hours.isWithin(0, hourLimit) && 
                minutes.isWithin(0, 60) && 
                seconds.isWithin(0, 60);
        }

        public bool IsValidTime(string timeAsString, bool limitHours24 = false)
        {
            return TryParse(timeAsString, out TimeSpan _ , limitHours24);
        }
    }
}
