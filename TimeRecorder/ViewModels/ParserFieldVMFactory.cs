using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.ValueParsers;
using TimeRecorder.ViewModels.Interfaces;

namespace TimeRecorder.ViewModels
{
    public class ParserFieldVMFactory : IParserFieldVMFactory
    {
        private ITimeStringParser parser;
        public ParserFieldVMFactory(ITimeStringParser timeParser)
        {
            parser = timeParser;
        }

        public ITimeFieldVM Generate24HourTimeField() => new TimeFieldVM(parser, true);

        public IDateFieldVM GenerateDateField() => new DateFieldVM();

        public ITimeFieldVM GenerateUnlimitedTimeField() => new TimeFieldVM(parser, false);

    }
}
