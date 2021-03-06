﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeRecorder.Server.RepositoryLayer.Models;

namespace TimeRecorder.Server.RepositoryLayer.Migrations
{
    [DbContext(typeof(TimeRecorderServerContext))]
    partial class TimeRecorderServerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Server.RepositoryLayer.Models.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Argb");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name");

                    b.Property<int>("TemporaryId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("TemporaryId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Server.RepositoryLayer.Models.Entities.Recording", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("End");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int?>("ProjectId");

                    b.Property<DateTime>("Start");

                    b.Property<int>("TemporaryId");

                    b.Property<string>("Title");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TemporaryId");

                    b.ToTable("Recordings");
                });

            modelBuilder.Entity("Server.RepositoryLayer.Models.Entities.Recording", b =>
                {
                    b.HasOne("Server.RepositoryLayer.Models.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");
                });
#pragma warning restore 612, 618
        }
    }
}
