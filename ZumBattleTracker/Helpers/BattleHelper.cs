using ZumBattleTracker.Services;

namespace ZumBattleTracker.Helpers
{
	public class BattleHelper : IBattleHelper
	{
		public BattleHelper()
		{
		}

		public (Pokemon, Pokemon, int) Battle(Pokemon fighter1, Pokemon fighter2) 
		{
			if (Constants.TypeBattles[fighter1.Type] == fighter2.Type)
			{
				return (fighter1, fighter2, 0);
			} else if (Constants.TypeBattles[fighter2.Type] == fighter1.Type)
			{
				return (fighter1, fighter2, 1);
			}
            else if (fighter1.BaseExperience == fighter2.BaseExperience)
            {
				return (fighter1, fighter2, -1); // this means the two pokemon tied
			} else
			{
				var result = fighter1.BaseExperience > fighter2.BaseExperience ? 0 : 1;
				return (fighter1, fighter2, result);
			}
		}
	}

	public interface IBattleHelper
	{
		public (Pokemon, Pokemon, int) Battle(Pokemon fighter1, Pokemon fighter2);
	}
}
