using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using TimeRecorder.Models.ValueValidators;

namespace TimeRecorder.Models.ValueConverters
{
    public class TimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = (TimeSpan)value;
            if (time == null)
                return null;
            return time.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var inputString = value.ToString();
            if (!TimeStringParser.TryParseStringToTime(inputString, out TimeSpan? time))
                return null;
            return time; 
        }
    }
}
