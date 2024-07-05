using System;
using ZumBattleTracker.Helpers;
using ZumBattleTracker.Services;
using Moq;
using Moq.Protected;
using ZumBattleTracker;


namespace UnitTests
{
	public class TournamentTests
	{
		[Test]
		public void GivenTournament_Expect56BattleResults()
		{
			// to start with we need 8 pokemon
			// their types don't matter because I'm not here to test the result, but to test the logic of the tournament
			var poke1 = PokemonBuilder("water", 2);

			var fighters = new List<Pokemon>() {
				poke1, PokemonBuilder("water", 2), PokemonBuilder("water", 2),
				PokemonBuilder("water", 2), PokemonBuilder("water", 2), PokemonBuilder("water", 2),
				PokemonBuilder("water", 2), PokemonBuilder("water", 2) };

			var mockBattleHelper = new Mock<IBattleHelper>();
			var mockPokemonRepositry = new Mock<PokemonRepository>();
			mockBattleHelper.Setup(m => m.Battle(It.IsAny<Pokemon>(), It.IsAny<Pokemon>())).Returns((poke1, poke1, 1));
			var tournamentHelper = new TournamentHelper(mockBattleHelper.Object, mockPokemonRepositry.Object);

			var results = tournamentHelper.BeginTournament(fighters);

			var expextedNumberOfBattles = 56;

			Assert.AreEqual(expextedNumberOfBattles, results.Count);
		}

		private Pokemon PokemonBuilder(string type, int base_exp)
		{
			var pokemon = new Pokemon();
			pokemon.Type = type;
			pokemon.BaseExperience = base_exp;
			return pokemon;
		}
	}
}
