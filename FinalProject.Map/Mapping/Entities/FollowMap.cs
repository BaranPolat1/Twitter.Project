using FinalProject.Entities.Entity;
using FinalProject.Map.Mapping.Kernel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Map.Mapping.Entities
{
    public class FollowMap : KernelMap<Follow>
    {
        public override void Configure(EntityTypeBuilder<Follow> builder)
        {


            builder.HasOne(x => x.FollowedUser)
                .WithMany(x => x.Followed)
                .HasForeignKey(x => x.FollowedId);

            builder.HasOne(x => x.FollowerUser)
                .WithMany(x => x.Follower)
                .HasForeignKey(x => x.FollowerId);
            base.Configure(builder);
        }
    }
}
