using TimeRecorder.Server.RepositoryLayer.Models;
using TimeRecorder.Server.RepositoryLayer.Models.Entities;
using TimeRecorder.Server.RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeRecorder.Server.RepositoryLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new TimeRecorderServerContext();
            var repo = new ProjectRepository(context);

            var project = new Project { Argb = 42, Name = "hey project", TemporaryId = 62456 };
            //var task = repo.CreateAsync(project);
            var task = repo.FindAsync(1);
            task.Wait();
            Console.WriteLine(task.Result.Name);
            Console.ReadKey();
        }
    }
}
