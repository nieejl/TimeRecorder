using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TimeRecorder.ViewModels;
using Xunit;

namespace TimeRecorder.Tests.ViewModels
{
    public class DateFieldVMTest
    {
        [Fact]
        public void Test_BorderColor_Returns_Green_If_ParsedDateField_Is_Valid()
        {
            var dateFieldVM = new DateFieldVM();

            dateFieldVM.ParsedDate = DateTime.Parse("12-03-2019");

            Assert.Equal(Colors.Green, dateFieldVM.BorderColor.Color);
        }

        [Fact]
        public void Test_BorderColor_Returns_Red_If_ParsedDateField_Is_InValid()
        {
            var dateFieldVM = new DateFieldVM();

            Assert.Equal(Colors.Red, dateFieldVM.BorderColor.Color);
        }

        [Fact]
        public void Test_IsValid_Returns_False_If_ParsedDateField_Is_InValid()
        {
            var dateFieldVM = new DateFieldVM();

            Assert.False(dateFieldVM.IsValid);
        }

        [Fact]
        public void Test_IsValid_Returns_True_If_ParsedDateField_Is_Valid()
        {
            var dateFieldVM = new DateFieldVM();

            dateFieldVM.ParsedDate = DateTime.Parse("12-03-2019");

            Assert.True(dateFieldVM.IsValid);
        }

        [Fact]
        public void Test_TextField_Returns_Matching_DateString()
        {
            var dateFieldVM = new DateFieldVM();

            dateFieldVM.ParsedDate = DateTime.Parse("12-03-2019");

            Assert.Equal("12-03-2019", dateFieldVM.TextField);
        }

    }
}
