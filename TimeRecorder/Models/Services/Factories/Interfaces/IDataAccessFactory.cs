using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRecorder.Models.Services.Factories.Interfaces
{
    public interface IDataAccessFactory<Repo>
    {
        Repo GetRepository();
        bool CanHandle(StorageStrategy strategy);
    }
}
