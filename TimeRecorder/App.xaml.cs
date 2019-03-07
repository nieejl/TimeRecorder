using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TimeRecorder.ViewModels;

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
            iocContainer.AddTransient<MainWindowViewModel>();

            Services = iocContainer.BuildServiceProvider();
        }
    }
}
