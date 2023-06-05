using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace desafio.Model;

public partial class DesafioGitContext : DbContext
{
    public DesafioGitContext()
    {
    }

    public DesafioGitContext(DbContextOptions<DesafioGitContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Repositorio> Repositorios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CT-C-0013J\\SQLEXPRESS01;Initial Catalog=desafioGit;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Repositorio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reposito__3214EC27A9FC555D");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.LastPull).HasColumnType("datetime");
            entity.Property(e => e.Nome)
                .IsRequired()
                .IsUnicode(false);
            entity.Property(e => e.RepoPath)
                .IsRequired()
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
