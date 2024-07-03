using Microsoft.EntityFrameworkCore;

namespace ZumBattleTracker
{
	public class PokemonContext : DbContext
	{
		public DbSet<PokemonModel> Pokemon { get; set; }
	
		public string DbPath { get; }

		public PokemonContext(DbContextOptions options) : base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<PokemonModel>().ToTable("Pokemon");
		}
	}
}
