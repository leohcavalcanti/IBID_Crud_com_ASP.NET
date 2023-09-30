using IBIDCrud.Entities;
using Microsoft.EntityFrameworkCore;

namespace IBIDCrud.Persistence
{
    public class IBIDEventsDbContext : DbContext 
    {
        public DbSet<IBIDEvent> IBIDEvent {  get; set; }
        public DbSet<IBIDEventSpeaker> IBIDEventSpeakers { get; set; }

        public IBIDEventsDbContext(DbContextOptions<IBIDEventsDbContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IBIDEvent>(e =>
            {
                e.HasKey(ie => ie.Id);

                e.Property(ie => ie.Title).IsRequired(false);

                e.Property(ie => ie.Description)
                    .HasMaxLength(200)
                    .HasColumnType("varchar(200)");

                e.Property(ie => ie.StartDate)
                    .HasColumnName("Start_Date");

                e.Property(ie => ie.EndDate)
                    .HasColumnName("End_Date");

                e.HasMany(ie => ie.Speakers)
                    .WithOne()
                    .HasForeignKey(s => s.IBIDEventId);
            });

            builder.Entity<IBIDEventSpeaker>(e =>
            {
                e.HasKey(ie => ie.Id);
            });
        }
    }
}
