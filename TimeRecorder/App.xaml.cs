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
using TimeRecorder.Models.Services.LocalStorage;
using TimeRecorder.Models.Services.RepositoryInterfaces;
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

            iocContainer.AddScoped<ITimeRecorderContext, TimeRecorderContext>();
            iocContainer.AddTransient<IRecordingRepository, RecordingLocalRepository>();
            iocContainer.AddTransient<IProjectRepository, ProjectLocalRepository>();
            iocContainer.AddTransient<ITagRepository, TagRepository>();
            //iocContainer.AddTransient<IDataAccessFactory, DataAccessFactory>();

            iocContainer.AddSingleton<ITimeStringParser, TimeStringParser>();
            iocContainer.AddSingleton<IParserFieldVMFactory, ParserFieldVMFactory>();

            iocContainer.AddTransient<IRecordingOverviewVM, RecordingOverviewPageVM>();
            iocContainer.AddTransient<IRecordingDetailPageVM, RecordingDetailPageVM>();


            Services = iocContainer.BuildServiceProvider();
        }
    }
}
