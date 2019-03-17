using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Server.RepositoryLayer.Models;
using Server.RepositoryLayer.Models.Entities;
using Server.RepositoryLayer.Repositories;
using Server.WebAPI.AdapterRepositories;
using Server.WebAPI.Adapters;
using TimeRecorder.Shared;

namespace Server.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddTransient<ITimeRecorderServerContext, TimeRecorderServerContext>();

            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IAdapter<ProjectDTO, Project>, ProjectAdapter>();
            services.AddTransient<IAdapterRepo<ProjectDTO, Project>, ProjectAdapterRepo>();

            services.AddTransient<IRecordingRepository, RecordingRepository>();
            services.AddTransient<IAdapter<RecordingDTO, Recording>>();
            services.AddTransient<IAdapterRepo<RecordingDTO, Recording>, RecordingAdapterRepo>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
