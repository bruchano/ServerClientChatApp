using Pie.Commands;
using Pie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Pie.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        private readonly ChatsPanelViewModel _viewModel;
        private readonly User _user;
        private readonly Chat _chat;

        public ICommand SelectChatCommand { get; private set; }

        public ObservableCollection<Message> Messages { get; }
        public Chat Chat
        {
            get
            {
                return _chat;
            }
        }

        public int ChatID
        {
            get
            {
                return _chat.ID;
            }
        }

        public string ChatName
        {
            get
            {
                if (_chat.ChatName != null) return _chat.ChatName;
                else return "Chat";
            }
        }

        public bool IsCurrentChat => this == _viewModel.CurrentChat;

        public ChatViewModel(ChatsPanelViewModel viewModel, User user, Chat chat)
        {
            _viewModel = viewModel;
            _user = user;
            _chat = chat;

            SelectChatCommand = new SelectChatCommand(this);

            Messages = new ObservableCollection<Message>();

            foreach (Message message in _chat.Messages)
            {
                Messages.Add(message);
            }
        }

        public void SelectChat()
        {
            if (_viewModel.CurrentChat != this)
            {
                _viewModel.CurrentChat = this;
                _viewModel.TbInputMessage = "";
            }
        }
    }
}
