using OsDsII.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace OsDsII.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<ServiceOrder> ServiceOrders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .ToTable("customer");

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Customer>()
                .Property(c => c.Name)
                .HasMaxLength(60);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Email)
                .HasMaxLength(255);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Phone)
                .HasMaxLength(20);

            modelBuilder.Entity<ServiceOrder>()
                .ToTable("service_order");

            modelBuilder.Entity<ServiceOrder>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<ServiceOrder>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ServiceOrder>()
                .Property(s => s.Description)
                .IsRequired();

            modelBuilder.Entity<ServiceOrder>()
                .Property(s => s.Price)
                .IsRequired();

            modelBuilder.Entity<ServiceOrder>()
                .Property(s => s.Status)
                .HasConversion(
                new EnumToStringConverter<StatusServiceOrder>());

            modelBuilder.Entity<ServiceOrder>()
                .Property(s => s.OpeningDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ServiceOrder>()
                .Property(s => s.FinishDate)
                .HasDefaultValue(null);

            modelBuilder.Entity<Comment>()
                .ToTable("coomments");

            modelBuilder.Entity<Comment>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Comment>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Comment>()
                .Property(c => c.Description)
                .HasColumnType("text")
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .Property(c => c.SendDate)
                .HasDefaultValue(DateTimeOffset.Now);

            modelBuilder.Entity<ServiceOrder>()
                .HasOne(s => s.Customer)
                .WithMany(e => e.ServiceOrders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ServiceOrder)
                .WithMany(e => e.Comments)
                .HasForeignKey(e => e.ServiceOrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}