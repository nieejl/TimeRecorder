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

            var elapsed = (dto.End - dto.Start).Value.ToHHMMSS();
            return new RecordingSummaryVM
            {
                Title = title,
                ProjectName = projectName,
                ProjectColor = color,
                Duration = elapsed
            };
            //if (dto.Project == null)
            //    return new RecordingSummaryVM
            //    {
            //        Title = dto.Title,
            //        Elapsed
            //    }
            //return new RecordingSummaryVM
            //{
            //    Title = dto.Title ?? "No description",
            //    ProjectName = dto.Project.Name ?? "No ,
            //    ProjectColor = dto.Project.Color,
            //    Elapsed = (dto.End - dto.Start).Value.ToSimpleString()
            //};
        }
    }
}
