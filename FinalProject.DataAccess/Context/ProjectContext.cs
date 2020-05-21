using FinalProject.Entities.Entity;
using FinalProject.Kernel.Entity.Concrete;
using FinalProject.Map.Mapping.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.DataAccess.Context
{
    public class ProjectContext : IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(GetConnectionString());
        }
        private static string GetConnectionString()
        {
            const string databaseName = "ProjectTwitter";

            return $"Server=localhost;" +
                   $"database={databaseName};" +
                   $"Trusted_Connection = True;" +
                   $"MultipleActiveResultSets = True;" +
                    $"pooling=true;";
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CommentMap());
            builder.ApplyConfiguration(new LikeMap());
            builder.ApplyConfiguration(new UserMap<AppUser>());
            builder.ApplyConfiguration(new TweetMap());
            builder.ApplyConfiguration(new RetweetMap());
            builder.ApplyConfiguration(new MessageMap());
            builder.ApplyConfiguration(new FollowMap());

            builder.Entity<ChatRoomUsers>(b => b.HasOne<AppUser>(navigationExpression: uf => uf.AppUser)
           .WithMany(navigationExpression: nf => nf.ChatRoomUsers)
           .HasForeignKey(nf => nf.UserId));

            builder.Entity<ChatRoomUsers>(b => b.HasOne<ChatRoom>(navigationExpression: uf => uf.ChatRoom)
           .WithMany(navigationExpression: nf => nf.ChatRoomUsers)
           .HasForeignKey(nf => nf.ChatRoomId));

            builder.Entity<ChatRoomUsers>(b => b.HasKey(x => new { x.UserId, x.ChatRoomId }));

            base.OnModelCreating(builder);
        }
        

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Retweet> Retweets { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatRoomUsers> ChatRoomUsers { get; set; }

        
    }
}
