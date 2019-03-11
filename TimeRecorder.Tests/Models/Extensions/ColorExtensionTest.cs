using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Xunit;
using TimeRecorder.Models.Extensions;

namespace TimeRecorder.Tests.Models.Extensions
{
    public class ColorExtensionTest
    {
        [Fact]
        public void Test_ToArgb_Returns_Matching_Integer()
        {
            var color = Colors.Coral;
            UInt32 colorCode = Convert.ToUInt32("FFFF7F50", 16);

            var result = color.ToUInt();

            Assert.Equal(colorCode, result);
        }

        [Fact]
        public void Test_ToInt_Returns_Matching_Color()
        {
            UInt32 colorAsInt = Convert.ToUInt32("FFDEB887", 16);
            var color = Colors.BurlyWood;

            var result = colorAsInt.ToColor();


            Assert.Equal(color, result);
        }
    }
}
