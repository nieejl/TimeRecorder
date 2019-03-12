using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TimeRecorder.Models.ValueParsers;
using TimeRecorder.ViewModels;
using Xunit;

namespace TimeRecorder.Tests.ViewModels
{
    public class TimeFieldVMTest
    {
        [Fact]
        public void Test_TextField_Given_False_On_TryParse_Sets_BorderColor_Red()
        {
            var mock = new Mock<ITimeStringParser>();
            var span = new TimeSpan();
            mock.Setup(m => m.TryParse(It.IsAny<string>(), out span, false)).Returns(false);
            var timeFieldVm = new TimeFieldVM(mock.Object);

            timeFieldVm.TextField = "non parseable value";

            Assert.Equal(Colors.Red, timeFieldVm.BorderColor.Color);
        }

        [Fact]
        public void Test_TextField_Given_True_On_TryParse_Sets_BorderColor_Green()
        {
            var mock = new Mock<ITimeStringParser>();
            var span = new TimeSpan();
            mock.Setup(m => m.TryParse(It.IsAny<string>(), out span, false)).Returns(true);
            var timeFieldVm = new TimeFieldVM(mock.Object);

            timeFieldVm.TextField = "parseable value";

            Assert.Equal(Colors.Green, timeFieldVm.BorderColor.Color);
        }

        [Fact]
        public void Test_TextField_Given_False_On_TryParse_Sets_IsValid_To_False()
        {
            var mock = new Mock<ITimeStringParser>();
            var span = new TimeSpan();
            mock.Setup(m => m.TryParse(It.IsAny<string>(), out span, false)).Returns(false);
            var timeFieldVm = new TimeFieldVM(mock.Object);

            timeFieldVm.TextField = "parseable value";

            Assert.False(timeFieldVm.IsValid);
        }

        [Fact]
        public void Test_TextField_Given_True_On_TryParse_Sets_IsValid_To_True()
        {
            var mock = new Mock<ITimeStringParser>();
            var span = new TimeSpan();
            mock.Setup(m => m.TryParse(It.IsAny<string>(), out span, false)).Returns(true);
            var timeFieldVm = new TimeFieldVM(mock.Object);

            timeFieldVm.TextField = "parseable value";

            Assert.True(timeFieldVm.IsValid);
        }

        [Fact]
        public void Test_TextField_Given_True_On_TryParse_Sets_ParsedTime_To_OutValue()
        {
            var mock = new Mock<ITimeStringParser>();
            var expected = new TimeSpan(1,2,3);
            mock.Setup(m => m.TryParse(It.IsAny<string>(), out expected, false)).Returns(true);
            var timeFieldVm = new TimeFieldVM(mock.Object);

            timeFieldVm.TextField = "parseable value";

            Assert.Equal(expected, timeFieldVm.ParsedTime);
        }

        [Fact]
        public void Test_TextField_Given_True_On_TryParse_Does_Not_Set_ParsedTime_To_OutValue()
        {
            var mock = new Mock<ITimeStringParser>();
            var notExpected = new TimeSpan(1, 2, 3);
            mock.Setup(m => m.TryParse(It.IsAny<string>(), out notExpected, false)).Returns(false);
            var timeFieldVm = new TimeFieldVM(mock.Object);

            timeFieldVm.TextField = "parseable value";

            Assert.NotEqual(notExpected, timeFieldVm.ParsedTime);
        }
    }
}
