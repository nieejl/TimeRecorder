using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TimeRecorder.Models;
using TimeRecorder.Models.Services.Repositories;
using TimeRecorder.Models.ValueParsers;
using TimeRecorder.ViewModels;
using TimeRecorder.ViewModels.Interfaces;
namespace TimeRecorder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider Services { get; private set; }
        public App()
        {
            RegisterServices();
        }

        public void RegisterServices()
        {
            var iocContainer = new ServiceCollection();

            iocContainer.AddScoped<ITimeRecorcerContext, TimeRecorderContext>();
            iocContainer.AddTransient<IRecordingRepository, RecordingRepository>();
            iocContainer.AddTransient<IProjectRepository, ProjectRepository>();
            iocContainer.AddTransient<ITagRepository, TagRepository>();

            iocContainer.AddSingleton<ITimeStringParser, TimeStringParser>();
            iocContainer.AddSingleton<IParserFieldVMFactory, ParserFieldVMFactory>();

            iocContainer.AddTransient<IRecordingOverviewVM, RecordingOverviewPageVM>();
            iocContainer.AddTransient<IRecordingDetailPageVM, RecordingDetailPageVM>();


            Services = iocContainer.BuildServiceProvider();
        }
    }
}
