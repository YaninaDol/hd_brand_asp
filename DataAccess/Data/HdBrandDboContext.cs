using System;
using System.Collections.Generic;
using hd_brand_asp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hd_brand_asp.Data;

public partial class HdBrandDboContext : IdentityDbContext<IdentityUser>
{
    public HdBrandDboContext()
    {
    }

    public HdBrandDboContext(DbContextOptions<HdBrandDboContext> options)
        : base(options)
    {
    }
    public virtual DbSet<SeasonShoeType> SeasonShoeTypes { get; set; }
    public virtual DbSet<Material> Materials { get; set; }
    public virtual DbSet<ShoeType> ShoeTypes { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Productsize> Productsizes { get; set; }

    public virtual DbSet<Season> Seasons { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pkey");

            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_pkey");

            entity.ToTable("product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ShoeType)
                .HasMaxLength(100)
                .HasColumnName("shoetype");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Seasonid).HasColumnName("seasonid");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.Categoryid)
                .HasConstraintName("product_categoryid_fkey");

            entity.HasOne(d => d.Season).WithMany(p => p.Products)
                .HasForeignKey(d => d.Seasonid)
                .HasConstraintName("product_seasonid_fkey");
        });

        modelBuilder.Entity<Productsize>(entity =>
        {
            entity.HasKey(e => new { e.Productid, e.Sizeid }).HasName("productsize_pkey");

            entity.ToTable("productsize");

            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Sizeid).HasColumnName("sizeid");

            entity.HasOne(d => d.Product).WithMany(p => p.Productsizes)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("productsize_productid_fkey");
        });
        modelBuilder.Entity<Material>(entity =>
        {
            entity.ToTable("Materials"); 
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            
        });

        modelBuilder.Entity<ShoeType>(entity =>
        {
            entity.ToTable("ShoeTypes"); 
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
           
        });
        modelBuilder.Entity<Season>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("season_pkey");

            entity.ToTable("season");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("size_pkey");

            entity.ToTable("size");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Value).HasColumnName("value");
        });
        modelBuilder.Entity<IdentityUser>()
           .ToTable("AspNetUsers")
           .HasKey(u => u.Id);

        modelBuilder.Entity<IdentityRole>()
            .ToTable("AspNetRoles")
            .HasKey(r => r.Id);

        modelBuilder.Entity<IdentityUserRole<string>>()
            .ToTable("AspNetUserRoles")
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<IdentityUserClaim<string>>()
            .ToTable("AspNetUserClaims")
            .HasKey(uc => uc.Id);

        modelBuilder.Entity<IdentityUserLogin<string>>()
            .ToTable("AspNetUserLogins")
            .HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });

        modelBuilder.Entity<IdentityUserToken<string>>()
            .ToTable("AspNetUserTokens")
            .HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });

        modelBuilder.Entity<SeasonShoeType>()
            .HasKey(sst => sst.Id);

        


        OnModelCreatingPartial(modelBuilder);
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
