using ZumBattleTracker.Helpers;
using ZumBattleTracker.Services;

namespace UnitTests
{
	public class BattleTests
	{
		private BattleHelper _battleHelper;

		[SetUp]
		public void Setup()
		{
			_battleHelper = new BattleHelper();
		}

		[Test]
		public void GivenWaterPokemon1_FirePokemon2_WaterWins()
		{
			// need to set up a pokemon
			var fire = new Pokemon();
			fire.Type = "fire";
			var water = new Pokemon();
			water.Type = "water";
			var result = _battleHelper.Battle(water, fire);

			Assert.AreEqual((water, fire, 0), result);
		}

		[Test]
		public void GivenFirePokemon1_WaterPokemon2_WaterWins()
		{
			// need to set up a pokemon
			var fire = new Pokemon();
			fire.Type = "fire";
			var water = new Pokemon();
			water.Type = "water";
			var result = _battleHelper.Battle(fire, water);

			Assert.AreEqual((fire, water, 1), result);
		}

		[Test]
		public void GivenNoTypeWinner_ReturnFighter2_WithHigherBaseExp()
		{
			// need to set up a pokemon
			var fire = new Pokemon();
			fire.Type = "fire";
			fire.BaseExperience = 10;
			var electric = new Pokemon();
			electric.Type = "electric";
			electric.BaseExperience = 20;
			var result = _battleHelper.Battle(fire, electric);

			Assert.AreEqual((fire, electric, 1), result);
		}

		[Test]
		public void GivenNoTypeWinner_ReturnFighter1_WithHigherBaseExp()
		{
			// need to set up a pokemon
			var fire = new Pokemon();
			fire.Type = "fire";
			fire.BaseExperience = 80;
			var electric = new Pokemon();
			electric.Type = "electric";
			electric.BaseExperience = 20;
			var result = _battleHelper.Battle(fire, electric);

			Assert.AreEqual((fire, electric, 0), result);
		}

		[Test]
		public void GivenNoTypeWinnerAndNoBaseExpWinner_ReturnEmptyPokemon()
		{
			// need to set up a pokemon
			var fire = new Pokemon();
			fire.Type = "fire";
			fire.BaseExperience = 20;
			var electric = new Pokemon();
			electric.Type = "electric";
			electric.BaseExperience = 20;
			var result = _battleHelper.Battle(fire, electric);

			Assert.That(result.Item3, Is.EqualTo(-1));
		}

	}
}