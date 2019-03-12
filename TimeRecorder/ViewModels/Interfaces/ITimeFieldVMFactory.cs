using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.ViewModels.Interfaces;

namespace TimeRecorder.ViewModels
{
    public interface IParserFieldVMFactory
    {
        ITimeFieldVM Generate24HourTimeField();
        ITimeFieldVM GenerateUnlimitedTimeField();
        IDateFieldVM GenerateDateField();
    }
}
