namespace SimpleChat.Messaging.Database.Ef.EntityTypeConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using SimpleChat.Messaging.Entities;

    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(message => message.Id);
            builder.Property(message => message.UserId).IsRequired();

            builder
                .HasOne(message => message.User)
                .WithMany(user => user.Messages)
                .HasForeignKey(message => message.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
