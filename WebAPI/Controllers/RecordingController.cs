using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.RepositoryLayer.Models.Entities;
using Server.WebAPI.AdapterRepositories;
using TimeRecorder.Shared;

namespace Server.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordingController : AbstractCrudController<RecordingDTO, Recording>
    {
        public RecordingController(IAdapterRepo<RecordingDTO, Recording> repo) : base(repo)
        {
        }

        [ProducesResponseType(typeof(NotFoundObjectResult), 404)]
        [ProducesResponseType(typeof(RecordingDTO), 200)]
        public override Task<ActionResult<RecordingDTO>> FindAsync(int id)
        {
            return base.FindAsync(id);
        }
    }
}
