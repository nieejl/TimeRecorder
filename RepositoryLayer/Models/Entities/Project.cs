using System;
using System.Collections.Generic;
using System.Text;

namespace Server.RepositoryLayer.Models.Entities
{
    public class Project : Entity
    {
        public string Name { get; set; }
        public UInt32 Argb { get; set; }
    }
}
