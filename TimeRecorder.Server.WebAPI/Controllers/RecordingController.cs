using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeRecorder.Server.RepositoryLayer.Models.Entities;
using TimeRecorder.Server.WebAPI.AdapterRepositories;
using TimeRecorder.Server.WebAPI.AdapterRepositories.Interfaces;
using TimeRecorder.Shared;

namespace TimeRecorder.Server.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordingController : AbstractCrudController<RecordingDTO, Recording>
    {
        protected new IRecordingAdapterRepo adapterRepo;
        public RecordingController(IRecordingAdapterRepo repo) : base(repo)
        {
            adapterRepo = repo;
        }

        [ProducesResponseType(typeof(NotFoundObjectResult), 404)]
        [ProducesResponseType(typeof(RecordingDTO), 200)]
        public override Task<ActionResult<RecordingDTO>> FindAsync(int id)
        {
            return base.FindAsync(id);
        }

        [HttpGet("read/{amount}/{startIndex}")]
        [ProducesResponseType(typeof(IEnumerable<RecordingDTO>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<ActionResult<IEnumerable<RecordingDTO>>> ReadAmount(int amount, int startIndex) {
            var dtos = await adapterRepo.ReadAmount(amount, startIndex);
            if (dtos != null && dtos.Count() > 0)
                return Ok(dtos);
            else
                return NotFound();
        }
    }
}
