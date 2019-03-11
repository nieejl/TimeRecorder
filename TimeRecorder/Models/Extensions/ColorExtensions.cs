using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TimeRecorder.Models.Extensions
{
    public static class ColorExtensions
    {
        public static int ToInt32(this Color color) {
            return BitConverter.ToInt32(
                new byte[] { color.A, color.R, color.G, color.B }, 0
            );
        }

        public static Color ToColor(this int value)
        {
            var bytes = BitConverter.GetBytes(value);
            return Color.FromArgb(bytes[0], bytes[1], bytes[2], bytes[3]);
        }
    }
}
