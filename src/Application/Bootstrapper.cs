using Caliburn.Micro;
using Ivao.It.AuroraHelper.Application.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Ivao.It.AuroraHelper.Application;

public class Bootstrapper : BootstrapperBase
{
    private ServiceProvider _serviceProvider;

    public Bootstrapper()
    {
        Initialize();
    }

    protected override void Configure()
    {
        var sc = new ServiceCollection();

        //Config - Json like aspnetcore
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        //ViewModels
        sc.AddScoped<DummyViewModel>();
        sc.AddScoped<ShellViewModel>();

        //Services

        //Caliburn
        sc.AddScoped<IWindowManager, WindowManager>();
        sc.AddScoped<IEventAggregator, EventAggregator>();

        //Wiring up with Bootstrapper
        this._serviceProvider = sc.BuildServiceProvider();
    }


    protected override object GetInstance(Type service, string key)
        => _serviceProvider.GetService(service);

    protected override IEnumerable<object> GetAllInstances(Type service)
        => _serviceProvider.GetServices(service);

    protected override void OnStartup(object sender, StartupEventArgs e)
    {
        DisplayRootViewForAsync<ShellViewModel>(
            new Dictionary<string, object>{
                {"Title", "IVAO IT Aurora Helper" },
                {"MinWidth", 600 },
                {"MinHeight", 600 },
            });
    }
}
