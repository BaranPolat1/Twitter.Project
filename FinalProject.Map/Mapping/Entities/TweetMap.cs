using FinalProject.Entities.Entity;
using FinalProject.Map.Mapping.Kernel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Map.Mapping.Entities
{
    public class TweetMap : KernelMap<Tweet>
    {
        public override void Configure(EntityTypeBuilder<Tweet> builder)
        {

            builder.Property(x => x.Content).HasMaxLength(140).IsRequired(false);
            builder.Property(x => x.ImagePath).IsRequired(false);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Tweets)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.Tweet)
                .HasForeignKey(x => x.TweetId);

            builder.HasMany(x => x.Likes)
                .WithOne(x => x.Tweet)
                .HasForeignKey(x => x.TweetId);

            builder.HasMany(x => x.Retweets)
                .WithOne(x => x.Tweet)
                .HasForeignKey(x => x.TweetId);

            base.Configure(builder);
        }
    }
}
