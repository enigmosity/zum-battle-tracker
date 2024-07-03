namespace ZumBattleTracker.Helpers
{

	public static class SortExtensions
	{
		public static void Sort(this List<PokemonModel> pokemon, string sort)
		{
			if (sort == "wins")
			{
				pokemon.Sort((x, y) => x.Wins.CompareTo(y.Wins));
			} else if (sort == "losses") {
				pokemon.Sort((x, y) => x.Losses.CompareTo(y.Losses));
			} else if (sort == "ties")
			{
				pokemon.Sort((x, y) => x.Ties.CompareTo(y.Ties));
			} else if (sort == "name")
			{
				pokemon.Sort((x, y) => x.Name.CompareTo(y.Name));
			} else
			{
				pokemon.Sort((x, y) => x.Id.CompareTo(y.Id));
			}
		}
	}
}
