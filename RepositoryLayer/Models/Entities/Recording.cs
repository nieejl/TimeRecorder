using System;
using System.Collections.Generic;
using System.Text;

namespace Server.RepositoryLayer.Models.Entities
{
    public class Recording : Entity
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public int? ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
