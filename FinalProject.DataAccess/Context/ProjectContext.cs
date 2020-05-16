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
        private IHttpContextAccessor _accessor;

        public ProjectContext(DbContextOptions<ProjectContext> options, IHttpContextAccessor accessor) : base(options)
        {
            _accessor = accessor;

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

            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(GetConnectionString());
        }
        private static string GetConnectionString()
        {
            const string databaseName = "TwitterBlog";


            return $"Server=localhost;" +
                   $"database={databaseName};" +
                   $"Trusted_Connection = True;" +
                   $"MultipleActiveResultSets = True;" +
                    $"pooling=true;";
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Retweet> Retweets { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Image> Images { get; set; }

        public override int SaveChanges()
        {
            var modifiedEntites = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Added).ToList();
           
            string compterName = Environment.MachineName;
            var ipAddress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            DateTime date = DateTime.Now;
            
            foreach (var item in modifiedEntites)
            {
                KernelEntity entity = item.Entity as KernelEntity;
               
                
              if (item != null)
                {
                    switch (item.State)
                    {
                        case EntityState.Modified:
                            
                            entity.Status = Kernel.Enum.Status.Modified;
                            entity.ModifiedComputerName = compterName;
                            entity.ModifiedIP = ipAddress;
                            entity.ModifiedDate = date;
                         
                            break;
                        case EntityState.Added:
                        
                            entity.Status = Kernel.Enum.Status.Active;
                            entity.CreatedComputerName = compterName;
                            entity.CreatedIP = ipAddress;
                            entity.CreatedDate = date;
                          
                            break;
                        case EntityState.Deleted:
                             entity.Status = Kernel.Enum.Status.Passive;
                            entity.CreatedComputerName = compterName;
                            entity.CreatedIP = ipAddress;
                            entity.CreatedDate = date;
                            break;

                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
