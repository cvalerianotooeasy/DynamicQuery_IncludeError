using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace db
{
   public  class DbTestContext : DbContext
    {
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<AttributeValue> AttributeValues { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbTestContext(DbContextOptions<DbTestContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.id);
                entity.Property(e => e.name).HasMaxLength(250);
                entity.Property(e => e.sku).HasMaxLength(250);
                entity.HasMany(e => e.AttributeValues).WithOne(e => e.Product).HasForeignKey(x => x.productId);
                 
            });

            modelBuilder.Entity<AttributeValue>(entity =>
            {
                entity.HasKey(x => x.id);
                entity.Property(e => e.value).HasMaxLength(250);
                
                entity.HasOne(e => e.Product).WithMany(e => e.AttributeValues).HasForeignKey(x => x.productId);
                entity.HasOne(e => e.Attribute).WithMany(e => e.Values).HasForeignKey(x => x.attributeId);

            });

            modelBuilder.Entity<db.Attribute>(entity =>
            {
                entity.HasKey(x => x.id);
                entity.Property(e => e.name).HasMaxLength(250);
                
                entity.HasMany(e => e.Values).WithOne(e => e.Attribute).HasForeignKey(x => x.attributeId);

            });

            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
