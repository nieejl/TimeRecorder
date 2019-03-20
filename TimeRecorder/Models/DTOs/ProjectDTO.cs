using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TimeRecorder.Models.Extensions;

namespace TimeRecorder.Models.DTOs
{
    public class ProjectDTO : LocalEntity
    {
        [Required]
        public string Name { get; set; }
        public UInt32 Argb {
            get {
                return Color.ToUInt();
            }
            set {
                Color = value.ToColor();
            }
        }

        [NotMapped]
        public Color Color { get; set; }
        //public DateTime LastUsed { get; set; }
        //public bool IsArchived { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !this.GetType().Equals(obj.GetType()))
                return false;
            else
            {
                ProjectDTO other = (ProjectDTO)obj;

                return Name == other.Name &&
                    Argb == other.Argb &&
                    Id == other.Id &&
                    TemporaryId == other.TemporaryId;
            }
        }

    }
}
