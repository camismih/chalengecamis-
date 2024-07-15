using Microsoft.EntityFrameworkCore;

namespace Camila.Api.Data;

/// <summary>
/// Define um contexto de banco de dados para a aplicação
/// </summary>
public class CamilaContext : DbContext
{
    /// <summary>
    /// Define um construtor que recebe um DbContextOptions que definirá a conexão com o banco de dados
    /// </summary>
    /// <param name="options"></param>
    public CamilaContext(DbContextOptions options) : base(options)
    {
    }

    /// <summary>
    /// Define um DbSet que representa a lista de clientes/contas
    /// </summary>
    public DbSet<Cliente> Clientes { get; set; } = null!;
    
    /// <summary>
    /// Define um DbSet que representa a lista de transferências
    /// </summary>
    public DbSet<Transferencia> Transferencias { get; set; } = null!;

    /// <summary>
    /// Sobreescrita do método OnModelCreating para definir as configurações de mapeamento das entidades
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(c =>
        {
            c.HasKey(c => c.Id);

            c.HasAlternateKey(c => c.NumeroConta);            
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