using Server.RepositoryLayer.Models.Entities;
using TimeRecorder.Shared;

namespace Server.WebAPI.Adapters
{
    public class RecordingAdapter : IAdapter<RecordingDTO, Recording>
    {
        public RecordingDTO ConvertToDTO(Recording entity)
        {
            return new RecordingDTO
            {
                Id = entity.Id,
                TemporaryId = entity.TemporaryId,
                Start = entity.Start,
                End = entity.End,
                ProjectId = entity.ProjectId,
                Title = entity.Title,
            };
        }

        public Recording ConvertToEntity(RecordingDTO dto)
        {
            return new Recording
            {
                TemporaryId = dto.TemporaryId,
                Start = dto.Start,
                End = dto.End,
                ProjectId = dto.ProjectId,
                Title = dto.Title,
                //TODO: What to do about lastupdated and version?
            };
        }
    }
}
