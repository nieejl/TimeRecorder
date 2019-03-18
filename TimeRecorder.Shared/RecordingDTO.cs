using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRecorder.Shared
{ 
    public class RecordingDTO : DTO
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public int? ProjectId { get; set; }
        public ProjectDTO Project { get; set; }
        //public List<TagDTO> Tags { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !this.GetType().Equals(obj.GetType()))
                return false;
            else
            {
                RecordingDTO other = (RecordingDTO)obj;

                return Title == other.Title &&
                    Start == other.Start &&
                    (End != null && other.End != null && End.Equals(other.End) ||
                    (End == null && other.End == null)) &&
                    ProjectId == other.ProjectId;
            }
        }
    }
}
