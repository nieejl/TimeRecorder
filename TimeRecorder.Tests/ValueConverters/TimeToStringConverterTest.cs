using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.ValueConverters;
using Xunit;

namespace TimeRecorder.Tests.ValueConverters
{
    public class TimeToStringConverterTest
    {
        [Fact(DisplayName ="Convert given Timespan(1,30,35) returns '1:30:35'")]
        public void Test_Convert_Given_01_30_35_Returns_Input_As_String()
        {
            var converter = new TimeToStringConverter();
            var time = new TimeSpan(1, 30, 35);

            var result = converter.Convert(time, typeof(string), null, CultureInfo.CurrentCulture);

            Assert.Equal("1:30:35", result);
        }

        [Fact(DisplayName="ConvertBack given '1:30:35' returns TimeSpan(1,30,35")]
        public void Test_ConvertBack_Given_01_30_35_Returns_Input_As_Datetime()
        {
            var converter = new TimeToStringConverter();
            var inputTime = "1, 30, 35";
            var expectedTime = new TimeSpan(01, 30, 35);

            var result = converter.ConvertBack(inputTime, typeof(TimeSpan), null, CultureInfo.CurrentCulture);

            Assert.Equal(expectedTime, result);
        }

        [Fact(DisplayName ="ConvertBack given 25:12:34 returns null")]
        public void Test_Convert_Given_Invalid_Time_Returns_Null()
        {
            var converter = new TimeToStringConverter();
            var inputTime = "25, 30, 35";

            var result = converter.ConvertBack(inputTime, typeof(TimeSpan), null, CultureInfo.CurrentCulture);

            Assert.Null(result);

        }

    }
}
