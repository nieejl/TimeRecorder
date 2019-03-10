using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRecorder.Models.ValueValidators
{
    public interface ITimeStringParser
    {
        bool TryParseStringToTime(string timeAsString, out TimeSpan? result);
        bool IsValidTime(int hours, int minutes = 0, int seconds = 0);
    }
}
