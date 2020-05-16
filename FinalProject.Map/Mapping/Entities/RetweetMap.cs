using FinalProject.Entities.Entity;
using FinalProject.Map.Mapping.Kernel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Map.Mapping.Entities
{
    public class RetweetMap : KernelMap<Retweet>
    {
        public override void Configure(EntityTypeBuilder<Retweet> builder)
        {

            builder.HasOne(x => x.User)
                .WithMany(x => x.Retweets)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Tweet)
                .WithMany(x => x.Retweets)
                .HasForeignKey(x => x.TweetId);
            base.Configure(builder);
        }
    }
}
