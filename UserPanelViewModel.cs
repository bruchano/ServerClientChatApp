using Pie.Domain.Models;
using Pie.EntityFramework.Services.IAuthenticationService;

namespace Pie.ViewModels
{
    public class UserPanelViewModel : BaseViewModel
    {
        public User CurrentUser { get; private set; }
        public UserPanelViewModel(WindowViewModel m, User currentUser)
        {
            windowViewModel = m;
            CurrentUser = currentUser;
        }
    }
}
