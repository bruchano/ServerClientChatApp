using Microsoft.AspNetCore.SignalR.Client;
using Pie.EntityFramework.Services.IAuthenticationService;
using Pie.EntityFramework.Services.ServerServices;
using Pie.EntityFramework.Services.UserStateHandlers;
using Pie.ViewModels;
using System.Windows.Input;

namespace Pie.Commands.ViewCommand
{
    public class ViewCommands
    {
        private WindowViewModel _windowViewModel;
        private readonly ServerService _serverService;
        private readonly IUserStateHandler _userStateHandler;

        public ICommand ShowLoginViewCommand { get; private set; }
        public ICommand ShowRegisterViewCommand { get; private set; }
        public ICommand ShowMenuViewCommand { get; private set; }

        public ICommand ShowGuestPanelViewCommand { get; private set; }
        public ICommand ShowUserPanelViewCommand { get; private set; }
        public ICommand ShowFriendsPanelViewCommand { get; private set; }
        public ICommand ShowChatsPanelViewCommand { get; private set; }

        public ViewCommands(WindowViewModel windowViewModel, ServerService serverService, IUserStateHandler userStateHandler)
        {
            _windowViewModel = windowViewModel;
            _serverService = serverService;
            _userStateHandler = userStateHandler;

            ShowLoginViewCommand = new VoidCommand(ShowLoginView);
            ShowRegisterViewCommand = new VoidCommand(ShowRegisterView);
            ShowMenuViewCommand = new VoidCommand(ShowMenuView);
            ShowGuestPanelViewCommand = new VoidCommand(ShowGuestPanelView);
            ShowUserPanelViewCommand = new VoidCommand(ShowUserPanelView);
            ShowFriendsPanelViewCommand = new VoidCommand(ShowFriendsPanelView);
            ShowChatsPanelViewCommand = new VoidCommand(ShowChatsPanelView);
        }

        public void ShowLoginView()
        {
            _windowViewModel.LeftSideBarViewModel = new LoginViewModel(_windowViewModel, _userStateHandler, this);
        }

        public void ShowRegisterView()
        {
            _windowViewModel.LeftSideBarViewModel = new RegisterViewModel(_windowViewModel, _userStateHandler, this);
        }

        public void ShowMenuView()
        {
            _windowViewModel.LeftSideBarViewModel = new MenuViewModel(_windowViewModel, _userStateHandler, this);

        }

        public void ShowGuestPanelView()
        {
            _windowViewModel.MainPanelViewModel = new GuestPanelViewModel(_windowViewModel);

        }

        public void ShowUserPanelView()
        {
            _windowViewModel.MainPanelViewModel = new UserPanelViewModel(_windowViewModel, _userStateHandler.CurrentUser);

        }

        public void ShowFriendsPanelView()
        {
            _windowViewModel.MainPanelViewModel = new FriendsPanelViewModel(_windowViewModel, _serverService, _userStateHandler.CurrentUser);

        }

        public void ShowChatsPanelView()
        {
            _windowViewModel.MainPanelViewModel = new ChatsPanelViewModel(_windowViewModel, _serverService, _userStateHandler.CurrentUser);
        }
    }
}
