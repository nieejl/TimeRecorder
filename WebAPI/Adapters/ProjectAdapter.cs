using Server.RepositoryLayer.Models.Entities;
using TimeRecorder.Shared;

namespace Server.WebAPI.Adapters
{
    public class ProjectAdapter : IAdapter<ProjectDTO, Project>
    {
        public ProjectDTO ConvertToDTO(Project entity)
        {
            return new ProjectDTO
            {
                Id = entity.Id,
                TemporaryId = entity.TemporaryId,
                Argb = entity.Argb,
                Name = entity.Name,
            };
        }

        public Project ConvertToEntity(ProjectDTO dto)
        {
            return new Project
            {
                Id = dto.Id,
                TemporaryId = dto.TemporaryId,
                Argb = dto.Argb,
                Name = dto.Name
                //TODO: What to do about lastupdated and version?
            };
        }
    }
}
