using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate.TextTokens;
using PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate;
using PansyDev.Shetter.Infrastructure.Data.Serialization;
using PansyDev.Shetter.Infrastructure.Services;
using Volo.Abp.EntityFrameworkCore;

namespace PansyDev.Shetter.Infrastructure.Data
{
    public class ShetterDbContext : AbpDbContext<ShetterDbContext>
    {
        public DbSet<PostAuthor> PostAuthors => Set<PostAuthor>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<PostVersion> PostVersions => Set<PostVersion>();

        public ShetterDbContext(DbContextOptions<ShetterDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var imageDeserializer = LazyServiceProvider?.LazyGetRequiredService<PostImageDeserializer>();

            var textTokensSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Objects,
                MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
                ContractResolver = new PrivateSetterContractResolver()
            };

            var imagesSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new IgnorePropertyContractResolver("Url")
            };

            modelBuilder.Entity<PostVersion>(builder =>
            {
                builder.Property(x => x.PostId);

                builder.HasOne(x => x.Post)
                    .WithMany(x => x.PreviousVersions)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.Property(x => x.TextTokens).HasColumnType("jsonb").HasConversion(
                    v => JsonConvert.SerializeObject(v, textTokensSettings),
                    v => JsonConvert.DeserializeObject<IReadOnlyList<TextToken>>(v, textTokensSettings)!);

                builder.Property(x => x.Images).HasColumnType("jsonb").HasConversion(
                    v => JsonConvert.SerializeObject(v, imagesSettings),
                    v => imageDeserializer!.Deserialize(v));
            });

            modelBuilder.Entity<Post>(builder =>
            {
                builder.HasOne(x => x.Author)
                    .WithMany()
                    .HasForeignKey(x => x.AuthorId);

                builder.HasOne(x => x.CurrentVersion)
                    .WithMany()
                    .HasForeignKey(x => x.CurrentVersionId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(x => x.PreviousVersions)
                    .WithOne()
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}