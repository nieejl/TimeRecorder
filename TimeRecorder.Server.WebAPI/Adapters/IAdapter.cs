using TimeRecorder.Server.RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeRecorder.Server.WebAPI.Adapters
{
    public interface IAdapter<DTOType, EntityType>
    {
        DTOType ConvertToDTO(EntityType entity);
        EntityType ConvertToEntity(DTOType dto);
    }
}
