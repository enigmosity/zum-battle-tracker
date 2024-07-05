using Microsoft.EntityFrameworkCore;

namespace ZumBattleTracker
{
	public interface IRepository {
		public Task RecordTournamentResults(List<PokemonModel> processedResults);
	}
	public class PokemonRepository : IRepository
	{
		public PokemonRepository(PokemonContext context) {
			_pokemonContext = context;
		}
		private readonly PokemonContext _pokemonContext;

		public async Task RecordTournamentResults(List<PokemonModel> processedResults)
		{
			foreach (var result in processedResults)
			{
				await RecordBattleResults(result);
			}
		}

		// this makes an interesting tradeoff
		// it will be slower to save changes for every pokemon individually, less performative
		// however, it will decrease the risk of cross contamination of changes being saved
		public async Task RecordBattleResults(PokemonModel pokemon)
		{
			
			if (await _pokemonContext.Pokemon.AnyAsync(p => p.Id == pokemon.Id))
			{
				var remotePokemon = await _pokemonContext.Pokemon.SingleAsync(p => p.Id == pokemon.Id);
				remotePokemon.Wins += pokemon.Wins;
				remotePokemon.Ties += pokemon.Ties;
				remotePokemon.Losses += pokemon.Losses;
			} else
			{
				await _pokemonContext.Pokemon.AddAsync(pokemon);
			}
			
			_pokemonContext.SaveChanges();
		}
	}
}
