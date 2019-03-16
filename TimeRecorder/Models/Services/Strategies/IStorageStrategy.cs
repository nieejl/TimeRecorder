using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.Shared;

namespace TimeRecorder.Models.Services.Strategies
{
    public interface IStorageStrategy<Factory, Repo>
    {
        Repo CreateRepository(StorageStrategy strategy);
    }
}
