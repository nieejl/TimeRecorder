using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Extensions;
using TimeRecorder.ViewModels;

namespace TimeRecorder.Models.ValueConverters
{
    public static class DTOAdapter
    {
        public static RecordingSummaryVM ToSummaryVM(this RecordingDTO dto)
        {
            var title = dto.Title ?? "No description";

            var projectName = dto.Project != null ?
                dto.Project.Name != null ? dto.Project.Name
                : "Unnamed Project" : "No Project Chosen";

            var color = dto.Project == null || dto.Project.Color == null ?
                new SolidColorBrush(Colors.LightGray) : new SolidColorBrush(dto.Project.Color);
            string elapsed = "";
            if (dto.End != null)
            {
                elapsed = (dto.End.Value - dto.Start).ToHHMMSS();

            }
            return new RecordingSummaryVM
            {
                Id = dto.Id,
                Title = title,
                ProjectName = projectName,
                ProjectColor = color,
                Duration = elapsed
            };
        }

        public static Shared.ProjectDTO ToDTO(this ProjectDTO localEntity)
        {
            return new Shared.ProjectDTO
            {
                Id = localEntity.Id,
                Argb = localEntity.Argb,
                Name = localEntity.Name,
                TemporaryId = localEntity.TemporaryId
            };
        }

        public static Shared.RecordingDTO ToDTO(this RecordingDTO localEntity)
        {
            return new Shared.RecordingDTO
            {
                Id = localEntity.Id,
                TemporaryId = localEntity.TemporaryId,
                Title = localEntity.Title,
                Start = localEntity.Start,
                End = localEntity.End,
                ProjectId = localEntity.ProjectId,
                Project = localEntity.Project.ToDTO(),
            };
        }

        public static ProjectDTO ToDTO(this Shared.ProjectDTO localEntity)
        {
            return new ProjectDTO
            {
                Id = localEntity.Id,
                Argb = localEntity.Argb,
                Name = localEntity.Name,
                TemporaryId = localEntity.TemporaryId
            };
        }

        public static RecordingDTO ToDTO(this Shared.RecordingDTO dto)
        {
            return new RecordingDTO
            {
                Id = dto.Id,
                TemporaryId = dto.TemporaryId,
                Title = dto.Title,
                Start = dto.Start,
                End = dto.End,
                ProjectId = dto.ProjectId,
                Project = dto.Project.ToDTO(),
            };
        }


    }
}
