namespace ZumBattleTracker.Helpers
{

	public static class SortExtensions
	{
		public static IEnumerable<PokemonModel> Sort(this List<PokemonModel> pokemon, string sort, string sortDirection)
		{
			if (sortDirection == Constants.SortDirectionAscending)
			{
				if (sort == "wins")
				{
					return pokemon.OrderBy(x => x.Wins);
				}
				else if (sort == "losses")
				{
					return pokemon.OrderBy(x => x.Losses);
				}
				else if (sort == "ties")
				{
					return pokemon.OrderBy(x => x.Ties);
				}
				else if (sort == "name")
				{
					return pokemon.OrderBy(x => x.Name);
				}
				else
				{
					return pokemon.OrderBy(x => x.Id);
				}
			}
			else
			{

				if (sort == "wins")
				{
					return pokemon.OrderByDescending(x => x.Wins);
				}
				else if (sort == "losses")
				{
					return pokemon.OrderByDescending(x => x.Losses);
				}
				else if (sort == "ties")
				{
					return pokemon.OrderByDescending(x => x.Ties);
				}
				else if (sort == "name")
				{
					return pokemon.OrderByDescending(x => x.Name);
				}
				else
				{
					return pokemon.OrderByDescending(x => x.Id);
				}
			}
		}
	}
}
