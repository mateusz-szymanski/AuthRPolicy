using AuthRPolicy.Sample.Domain.DocumentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace AuthRPolicy.Sample.Infrastructure.EntityFramework
{
    public class SampleDbContext : DbContext
    {
        public DbSet<Document> Document { get; set; }

        public SampleDbContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.ToUniversalTime(),
                v => new DateTime(v.Ticks, DateTimeKind.Utc)
            );

            modelBuilder.Entity<Document>(builder =>
            {
                builder.HasKey(d => d.Id);

                builder.OwnsOne(d => d.DocumentId, documentIdBuilder =>
                {
                    documentIdBuilder
                        .Property(di => di.Id)
                        .HasColumnName("Id");

                    documentIdBuilder.HasIndex(di => di.Id);
                });

                builder.Property(d => d.Title);
                builder.Property(d => d.Owner);
                builder.Property(d => d.Reviewer);
                builder.Property(d => d.ReviewedOn).HasConversion(dateTimeConverter);
            });
        }
    }
}
