using TimeRecorder.Server.RepositoryLayer.Models;
using TimeRecorder.Server.RepositoryLayer.Repositories;
using TimeRecorder.Server.WebAPI.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRecorder.Shared;

namespace TimeRecorder.Server.WebAPI.AdapterRepositories.Interfaces
{
    public interface IAdapterRepo <DTOType, EntityType> : 
        IRepository<DTOType>, 
        IAdapter<DTOType, EntityType>
        where DTOType : DTO
        where EntityType : Entity
    {

    }
}
