﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.RepositoryLayer.Models;
using Server.WebAPI.AdapterRepositories;
using TimeRecorder.Shared;

namespace Server.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class AbstractCrudController<DTOType, EntityType> : 
        ControllerBase
        where DTOType : DTO
        where EntityType : Entity
    {
        private IAdapterRepo<DTOType, EntityType> adapterRepo;

        public AbstractCrudController(IAdapterRepo<DTOType, EntityType> repo)
        {
            adapterRepo = repo;
        }

        [ProducesResponseType(typeof(NotFoundObjectResult), 404)]
        public virtual async Task<ActionResult<DTOType>> FindAsync(int id)
        {
            var dto = await adapterRepo.FindAsync(id);
            if (dto != null)
                return dto;
            return new NotFoundObjectResult(dto);
        }
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(NotFoundObjectResult), 404)]
        public virtual async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            bool deleted = await adapterRepo.DeleteAsync(id);
            if (deleted)
                return NoContent();
            return new NotFoundObjectResult(deleted);
        }

        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(NotFoundObjectResult), 404)]
        public virtual async Task<ActionResult<int>> PostAsync(DTOType dto)
        {
            int id = await adapterRepo.CreateAsync(dto);
            if (id != 0)
                return id;
            return new NotFoundObjectResult(id);
        }

        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(NotFoundObjectResult), 404)]
        public virtual async Task<ActionResult<bool>> PutAsync(DTOType dto)
        {
            bool updated = await adapterRepo.UpdateAsync(dto);
            if (updated)
                return NoContent();
            return new NotFoundObjectResult(false);
        }


    }
}