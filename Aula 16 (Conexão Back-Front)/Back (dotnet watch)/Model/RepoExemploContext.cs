using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace webBack.Model;

public partial class RepoExemploContext : DbContext
{
    public RepoExemploContext()
    {
    }

    public RepoExemploContext(DbContextOptions<RepoExemploContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Mensagem> Mensagems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=CT-C-00186\\SQLEXPRESS;Initial Catalog=repoExemplo;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mensagem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mensagem__3214EC27D95EFE25");

            entity.ToTable("Mensagem");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Horario).HasColumnType("datetime");
            entity.Property(e => e.Texto).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
