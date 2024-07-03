using ZumBattleTracker.Services;

namespace ZumBattleTracker.Helpers
{
	public class TournamentHelper
	{
        private readonly IBattleHelper _battleHelper;
        public TournamentHelper(IBattleHelper battleHelper)
        {
            _battleHelper = battleHelper;
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

            // now we need to loop through and add the stuff to the thing.
            return results;
        }

        public void TournamentResults(List<(Pokemon, Pokemon, int)> results)
        {

        }
    }
}
