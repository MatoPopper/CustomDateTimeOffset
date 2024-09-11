using CustomDateTimeOffset.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CustomDateTimeOffset.Configuration
{
    public static class CustomDateTimeConfiguration
    {
        // Generic method for CustomDateTime entity configuration
        public static void ConfigureCustomDateTime<TEntity>(ModelBuilder modelBuilder, Expression<Func<TEntity, CustomDateTime>> navigationExpression)
            where TEntity : class
        {
            modelBuilder.Entity<TEntity>().OwnsOne(navigationExpression, builder =>
            {
                builder.Property(p => p.DateTime)
                    .HasColumnName($"{GetPropertyName(navigationExpression)}DateTime")
                    .HasColumnType("timestamp")
                    .HasConversion(
                        v => DateTime.SpecifyKind(v, DateTimeKind.Unspecified),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

                builder.Property(p => p.Offset)
                    .HasColumnName($"{GetPropertyName(navigationExpression)}Offset")
                    .HasColumnType("smallint");
            });
        }

        // Helper for get  name 
        private static string GetPropertyName<TEntity>(Expression<Func<TEntity, CustomDateTime>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }

            throw new InvalidOperationException("Invalid expression for property.");
        }
    }
}
