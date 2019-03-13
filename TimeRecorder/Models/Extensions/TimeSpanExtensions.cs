using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRecorder.Models.Extensions
{
    public static class TimeSpanExtensions
    {
        private static string toStringIgnoreZero(int number)
        {
            return number == 0 ? "" : number.ToString();
        }
        public static string ToSimpleString(this TimeSpan time)
        {
            string asString =
                toStringIgnoreZero(time.Days) +
                toStringIgnoreZero(time.Hours) +
                toStringIgnoreZero(time.Minutes) +
                toStringIgnoreZero(time.Seconds);
            return asString;
        }

        public static string ToHHMMSS(this TimeSpan time)
        {
            var hours = ((time.Days * 24) + time.Hours).ToString();
            var minutes = time.Minutes < 10 ? "0" + time.Minutes : time.Minutes.ToString();
            var seconds = time.Seconds < 10 ? "0" + time.Seconds : time.Seconds.ToString();
            return hours + minutes + seconds;
        }
    }
}
