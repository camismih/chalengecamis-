using Microsoft.EntityFrameworkCore;

namespace Camila.Api.Data;

public class CamilaContext : DbContext
{
    public CamilaContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; } = null!;
    public DbSet<Transferencia> Transferencias { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(c =>
        {
            c.HasKey(c => c.Id);

            c.HasIndex(c => c.Nome)
            .IsUnique();
        });
        modelBuilder.Entity<Transferencia>(t =>
        {
            t.Property<Guid>("Id");

            t.HasKey("Id");

            t.HasOne(c => c.ContaOrigem)
            .WithMany();

            t.HasOne(c => c.ContaDestino)
            .WithMany();
        });
    }
}