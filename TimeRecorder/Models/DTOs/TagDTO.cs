﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRecorder.Models.DTOs
{
    public class TagDTO : IDTO
    {
        public int Id { get; set; }
        public string TagValue { get; set; }
    }
}