using FinalProject.Entities.Entity;
using FinalProject.Map.Mapping.Kernel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Map.Mapping.Entities
{
    public class MessageMap : KernelMap<Message>
    {
        public override void Configure(EntityTypeBuilder<Message> builder)
        {

            builder.Property(x => x.Content).IsRequired(false);

            builder.HasOne(x => x.SenderUser)
                .WithMany(x => x.Senders)
                .HasForeignKey(x => x.SenderId);

            builder.HasOne(x => x.RecipientUser)
                .WithMany(x => x.Recipients)
                .HasForeignKey(x => x.RecipientId);

            base.Configure(builder);
        }
    }
}
