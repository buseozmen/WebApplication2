using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models;

public partial class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Wktobj> Wktobjs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Wktobj>(entity =>
        {
            entity.HasKey(e => e.ObjectId).HasName("wktobj_pkey");

            entity.Property(e => e.ObjectId).UseIdentityAlwaysColumn();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
