using Microsoft.AspNetCore.Mvc.Formatters;
using Moq;
using Moq.Language.Flow;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
//using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;
using TimeRecorder.Models.Services.ServerStorage;
using Xunit;

namespace TimeRecorder.Tests.Models.Services.ServerStorage
{
    public abstract class AbstractOnlineCrudRepoTest<DTOType, RepoType>
        where DTOType : LocalEntity
        where RepoType : AbstractOnlineCrudRepo<DTOType>
    {
        public HttpResponseMessage CreateHttpResponse<T>(HttpStatusCode code, T value)
        {
            var response = new HttpResponseMessage(code);
            var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8);
            //var content = new ObjectContent<T>(value, new JsonMediaTypeFormatter());
            response.Content = content;
            return response;
        }

        public Mock<IHttpClient> CreateIHttpMock()
        {
            //var formatters = new MediaTypeFormatter[] { new JsonMediaTypeFormatter()};
            var mock = new Mock<IHttpClient>();
            //mock.Setup(m => m.Formatters).Returns(formatters);
            return mock;
        }


        public Mock<IHttpClient> CreatePostAndPutClient<T>(HttpStatusCode code, T value)
        {
            var response = CreateHttpResponse(code, value);
            var mock = CreateIHttpMock();
            mock.Setup(m => m.PostAsJsonAsync(It.IsAny<string>(), It.IsAny<DTOType>())).
                ReturnsAsync(response);
            mock.Setup(m => m.PutAsJsonAsync(It.IsAny<string>(), It.IsAny<DTOType>())).
                ReturnsAsync(response);
            return mock;
        }

        Mock<IHttpClient> CreateDefaultPostAndPutClient(HttpStatusCode code)
        {
            var dto = CreateSampleValue(1);
            return CreatePostAndPutClient(code, dto);
        }

        public abstract RepoType CreateRepo(IHttpClient client);
        public abstract DTOType CreateSampleValue(int id = 1);

        [Fact]
        public async Task Test_CreateAsync_Calls_PostAsJsonAsync()
        {
            var dto = CreateSampleValue();
            var client = CreatePostAndPutClient(HttpStatusCode.OK, dto.Id);
            var repo = CreateRepo(client.Object);

            await repo.CreateAsync(dto);

            client.Verify(m => m.PostAsJsonAsync(It.IsAny<string>(), It.IsAny<DTOType>()), Times.Once);
        }

        [Fact]
        public async Task Test_UpdateAsync_Calls_PutAsJsonAsync()
        {
            var dto = CreateSampleValue();
            var client = CreatePostAndPutClient(HttpStatusCode.OK, true);
            var repo = CreateRepo(client.Object);

            await repo.UpdateAsync(dto);

            client.Verify(m => m.PutAsJsonAsync(It.IsAny<string>(), It.IsAny<DTOType>()), Times.Once);
        }

        [Fact]
        public async Task Test_DeleteAsync_Calls_DeleteAsync_With_Id()
        {
            var client = CreateIHttpMock();
            var response = CreateHttpResponse(HttpStatusCode.OK, true);
            client.Setup(m => m.DeleteAsync(It.IsAny<string>())).ReturnsAsync(response);
            var repo = CreateRepo(client.Object);

            await repo.DeleteAsync(1);

            client.Verify(m => m.DeleteAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Test_FindAsync_Calls_GetAsync_With_Id()
        {
            var response = CreateHttpResponse(HttpStatusCode.OK, CreateSampleValue());
            var client = CreateIHttpMock();
            client.Setup(m => m.GetAsync(It.IsAny<string>())).ReturnsAsync(response);
            var repo = CreateRepo(client.Object);

            await repo.FindAsync(1);

            client.Verify(m => m.GetAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Test_FindAsync_Returns_DTO_On_SuccessCode_And_Not_Null_Value()
        {
            var dto = CreateSampleValue();
            var response = CreateHttpResponse(HttpStatusCode.OK, dto);
            var client = CreateIHttpMock();
            client.Setup(m => m.GetAsync(It.IsAny<string>())).ReturnsAsync(response);
            var repo = CreateRepo(client.Object);

            var result = await repo.FindAsync(1);

            Assert.Equal(dto, result);
        }

        [Fact]
        public async Task Test_CreateAsync_Returns_Id_On_SuccessCode_And_Not_Null_Value()
        {
            var dto = CreateSampleValue();
            var client = CreatePostAndPutClient(HttpStatusCode.OK, dto.Id);
            var repo = CreateRepo(client.Object);

            var result = await repo.CreateAsync(dto);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Test_UpdateAsync_Returns_True_On_SuccessCode_And_Not_Null_Value()
        {
            var dto = CreateSampleValue();
            var client = CreatePostAndPutClient(HttpStatusCode.OK, true);
            var repo = CreateRepo(client.Object);

            var result = await repo.UpdateAsync(dto);

            Assert.True(result);
        }

        [Fact]
        public async Task Test_DeleteAsync_Returns_True_On_SuccessCode_And_Not_Null_Value()
        {
            var client = CreateIHttpMock();
            var response = CreateHttpResponse(HttpStatusCode.OK, true);
            client.Setup(m => m.DeleteAsync(It.IsAny<string>())).ReturnsAsync(response);
            var repo = CreateRepo(client.Object);

            var result = await repo.DeleteAsync(1);

            Assert.True(result);
        }

        [Fact]
        public async Task Test_FindAsync_Returns_Null_On_ErrorCode()
        {
            var dto = CreateSampleValue();
            var response = CreateHttpResponse(HttpStatusCode.NotFound, dto);
            var client = CreateIHttpMock();
            client.Setup(m => m.GetAsync(It.IsAny<string>())).ReturnsAsync(response);
            var repo = CreateRepo(client.Object);

            var result = await repo.FindAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task Test_CreateAsync_Returns_Zero_On_ErrorCode()
        {
            var dto = CreateSampleValue();
            var client = CreatePostAndPutClient(HttpStatusCode.BadRequest, dto.Id);
            var repo = CreateRepo(client.Object);

            var result = await repo.CreateAsync(dto);

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task Test_UpdateAsync_Returns_False_On_ErrorCode()
        {
            var dto = CreateSampleValue();
            var client = CreatePostAndPutClient(HttpStatusCode.NotFound, true);
            var repo = CreateRepo(client.Object);

            var result = await repo.UpdateAsync(dto);

            Assert.False(result);
        }

        [Fact]
        public async Task Test_DeleteAsync_Returns_False_On_ErrorCode()
        {
            var client = CreateIHttpMock();
            var response = CreateHttpResponse(HttpStatusCode.NotFound, true);
            client.Setup(m => m.DeleteAsync(It.IsAny<string>())).ReturnsAsync(response);
            var repo = CreateRepo(client.Object);

            var result = await repo.DeleteAsync(1);

            Assert.False(result);
        }

        [Fact]
        public async Task Test_FindAsync_Returns_Null_On_Null_Reponse()
        {
            var dto = CreateSampleValue();
            var response = CreateHttpResponse(HttpStatusCode.NotFound, dto);
            var client = CreateIHttpMock();
            client.Setup(m => m.GetAsync(It.IsAny<string>())).ReturnsAsync(response);
            var repo = CreateRepo(client.Object);

            var result = await repo.FindAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task Test_CreateAsync_Returns_Zero_On_Null_Reponse()
        {
            var dto = CreateSampleValue();
            var client = CreateIHttpMock();
            client.Setup(m => m.PostAsJsonAsync(It.IsAny<string>(), dto)).ReturnsAsync(default(HttpResponseMessage));
            var repo = CreateRepo(client.Object);

            var result = await repo.CreateAsync(dto);

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task Test_UpdateAsync_Returns_False_On_Null_Value_Reponse()
        {
            var dto = CreateSampleValue();
            var client = CreateIHttpMock();
            client.Setup(m => m.PutAsJsonAsync(It.IsAny<string>(), dto)).ReturnsAsync(default(HttpResponseMessage));
            var repo = CreateRepo(client.Object);

            var result = await repo.UpdateAsync(dto);

            Assert.False(result);
        }

        [Fact]
        public async Task Test_DeleteAsync_Returns_False_On_Null_Value_Response()
        {
            var client = CreateIHttpMock();
            var response = default(HttpResponseMessage);
            client.Setup(m => m.DeleteAsync(It.IsAny<string>())).ReturnsAsync(response);
            var repo = CreateRepo(client.Object);

            var result = await repo.DeleteAsync(1);

            Assert.False(result);
        }
    }
}
