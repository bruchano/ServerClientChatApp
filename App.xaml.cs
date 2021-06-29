using Microsoft.AspNetCore.SignalR.Client;
using Pie.Domain.Services;
using Pie.EntityFramework;
using Pie.EntityFramework.Services;
using Pie.EntityFramework.Services.ServerServices;
using Pie.EntityFramework.Services.UserStateHandlers;
using Pie.ServerResponds;
using Pie.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Pie
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/Pie")
                .Build();

            ServerService serverService = ServerService.CreateServerService(connection);
            IUserDataService userDataService = new UserDataService(new PieDbContextFactory());
            IUserStateHandler userStateHandler = new UserStateHandler(serverService);
            WindowViewModel windowViewModel = new WindowViewModel(serverService, userDataService, userStateHandler);
            IServerRespondsHandler serverRespondsHandler = new ServerRespondsHandler(windowViewModel, connection, userDataService, userStateHandler);

            MainWindow window = new MainWindow()
            {
                DataContext = windowViewModel
            };
            
            window.Show();
        }
    }
}
