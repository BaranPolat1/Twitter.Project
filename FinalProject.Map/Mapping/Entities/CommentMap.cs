using FinalProject.Entities.Entity;
using FinalProject.Map.Mapping.Kernel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Map.Mapping.Entities
{
    public class CommentMap : KernelMap<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {

            builder.Property(x => x.Content).HasMaxLength(150).IsRequired(false);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Tweet)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.TweetId);

            base.Configure(builder);
        }
    }
}
