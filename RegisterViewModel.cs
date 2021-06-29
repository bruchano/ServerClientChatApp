using Pie.Commands;
using Pie.Commands.ViewCommand;
using Pie.EntityFramework.Services.IAuthenticationService;
using Pie.EntityFramework.Services.UserStateHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pie.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly IUserStateHandler _userStateHandler;
        public ViewCommands viewCommands { get; private set; }
        public ICommand UsernameGotFocusCommand { get; private set; }
        public ICommand PasswordGotFocusCommand { get; private set; }
        public ICommand ConfirmPasswordGotFocusCommand { get; private set; }

        private string _tbEmail;
        private string _tbUsername;
        private string _pbPassword;
        private string _pbConfirmPassword;

        public string TbEmail
        {
            get
            {
                return _tbEmail;
            }
            set
            {
                _tbEmail = value;
                OnPropertyChanged(nameof(TbEmail));
            }
        }

        public string TbUsername
        {
            get
            {
                return _tbUsername;
            }
            set
            {
                _tbUsername = value;
                OnPropertyChanged(nameof(TbUsername));
            }
        }

        public string PbPassword
        {
            get
            {
                return _pbPassword;
            }
            set
            {
                _pbPassword = value;
                OnPropertyChanged(nameof(PbPassword));
            }
        }

        public string PbConfirmPassword
        {
            get
            {
                return _pbConfirmPassword;
            }
            set
            {
                _pbConfirmPassword = value;
                OnPropertyChanged(nameof(PbConfirmPassword));
            }
        }
        

        public string pbPassword { get; set; }
        public string pbConfirmPassword { get; set; }

        public RegisterViewModel(WindowViewModel m, IUserStateHandler userStateHandler, ViewCommands viewCommands)
        {
            windowViewModel = m;
            _userStateHandler = userStateHandler;
            this.viewCommands = viewCommands;

            UsernameGotFocusCommand = new VoidCommand(UsernameGotFocus);
        }

        public void UsernameGotFocus()
        {
            if (TbUsername == "username") TbUsername = "";
        }

        public async Task Register(string email, string username, string password, string confirmPassword)
        {
            await _userStateHandler.Register(email, username, password, confirmPassword);
        }
    }
}
