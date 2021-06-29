using Pie.Commands;
using Pie.Domain.Models;
using Pie.EntityFramework.Services.ServerServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pie.ViewModels
{
    public class ChatsPanelViewModel : BaseViewModel
    {

        private readonly ServerService _serverService;
        private readonly User _user;
        public ICommand SendMessageCommand { get; private set; }

        private ChatViewModel _currentChat;
        public ChatViewModel CurrentChat
        {
            get
            {
                return _currentChat;
            }
            set
            {
                _currentChat = value;
                OnPropertyChanged(nameof(CurrentChat));
            }
        }
        private string _tbInputMessage;
        public string TbInputMessage
        {
            get
            {
                return _tbInputMessage;
            }
            set
            {
                _tbInputMessage = value;
                OnPropertyChanged(nameof(TbInputMessage));
            }
        }
        public ObservableCollection<ChatViewModel> Chats { get; }

        public ChatsPanelViewModel(WindowViewModel m, ServerService serverService, User user)
        {
            windowViewModel = m;
            _serverService = serverService;
            _user = user;

            SendMessageCommand = new SendMessageCommand(this);

            Chats = new ObservableCollection<ChatViewModel>();

            foreach (UserChat chat in _user.Chats)
            {
                Chats.Add(new ChatViewModel(this, _user, chat.Chat));
            }
        }

        public async Task SendMessage()
        {
            await _serverService.SendMessage(_user.Username, CurrentChat.ChatID, TbInputMessage);
            CurrentChat.Messages.Add(new Message()
            {
                Sender = _user,
                Chat = CurrentChat.Chat,
                Text = TbInputMessage,
            });
            TbInputMessage = "";
        }

    }
}
