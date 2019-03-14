using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TimeRecorder.Models.ValueParsers;
using TimeRecorder.ViewModels.Interfaces;

namespace TimeRecorder.ViewModels
{
    public class TimeFieldVM : BaseViewModel, ITimeFieldVM
    {
        private static SolidColorBrush invalidColor = new SolidColorBrush(Colors.Red);
        private static SolidColorBrush validColor = new SolidColorBrush(Colors.Transparent);
        private ITimeStringParser parser;
        internal bool LimitHours { get; private set; }
        public SolidColorBrush BorderColor { get {
                return IsValid ? validColor : invalidColor;
            }
        }

        public void Set24HourLimit(bool value)
        {
            LimitHours = value;
        }

        public TimeFieldVM(ITimeStringParser timeParser, bool isLimitedTo24 = false)
        {
            parser = timeParser;
            Set24HourLimit(isLimitedTo24);
        }
        private string textField;
        public string TextField {
            get { return textField; }
            set {
                textField = value;
                OnPropertyChanged();
                if (parser.TryParse(value, out TimeSpan time, LimitHours))
                {
                    ParsedTime = time;
                    IsValid = true;
                }
                else
                {
                    IsValid = false;
                }
            }
        }
        private TimeSpan parsedTime;
        public TimeSpan ParsedTime {
            get { return parsedTime; }
            set {
                if (value != parsedTime)
                {
                    parsedTime = value;
                    OnPropertyChanged("ParsedTime");
                }
            }
        }
        private bool isValid;
        public bool IsValid {
            get { return isValid; }
            set {
                if (value != isValid)
                {
                    isValid = value;
                    OnPropertyChanged("IsValid");
                    OnPropertyChanged("BorderColor");
                }
            }
        }

    }
}
