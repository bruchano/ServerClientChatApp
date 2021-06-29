using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pie.Domain.Models
{
    public enum UserType{
        User,
        Admin,
        Superuser
    }

    public class User : DomainObject
    {
        public User()
        {
            DateJoined = DateTime.Now;
        }

        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string HashedPassword { get; set; }
        [Required]
        public UserType Type { get; set; }
        [Required]
        public DateTime DateJoined { get; set; }
        public virtual ICollection<Friendship> InvitedFriendships { get; set; }
        public virtual ICollection<Friendship> AcceptedFriendships { get; set; }
        public virtual ICollection<FriendRequest> FriendRequests { get; set; }
        public virtual ICollection<UserChat> Chats { get; set; }

    }
}
