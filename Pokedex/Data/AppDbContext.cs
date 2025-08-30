using System.Data.Common;
using System.Formats.Tar;
using Microsoft.EntityFrameworkCore;
using Pokedex.Models;

namespace Pokedex.Data;

 public class AppDbContext : DbContext
{
   public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
   {
   }

   public DbSet<Genero> Generos { get; set; }

   public DbSet<Pokemon> Pokemons { get; set; }

   public DbSet<PokemonTipo> PokemonTipos { get; set; } 
   public DbSet<Regiao> Regiaos { get; set; } 

   public DbSet<Tipo> Tipos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Muitos para Muitos do Pokemon Tipo
        //configuração da chave primaria
        builder.Entity<PokemonTipo>().HasKey(
            pt => new { pt.PokemonNumero, pt.TipoId}           
        );

        //configuração da chave estrangeira - PokemonTipo => Pokemon
        builder.Entity<PokemonTipo>()
             .HasOne(pt => pt.Pokemon)
             .WithMany(p => p.Tipos)
             .HasForeignKey(pt => pt.PokemonNumero); 

            //chaveEstrangeira - PokemonTipo -> Tipo
             builder.Entity<PokemonTipo>()
             .HasOne(pt => pt.Tipo)
             .WithMany(t => t.Pokemons)
             .HasForeignKey(pt => pt.TipoId);




        #endregion
    }
}
