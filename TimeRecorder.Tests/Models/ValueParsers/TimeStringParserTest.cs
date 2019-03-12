using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.ValueParsers;
using Xunit;

namespace TimeRecorder.Tests.ValueParsers
{
    public class TimeStringParserTest
    {
        [Theory]
        [InlineData("01")]
        [InlineData("1")]
        public void Test_TryParseStringToTime_Given_Hours_Returns_True(string inputTime)
        {
            var parser = new TimeStringParser();
            var result = parser.TryParse(inputTime, out TimeSpan outputTime);

            Assert.True(result);
        }

        [Theory]
        [InlineData("01:02")]
        [InlineData("1:2")]
        [InlineData("01:2")]
        [InlineData("1:02")]
        public void Test_TryParseStringToTime_Given_Hours_And_Minutes_Returns_True(string inputTime)
        {
            var parser = new TimeStringParser();

            var result = parser.TryParse(inputTime, out TimeSpan outputTime);

            Assert.True(result);
        }

        [Theory]
        [InlineData("01:02:03")]
        [InlineData("01:02:3")]
        [InlineData("01:2:3")]
        [InlineData("01:2:03")]
        [InlineData("1:02:03")]
        [InlineData("1:02:3")]
        [InlineData("1:2:03")]
        [InlineData("1:2:3")]
        public void Test_TryParseStringToTime_Given_HHMMSS_Returns_True(string inputTime)
        {
            string formattedInput = "01:02:03";
            var expected = TimeSpan.Parse(formattedInput);
            var parser = new TimeStringParser();
            TimeSpan result;

            parser.TryParse(inputTime, out result);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("01")]
        [InlineData("1")]
        public void Test_TryParseStringToTime_Given_Hours_Returns_Matching_TimeSpan(string inputTime)
        {
            string formattedInput = "01:00:00";
            var expected = TimeSpan.Parse(formattedInput);
            var parser = new TimeStringParser();
            TimeSpan result;

            parser.TryParse(inputTime, out result);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("01:02")]
        [InlineData("1:2")]
        [InlineData("01:2")]
        [InlineData("1:02")]
        public void Test_TryParseStringToTime_Given_Hours_And_Minutes_Returns_Matching_TimeSpan(string inputTime)
        {
            string formattedInput = "01:02:00";
            var expected = TimeSpan.Parse(formattedInput);
            var parser = new TimeStringParser();
            TimeSpan result;


            parser.TryParse(inputTime, out result);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("01:02:03")]
        [InlineData("01:02:3")]
        [InlineData("01:2:3")]
        [InlineData("01:2:03")]
        [InlineData("1:02:03")]
        [InlineData("1:02:3")]
        [InlineData("1:2:03")]
        [InlineData("1:2:3")]
        public void Test_TryParseStringToTime_Given_HHMMSS_Returns_Matching_TimeSpan(string inputTime)
        {
            string formattedInput = "01:02:03";
            var expected = TimeSpan.Parse(formattedInput);
            var parser = new TimeStringParser();
            TimeSpan result;

            parser.TryParse(inputTime, out result);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_TryParseStringToTime_Given_Null_Returns_False()
        {
            var parser = new TimeStringParser();
            var result = parser.TryParse(null, out TimeSpan parsedTime);

            Assert.False(result);
        }
    }
}
