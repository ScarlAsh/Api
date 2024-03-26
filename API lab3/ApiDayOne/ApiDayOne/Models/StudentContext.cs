﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiDayOne.Models;

public partial class StudentContext : IdentityDbContext<ApplicationUser>
{
    public StudentContext(DbContextOptions<StudentContext> options): base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Department> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("students");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Image)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength();
        });
		modelBuilder.Entity<ApplicationUser>().HasKey(u => u.Id);
		modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);
		modelBuilder.Entity<IdentityUserClaim<string>>().HasKey(uc => uc.Id);
		modelBuilder.Entity<IdentityUserRole<string>>().HasKey(ur => new { ur.UserId, ur.RoleId });
		modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
		modelBuilder.Entity<IdentityUserToken<string>>().HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });

		OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}