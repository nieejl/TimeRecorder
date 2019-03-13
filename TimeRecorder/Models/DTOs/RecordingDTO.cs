using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRecorder.Models.DTOs
{
    public class RecordingDTO :IDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public ProjectDTO Project { get; set; }
        public List<TagDTO> Tags { get; set; }
    }
}
