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
        public static UInt32 ToUInt(this Color color) {
            // Window.Media colors are BGRA - which explains why ordering is reversed.
            return BitConverter.ToUInt32(
                new byte[] { color.B, color.G, color.R, color.A }, 0
            );
        }

        public static Color ToColor(this UInt32 value)
        {
            var bytes = BitConverter.GetBytes(value);
            // Window.Media colors are BGRA - which explains why ordering is reversed.
            return Color.FromArgb(bytes[3], bytes[2], bytes[1], bytes[0]);
        }
    }
}
