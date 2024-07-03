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

			Assert.AreEqual(water, result);
		}
	}
}