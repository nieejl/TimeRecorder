using System;
using System.Collections.Generic;
using System.Text;

namespace Server.RepositoryLayer.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public int TemporaryId { get; set; }
        public DateTime LastUpdated { get; set; }
        public int Version { get; set; }
    }
}
