using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.RepositoryLayer.Models;
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

        [ProducesResponseType(typeof(NotFoundObjectResult), 404)]
        public async Task<ActionResult<string>> Find(int id)
        {
            //var entity = await adapter.FindAsync(id);
            return "value";
        }
    }
}