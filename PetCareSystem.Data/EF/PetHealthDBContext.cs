using Microsoft.EntityFrameworkCore;
using PetCareSystem.Data.Configurations;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.EF
{
    public class PetHealthDBContext : DbContext
    {
        public PetHealthDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Barn> Barns { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new PetConfig());
            modelBuilder.ApplyConfiguration(new StaffConfig());
            modelBuilder.ApplyConfiguration(new ServiceConfig());
            modelBuilder.ApplyConfiguration(new AdminConfig());
            modelBuilder.ApplyConfiguration(new DoctorConfig());
            modelBuilder.ApplyConfiguration(new BarnConfig());
            modelBuilder.ApplyConfiguration(new RecordConfig());
            modelBuilder.ApplyConfiguration(new BookingConfig());
            modelBuilder.ApplyConfiguration(new ManageServiceConfig());
            modelBuilder.ApplyConfiguration(new ManageRecordConfig());

            modelBuilder.Entity<Booking>().Property(t => t.Total).HasColumnType("decimal(18,4)");
            modelBuilder.Entity<Service>().Property(t => t.Price).HasColumnType("decimal(18,4)");
            modelBuilder.Entity<ManageRecord>().HasOne(mr => mr.Record).WithMany(r => r.ManageRecords).HasForeignKey(mr => mr.RecordId).OnDelete(DeleteBehavior.Restrict);
            //base.OnModelCreating(modelBuilder);
        }

        
    }
}
