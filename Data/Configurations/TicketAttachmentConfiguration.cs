using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class TicketAttachmentConfiguration : IEntityTypeConfiguration<TicketAttachment>
    {
        public void Configure(EntityTypeBuilder<TicketAttachment> builder)
        {
            builder.ToTable("TicketAttachments", "Support");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.FileName).HasMaxLength(255).IsRequired();
            builder.Property(a => a.FilePath).HasMaxLength(1000).IsRequired();
            builder.Property(a => a.ContentType).HasMaxLength(100).IsRequired();

            builder.HasOne(a => a.Ticket)
                .WithMany(t => t.Attachments)
                .HasForeignKey(a => a.TicketId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.TicketReply)
                .WithMany(r => r.Attachments)
                .HasForeignKey(a => a.TicketReplyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}