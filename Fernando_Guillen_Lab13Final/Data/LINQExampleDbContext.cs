using Microsoft.EntityFrameworkCore;
using Fernando_Guillen_Lab13Final.Models;

namespace Fernando_Guillen_Lab13Final.Data
{
    public class LINQExampleDbContext : DbContext
    {
        public LINQExampleDbContext(DbContextOptions<LINQExampleDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Response> Responses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Tickets)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.Responses)
                .WithOne(r => r.Ticket)
                .HasForeignKey(r => r.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Responses)
                .WithOne(r => r.Responder)
                .HasForeignKey(r => r.ResponderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}