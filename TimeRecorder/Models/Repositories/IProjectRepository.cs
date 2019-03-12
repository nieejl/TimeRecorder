﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Repositories
{
    public interface IProjectRepository : ICrudRepository<ProjectDTO>
    {
    }
}
