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
		public void GivenTournament_Expect28BattleResults()
		{
			// to start with we need 8 pokemon
			// their types don't matter because I'm not here to test the result, but to test the logic of the tournament
			var poke1 = PokemonBuilder("water", 2);

			var fighters = new List<Pokemon>() {
				poke1, PokemonBuilder("water", 2), PokemonBuilder("water", 2),
				PokemonBuilder("water", 2), PokemonBuilder("water", 2), PokemonBuilder("water", 2),
				PokemonBuilder("water", 2), PokemonBuilder("water", 2) };

			var mockBattleHelper = new Mock<IBattleHelper>();
			var mockPokemonRepository = new Mock<IRepository>();
			mockBattleHelper.Setup(m => m.Battle(It.IsAny<Pokemon>(), It.IsAny<Pokemon>())).Returns((poke1, poke1, 1));
			var tournamentHelper = new TournamentHelper(mockBattleHelper.Object, mockPokemonRepository.Object);

			var results = tournamentHelper.BeginTournament(fighters);

			var expextedNumberOfBattles = 28;

			Assert.AreEqual(expextedNumberOfBattles, results.Count);
		}

		[Test]
		public void GivenTournamentWith2Participants_Expect1BattleResults()
		{
			// to start with we need 8 pokemon
			// their types don't matter because I'm not here to test the result, but to test the logic of the tournament
			var poke1 = PokemonBuilder("water", 2);
			poke1.Id=1;
			var poke2 = PokemonBuilder("fire", 2);
			poke2.Id=2;

			var fighters = new List<Pokemon>() { poke1, poke2 };

			var mockBattleHelper = new Mock<IBattleHelper>();
			var mockPokemonRepository = new Mock<IRepository>();
			mockBattleHelper.Setup(m => m.Battle(It.IsAny<Pokemon>(), It.IsAny<Pokemon>())).Returns((poke1, poke1, 1));
			var tournamentHelper = new TournamentHelper(mockBattleHelper.Object, mockPokemonRepository.Object);

			var results = tournamentHelper.BeginTournament(fighters);

			var expectedNumberOfBattles = 1;

			Assert.AreEqual(expectedNumberOfBattles, results.Count);
		}

		// realistically this could follow a really good builder pattern - this isn't quite it because it's all done in the constructor. It's a future iteration.
		private Pokemon PokemonBuilder(string type, int base_exp)
		{
			var pokemon = new Pokemon();
			pokemon.Type = type;
			pokemon.BaseExperience = base_exp;
			return pokemon;
		}

		// I feel like the complexity of this test setup says the implementation is too complex
		private List<(Pokemon, Pokemon, int)> TournamentResultBuilder(int id, int otherPokeId, int ties, int wins, int losses)
		{
			var mainPokemon = PokemonBuilder("water", 7);
			mainPokemon.Id = id;
			var otherPokemon = PokemonBuilder("fire", 5);
			otherPokemon.Id = otherPokeId;

			var results = new List<(Pokemon, Pokemon, int)>();

			for (int i = 0; i < ties; i++) 
			{
				results.Add((mainPokemon, otherPokemon, -1));
			}

			for (int i = 0; i < wins; i++)
			{
				results.Add((mainPokemon, otherPokemon, 0));
			}

			for (int i = 0; i < losses; i++)
			{
				results.Add((mainPokemon, otherPokemon, 1));
			}

			return results;
		}

		[Test]
		public void GivenTournamentResults__ProcessedResultsHave7Fights()
		{
			var pokemonId = 7;
			var otherPokeId = 19;
			var ties = 3;
			var wins = 2;
			var losses = 2;
			var tournamentResults = TournamentResultBuilder(pokemonId, otherPokeId, ties, wins, losses);

			var mockBattleHelper = new Mock<IBattleHelper>();
			var mockPokemonRepositry = new Mock<IRepository>();

			var tournamentHelper = new TournamentHelper(mockBattleHelper.Object, mockPokemonRepositry.Object);
			var processedResults = tournamentHelper.TournamentResultProcesser(tournamentResults);

			var expectedTotalFights = ties + wins + losses;
			
			var processedPokemon = processedResults[pokemonId];
			var actualTotalFights = processedPokemon.Ties + processedPokemon.Wins + processedPokemon.Losses;
			Assert.AreEqual(expectedTotalFights, actualTotalFights);
		}
	}
}
