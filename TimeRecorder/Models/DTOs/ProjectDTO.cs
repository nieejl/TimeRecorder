using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TimeRecorder.Models.Extensions;

namespace TimeRecorder.Models.DTOs
{
    public class ProjectDTO
    {
        public string Name { get; set; }
        public Int32 Argb {
            get {
                return Color.ToInt32();
            }
            set {
                Color = value.ToColor();
            }
        }

        [NotMapped]
        public Color Color { get; set; }
    }
}
