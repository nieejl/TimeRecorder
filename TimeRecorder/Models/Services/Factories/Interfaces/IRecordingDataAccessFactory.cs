﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.Services.Factories.Interfaces;
using TimeRecorder.Models.Services.LocalStorage;

namespace TimeRecorder.Models.Services
{
    public interface IRecordingDataAccessFactory : 
        IDataAccessFactory<IRecordingRepository>
    {
    }
}
