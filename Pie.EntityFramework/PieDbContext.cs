using Microsoft.EntityFrameworkCore;
using Pie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pie.EntityFramework
{
    public class PieDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        public DbSet<Message> Messages { get; set; }

        public PieDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany<FriendRequest>(user => user.FriendRequests)
                .WithOne(request => request.Receiver);

            modelBuilder.Entity<Friendship>()
                .HasOne(friendship => friendship.Inviter)
                .WithMany(user => user.InvitedFriendships)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Friendship>()
                .HasOne(friendship => friendship.Accepter)
                .WithMany(user => user.AcceptedFriendships)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserChat>()
                .HasOne(userChat => userChat.User)
                .WithMany(user => user.Chats)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserChat>()
                .HasOne(userChat => userChat.Chat)
                .WithMany(user => user.Members)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Chat>()
                .HasMany(chat => chat.Messages)
                .WithOne(message => message.Chat);

            base.OnModelCreating(modelBuilder);
        }
        
    }
}
