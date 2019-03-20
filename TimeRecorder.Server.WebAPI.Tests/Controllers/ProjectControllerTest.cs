using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using TimeRecorder.Server.RepositoryLayer.Models.Entities;
using TimeRecorder.Server.WebAPI.AdapterRepositories.Interfaces;
using TimeRecorder.Server.WebAPI.Controllers;
using TimeRecorder.Shared;

namespace TimeRecorder.Server.WebAPI.Tests.Controllers
{
    public class ProjectControllerTest
        : AbstractCrudControllerTest<ProjectController, IProjectAdapterRepo, ProjectDTO, Project>
    {
        public override ProjectController CreateController(IProjectAdapterRepo adapterRepo)
        {
            return new ProjectController(adapterRepo);
        }

        public override Mock<IProjectAdapterRepo> CreateMock()
        {
            return new Mock<IProjectAdapterRepo>();
        }

        public override ProjectDTO CreateSampleNullValue() => null;

        public override ProjectDTO CreateSampleValue() => TestDataGenerator.CreateProjectDTO();
    }
}
