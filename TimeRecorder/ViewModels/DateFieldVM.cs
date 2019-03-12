using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TimeRecorder.ViewModels.Interfaces;

namespace TimeRecorder.ViewModels
{
    public class DateFieldVM : BaseViewModel, IDateFieldVM
    {
        private static SolidColorBrush invalidColor = new SolidColorBrush(Colors.Red);
        private static SolidColorBrush validColor = new SolidColorBrush(Colors.Green);

        public SolidColorBrush BorderColor {
            get {
                return IsValid ? validColor : invalidColor;
            }
        }

        public DateFieldVM()
        {
        }

        private string textField;
        public string TextField {
            get { return textField; }
            set {
                textField = value;
                OnPropertyChanged("TextField");
                //textField = value;
                //OnPropertyChanged("TextField");
                //if (DateTime.TryParse(TextField, out DateTime date))
                //{
                //    ParsedDate = date;
                //    IsValid = true;
                //}
                //else
                //{
                //    IsValid = false;
                //}
            }
        }

        private DateTime parsedDate;
        public DateTime ParsedDate {
            get { return parsedDate; }
            set {
                if (value != parsedDate)
                {
                    parsedDate = value;
                    OnPropertyChanged("ParsedDate");
                    TextField = parsedDate.ToShortDateString();
                    IsValid = DateTime.TryParse(TextField, out DateTime _);
                }
            }
        }

        private bool isValid;
        public bool IsValid {
            get { return isValid ; }
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
