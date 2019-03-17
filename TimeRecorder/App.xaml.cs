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
using TimeRecorder.Models.Services.Factories.Interfaces;
using TimeRecorder.Models.Services.Factories.Local;
using TimeRecorder.Models.Services.Factories.Online;
using TimeRecorder.Models.Services.LocalStorage;
using TimeRecorder.Models.Services.RepositoryInterfaces;
using TimeRecorder.Models.Services.ServerStorage;
using TimeRecorder.Models.Services.Strategies;
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
            string backEndUri = "http://localhost:3535/";
            iocContainer.AddTransient<IHttpClient, CustomHttpClient>(p => new CustomHttpClient(backEndUri));

            iocContainer.AddTransient<IRecordingRepository, RecordingLocalRepository>();
            iocContainer.AddTransient<IRecordingRepository, RecordingOnlineRepository>();
            iocContainer.AddTransient<IDataAccessFactory<IRecordingRepository>, RecordingLocalStorageFactory>();
            iocContainer.AddTransient<IDataAccessFactory<IRecordingRepository>, RecordingOnlineStorageFactory>();
            iocContainer.AddTransient<RecordingStrategy>();

            iocContainer.AddTransient<IProjectRepository, ProjectLocalRepository>();
            iocContainer.AddTransient<IProjectRepository, ProjectOnlineRepository>();
            iocContainer.AddTransient<IDataAccessFactory<IProjectRepository>, ProjectLocalStorageFactory>();
            iocContainer.AddTransient<IDataAccessFactory<IProjectRepository>, ProjectOnlineStorageFactory>();
            iocContainer.AddTransient<ProjectStrategy>();

            iocContainer.AddSingleton<ITimeStringParser, TimeStringParser>();
            iocContainer.AddSingleton<IParserFieldVMFactory, ParserFieldVMFactory>();

            iocContainer.AddTransient<IRecordingOverviewVM, RecordingOverviewPageVM>();
            iocContainer.AddTransient<IRecordingDetailPageVM, RecordingDetailPageVM>();


            Services = iocContainer.BuildServiceProvider();
        }
    }
}
