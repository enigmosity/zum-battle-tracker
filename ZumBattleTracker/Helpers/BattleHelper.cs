using ZumBattleTracker.Services;

namespace ZumBattleTracker.Helpers
{
	public class BattleHelper
	{
		public BattleHelper()
		{
		}

		public Pokemon Battle(Pokemon fighter1, Pokemon fighter2) 
		{
			if (Constants.TypeBattles[fighter1.Type] == fighter2.Type)
			{
				return fighter1;
			} else if (Constants.TypeBattles[fighter2.Type] == fighter1.Type)
			{
				return fighter2;
			}
            else if (fighter1.BaseExperience == fighter2.BaseExperience)
            {
				return new Pokemon(); // this means the two pokemon tied
			} else
			{
				return fighter1.BaseExperience > fighter2.BaseExperience ? fighter1 : fighter2;
			}
		}

		


	}
}
