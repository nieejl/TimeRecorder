using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Models.Services.Factories.Interfaces;
using TimeRecorder.Models.Services.RepositoryInterfaces;

namespace TimeRecorder.Models.Services.Strategies
{
    public interface IProjectStrategy : IStorageStrategy<IDataAccessFactory<IProjectRepository>, IProjectRepository>
    {
    }
}
