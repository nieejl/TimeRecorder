using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeRecorder.Server.RepositoryLayer.Models.Entities;
using TimeRecorder.Server.WebAPI.AdapterRepositories.Interfaces;
using TimeRecorder.Shared;

namespace TimeRecorder.Server.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : AbstractCrudController<ProjectDTO, Project>
    {
        protected new IProjectAdapterRepo adapterRepo;
        public ProjectController(IProjectAdapterRepo repo) : base(repo)
        {
            adapterRepo = repo;
            Debug.WriteLine("project controller created");
        }

        [ProducesResponseType(typeof(ProjectDTO), 200)]
        public override Task<ActionResult<ProjectDTO>> FindAsync(int id)
        {
            return base.FindAsync(id);
        }

        [ProducesResponseType(typeof(IEnumerable<ProjectDTO>), 200)]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> Read() {
            return (await adapterRepo.Read()).ToList();
        }
    }
}