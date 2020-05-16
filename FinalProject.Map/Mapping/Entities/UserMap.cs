using FinalProject.Entities.Entity;
using FinalProject.Map.Mapping.Kernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Map.Mapping.Entities
{
    public class UserMap<T> : IEntityTypeConfiguration<AppUser> where T : AppUser
    {
        public virtual void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Status).IsRequired(true);
            builder.Property(x => x.CreatedDate).IsRequired(false);
            builder.Property(x => x.CreatedComputerName).IsRequired(false);
            builder.Property(x => x.CreatedIP).IsRequired(false);

            builder.Property(x => x.ModifiedDate).IsRequired(false);
            builder.Property(x => x.ModifiedComputerName).IsRequired(false);
            builder.Property(x => x.ModifiedIP).IsRequired(false);
            builder.Property(x => x.Bio).HasMaxLength(150).IsRequired(false);

            builder.HasMany(x => x.Tweets)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Likes)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Retweets)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);


            builder.HasMany(x => x.Follower)
                 .WithOne(x => x.FollowerUser)
                 .HasForeignKey(x => x.FollowerId);

            builder.HasMany(x => x.Followed)
                .WithOne(x => x.FollowedUser)
                .HasForeignKey(x => x.FollowedId);

            builder.HasMany(x => x.Senders)
                .WithOne(x => x.SenderUser)
                .HasForeignKey(x => x.SenderId);

            builder.HasMany(x => x.Recipients)
                .WithOne(x => x.RecipientUser)
                .HasForeignKey(x => x.RecipientId);
            
            

        }
    }
    
} 
