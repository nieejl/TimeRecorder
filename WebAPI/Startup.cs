﻿using System;
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
using Swashbuckle.AspNetCore.Swagger;
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

            services.AddDbContext<ITimeRecorderServerContext, TimeRecorderServerContext>();
            services.AddScoped<ITimeRecorderServerContext, TimeRecorderServerContext>();

            services.AddTransient<IAdapter<ProjectDTO, Project>, ProjectAdapter>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IAdapterRepo<ProjectDTO, Project>, ProjectAdapterRepo>();

            services.AddTransient<IAdapter<RecordingDTO, Recording>, RecordingAdapter>();
            services.AddTransient<IRecordingRepository, RecordingRepository>();
            services.AddTransient<IAdapterRepo<RecordingDTO, Recording>, RecordingAdapterRepo>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
