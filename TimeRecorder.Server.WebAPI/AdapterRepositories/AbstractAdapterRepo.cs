using TimeRecorder.Server.RepositoryLayer.Models;
using TimeRecorder.Server.RepositoryLayer.Repositories;
using TimeRecorder.Server.WebAPI.AdapterRepositories.Interfaces;
using TimeRecorder.Server.WebAPI.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRecorder.Shared;

namespace TimeRecorder.Server.WebAPI.AdapterRepositories
{
    public abstract class AbstractAdapterRepo<DTOType, EntityType> :
        //IRepository<DTOType> // TODO: Implement only irepository
        IAdapterRepo<DTOType, EntityType>
        where EntityType : Entity
        where DTOType : DTO
    {
        protected IRepository<EntityType> repository;
        protected IAdapter<DTOType, EntityType> adapter;


        public AbstractAdapterRepo(IRepository<EntityType> repository, 
            IAdapter<DTOType, EntityType> adapter)
        {
            this.repository = repository;
            this.adapter = adapter;
        }

        public DTOType ConvertToDTO(EntityType entity)
        {
            return adapter.ConvertToDTO(entity);
        }

        public EntityType ConvertToEntity(DTOType dto)
        {
            return adapter.ConvertToEntity(dto);
        }

        public async Task<int> CreateAsync(DTOType dto)
        {
            var entity = adapter.ConvertToEntity(dto);
            int id = await repository.CreateAsync(entity);
            return id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await repository.DeleteAsync(id);
        }

        public async Task<DTOType> FindAsync(int id)
        {
            var entity = await repository.FindAsync(id);
            if (entity == null)
                return null;
            var dto = adapter.ConvertToDTO(entity);
            return dto;
        }

        public async Task<IQueryable<DTOType>> Read()
        {
            var entities = await repository.Read();
            return entities.Select(e => adapter.ConvertToDTO(e));
        }

        public async Task<bool> UpdateAsync(DTOType dto)
        {
            var entity = adapter.ConvertToEntity(dto);
            return await repository.UpdateAsync(entity);
        }
    }
}
