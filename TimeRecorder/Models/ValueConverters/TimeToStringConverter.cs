using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using TimeRecorder.Models.ValueParsers;

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
            var parser = new TimeStringParser();
            var inputString = value.ToString();
            if (!parser.TryParse(inputString, out TimeSpan time))
                return null;
            return time; 
        }
    }
}
