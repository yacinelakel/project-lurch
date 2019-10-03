using Lurch.Karma.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lurch.Karma.Data
{
    public class KarmaContext : DbContext
    {
        public KarmaContext(DbContextOptions<KarmaContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersEntityConfiguration());
        }

        public virtual DbSet<User> Users { get; set; }
    }

    public class UsersEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ForSqlServerUseSequenceHiLo("UsersSeq");

            builder.OwnsOne(u => u.Karma);
        }
    }
}
