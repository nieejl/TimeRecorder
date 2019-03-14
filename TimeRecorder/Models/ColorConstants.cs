using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TimeRecorder.Models.Extensions;

namespace TimeRecorder.Models
{
    public class ColorConstants
    {
        public static List<Color> GetProjectColors()
        {
            var colorCodes = new List<string>{
                "#FF0A014F",
                "#FFE4C2C6",
                "#FF52DEE5",
                "#FF7C7C7C",
                "#FF383D3B",
                "#FFF6AE2D",
                "#FFF26419",
                "#FF85FFC7",
                "#FF937666",
                "#FFEF3E36",
                "#FF5F5AA2",
                "#FF241023",
                "#FF6B0504",
                "#FFFFFFFF",
                "#FF000000",
                "#FF5F7367",
                "#FF563F1B",
                "#FF2191FB",
                "#FFF76F8E",
                "#FFC589E8",
            };
            var colors = new List<Color>();
            colorCodes.ForEach(cc => colors.Add((Color)ColorConverter.ConvertFromString(cc)));
            return colors;
        }
    }
}
