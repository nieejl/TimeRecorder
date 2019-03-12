using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRecorder.Models.ValueParsers
{
    public interface ITimeStringParser
    {
        bool TryParse(string timeAsString, out TimeSpan result, bool limitHours24=false);
        bool IsValidTime(int hours, int minutes = 0, int seconds = 0, bool limitHours24=false);
        bool IsValidTime(string timeAsString, bool limitHours24 = false);
    }
}
