using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class TicketReplyConfiguration : IEntityTypeConfiguration<TicketReply>
    {
        public void Configure(EntityTypeBuilder<TicketReply> builder)
        {
            builder.ToTable("TicketReplies", "Support");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Message).HasMaxLength(4000).IsRequired();
            builder.Property(r => r.IsInternalNote).HasDefaultValue(false);

            builder.HasOne(r => r.Ticket)
                .WithMany(t => t.Replies)
                .HasForeignKey(r => r.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}