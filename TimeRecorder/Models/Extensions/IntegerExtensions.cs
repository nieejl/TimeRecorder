using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRecorder.Models.Extensions
{
    public static class IntegerExtensions
    {
        public static bool isWithin(this int arg, int lower, int upper)
        {
            return lower <= arg && arg <= upper;
        }
    }
}
