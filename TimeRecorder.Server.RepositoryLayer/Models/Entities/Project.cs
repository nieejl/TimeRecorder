using System;
using System.Collections.Generic;
using System.Text;

namespace TimeRecorder.Server.RepositoryLayer.Models.Entities
{
    public class Project : Entity
    {
        public string Name { get; set; }
        public UInt32 Argb { get; set; }
    }
}
