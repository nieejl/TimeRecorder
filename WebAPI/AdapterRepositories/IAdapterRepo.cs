using Server.RepositoryLayer.Repositories;
using Server.WebAPI.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.WebAPI.AdapterRepositories
{
    public interface IAdapterRepo <DTOType, EntityType> : 
        IRepository<DTOType>, 
        IAdapter<DTOType, EntityType>
    {

    }
}
