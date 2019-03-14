﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRecorder.Shared
{ 
    public class ProjectDTO : IDTO
    {
        public int Id { get; set; }
        public int TemporaryId { get; set; }
        public string Name { get; set; }
        public UInt32 Argb { get; set; }
    }
}
