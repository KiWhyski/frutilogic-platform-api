using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Domain.Model.ValueObjects;
using KiWhisky.FrutiLogicPlatform.API.Shared.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiWhisky.FrutiLogicPlatform.API.AlertsAndNotifications.Infrastructure.Persistence.EFC.Configuration.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// This method applies the configuration for the Alert entity.
        /// </summary>
        /// <param name="builder">
        /// The model builder for the entity.
        /// </param>
        public static void ApplyAlertsAndNotificationsConfiguration(this ModelBuilder builder)
        {
            builder.Entity<Alert>(entity =>
            {
                entity.ToTable("alerts");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(a => a.Title)
                    .IsRequired()
                    .HasColumnName("title");

                entity.Property(a => a.Message)
                    .IsRequired()
                    .HasColumnName("message");

                entity.Property(a => a.Severity)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString(),
                        v => Enum.Parse<ESeverityTypes>(v, true))
                    .HasColumnName("severity");

                entity.Property(a => a.Type)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString(),
                        v => Enum.Parse<EAlertTypes>(v, true))
                    .HasColumnName("type");
        
                entity.Property(a => a.GeneratedAt)
                    .IsRequired()
                    .HasColumnName("generated_at");
        
                entity.Property(a => a.AccountId)
                    .HasConversion(
                        v => v.GetId,
                        v => new AccountId(v))
                    .HasColumnName("account_id");

                entity.Property(a => a.InventoryId)
                    .HasConversion(
                        v => v.GetId,
                        v => new InventoryId(v))
                    .HasColumnName("inventory_id");
            });
        }
    }
}
