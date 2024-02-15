﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AzureOpenAIChat.Models;

public partial class AzureOpenAIChatSQLVectorContext : DbContext
{
    public AzureOpenAIChatSQLVectorContext(DbContextOptions<AzureOpenAIChatSQLVectorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Article { get; set; }

    public virtual DbSet<ArticleDetail> ArticleDetail { get; set; }

    public virtual DbSet<ArticleVectorData> ArticleVectorData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.Property(e => e.ArticleName)
                .IsRequired()
                .HasMaxLength(250);
        });

        modelBuilder.Entity<ArticleDetail>(entity =>
        {
            entity.HasOne(d => d.Article).WithMany(p => p.ArticleDetail)
                .HasForeignKey(d => d.ArticleId)
                .HasConstraintName("FK_ArticleDetail_Article");
        });

        modelBuilder.Entity<ArticleVectorData>(entity =>
        {
            entity.Property(e => e.VectorValue).HasColumnName("vector_value");
            entity.Property(e => e.VectorValueId).HasColumnName("vector_value_id");

            entity.HasOne(d => d.ArticleDetail).WithMany(p => p.ArticleVectorData)
                .HasForeignKey(d => d.ArticleDetailId)
                .HasConstraintName("FK_ArticleVectorData_ArticleDetail");
        });

        OnModelCreatingGeneratedFunctions(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}