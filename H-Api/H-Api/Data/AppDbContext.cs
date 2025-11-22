using Microsoft.EntityFrameworkCore;
using H_Api.Models;

namespace H_Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSet'ы с явным указанием названий таблиц
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Level> Levels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Явное указание названий таблиц
            modelBuilder.Entity<Employee>().ToTable("employee");
            modelBuilder.Entity<Guest>().ToTable("guest");
            modelBuilder.Entity<Room>().ToTable("room");
            modelBuilder.Entity<Reservation>().ToTable("reservation");
            modelBuilder.Entity<Role>().ToTable("role");
            modelBuilder.Entity<Level>().ToTable("level");

            // Настройка связей между таблицами

            // Employee -> Role
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Role)
                .WithMany(r => r.Employees)
                .HasForeignKey(e => e.EmployeeRole);

            // Room -> Level
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Level)
                .WithMany(l => l.Rooms)
                .HasForeignKey(r => r.RoomLevel);

            // Room -> Employee (ответственный)
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Employee)
                .WithMany(e => e.ResponsibleRooms)
                .HasForeignKey(r => r.ResponsibleEmployee);

            // Reservation -> Guest
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Guest)
                .WithMany(g => g.Reservations)
                .HasForeignKey(r => r.GuestID);

            // Reservation -> Room
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany(room => room.Reservations)
                .HasForeignKey(r => r.RoomID);

            // Reservation -> Employee (кто оформил бронь)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Employee)
                .WithMany(e => e.Reservations)
                .HasForeignKey(r => r.EmployeeID);
        }
    }
}