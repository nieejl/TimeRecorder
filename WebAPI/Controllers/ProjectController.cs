using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.RepositoryLayer.Models.Entities;
using Server.WebAPI.AdapterRepositories;
using TimeRecorder.Shared;

namespace Server.WebAPI.Controllers
{
    public class ProjectController : AbstractCrudController<ProjectDTO, Project>
    {
        public ProjectController(IAdapterRepo<ProjectDTO, Project> repo) : base(repo)
        {
        }

        [ProducesResponseType(typeof(ProjectDTO), 200)]
        public override Task<ActionResult<ProjectDTO>> FindAsync(int id)
        {
            return base.FindAsync(id);
        }
    }
}