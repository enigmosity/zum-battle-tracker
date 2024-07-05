using ZumBattleTracker.Services;

namespace ZumBattleTracker.Helpers
{
	public class TournamentHelper
	{
        private readonly IBattleHelper _battleHelper;
        private readonly PokemonRepository _pokemonRepository;
        public TournamentHelper(IBattleHelper battleHelper, PokemonRepository pokemonRepository)
        {
            _battleHelper = battleHelper;
			_pokemonRepository = pokemonRepository;
        }

        public List<(Pokemon, Pokemon, int)> BeginTournament(List<Pokemon> pokemons)
        {
            var pokemonStillToFight = new List<Pokemon>(pokemons);
            var results = new List<(Pokemon, Pokemon, int)> ();
            foreach (var pokemon in pokemons)
            {
                foreach (var nextPokemon in pokemons) {
                    if (nextPokemon != pokemon)
                    {
                        results.Add(_battleHelper.Battle(pokemon, nextPokemon));

                    }
                }
                pokemonStillToFight.Remove(pokemon);
            }

            return results;
        }

        public Dictionary<int, PokemonModel> TournamentResultProcesser(List<(Pokemon, Pokemon, int)> results)
        {
            var processedResults = new Dictionary<int, PokemonModel>();

			foreach (var result in results)
            {
				PokemonModel pokemon1;
				PokemonModel pokemon2;

				// process the first fighter
				if (processedResults.ContainsKey(result.Item1.Id))
				{
					processedResults.TryGetValue(result.Item1.Id, out pokemon1);
				}
				else
				{
					pokemon1 = new PokemonModel(result.Item1);
				}
				pokemon1 = DetermineResult(pokemon1, result.Item3, 0);

				// process the second fighter
				if (processedResults.ContainsKey(result.Item2.Id))
				{
					processedResults.TryGetValue(result.Item2.Id, out pokemon2);
				}
				else
				{
					pokemon2 = new PokemonModel(result.Item2);
				}
				pokemon2 = DetermineResult(pokemon2, result.Item3, 1);

				// replace them or add them to the dictionary to keep track of values.
				processedResults[pokemon1.Id] = pokemon1;
				processedResults[pokemon2.Id] = pokemon2;
			}

			return processedResults;
		}

		public async Task RecordTournamentResults(Dictionary<int, PokemonModel> processedResults)
		{
			await _pokemonRepository.RecordTournamentResults(processedResults.Values.ToList());
		}

		private PokemonModel DetermineResult(PokemonModel pokemon, int result, int position)
        {

			if (result == position)
			{
				pokemon.Wins += 1;
			}
			else if (result == -1)
			{
				pokemon.Ties += 1;
			}
			else
			{
				pokemon.Losses += 1;
			}

            return pokemon;
		}
    }
}
