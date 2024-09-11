using CustomDateTimeOffset.Attributes;
using CustomDateTimeOffset.Configuration;
using CustomDateTimeOffset.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace CustomDateTimeOffset.Extensions
{
    public static class CustomDateTimeModelBuilderExtensions
    {
        public static void ApplyCustomDateTimeConfiguration(this ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes().ToList();

            foreach (var entityType in entityTypes)
            {
                var entityClrType = entityType.ClrType;

                foreach (var property in entityClrType.GetProperties())
                {
                    var customDateTimeAttribute = property.GetCustomAttribute<CustomDateTimeAttribute>();
                    if (customDateTimeAttribute != null && property.PropertyType == typeof(CustomDateTime))
                    {
                        var parameter = Expression.Parameter(entityClrType, "e");
                        var propertyExpression = Expression.Property(parameter, property.Name);
                        var lambda = Expression.Lambda(propertyExpression, parameter);
                        (typeof(CustomDateTimeConfiguration)
                            .GetMethod(nameof(CustomDateTimeConfiguration.ConfigureCustomDateTime))
                            ?.MakeGenericMethod(entityClrType))?.Invoke(null, [modelBuilder, lambda]);
                    }
                }
            }
        }
    }
}
