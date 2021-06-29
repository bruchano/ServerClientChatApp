using Microsoft.AspNet.Identity;
using Pie.Commands;
using Pie.Commands.ViewCommand;
using Pie.Domain.Services;
using Pie.EntityFramework;
using Pie.EntityFramework.Services;
using Pie.EntityFramework.Services.IAuthenticationService;
using Pie.EntityFramework.Services.ServerServices;
using Pie.EntityFramework.Services.UserStateHandlers;
using Pie.ServerResponds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pie.ViewModels
{
    public class WindowViewModel : BaseViewModel
    {

        private readonly ServerService _serverService;
        private readonly IUserDataService _userDataService;
        private IUserStateHandler _userStateHandler;

        public ViewCommands viewCommands;
        private BaseViewModel _leftSideBarViewModel;
        private BaseViewModel _mainPanelViewModel;

        public BaseViewModel LeftSideBarViewModel
        {
            get
            {
                return _leftSideBarViewModel;
            }
            set
            {
                _leftSideBarViewModel = value;
                OnPropertyChanged(nameof(LeftSideBarViewModel));
            }
        }

        public BaseViewModel MainPanelViewModel
        {
            get
            {
                return _mainPanelViewModel;
            }
            set
            {
                _mainPanelViewModel = value;
                OnPropertyChanged(nameof(MainPanelViewModel));
            }
        }

        public WindowViewModel(ServerService serverService, IUserDataService userDataService, IUserStateHandler userStateHandler)
        {
            _serverService = serverService;
            _userDataService = userDataService;
            _userStateHandler = userStateHandler;
            viewCommands = new ViewCommands(this, _serverService, _userStateHandler);

            viewCommands.ShowLoginViewCommand.Execute(null);
            viewCommands.ShowGuestPanelViewCommand.Execute(null);
        }

    }
}
