namespace ZumBattleTracker
{
	public class Constants
	{
		public const string PokeApiEndpoint = "https://pokeapi.co/api/v2/pokemon/";
		public const int LowestPokemonId = 1;
		public const int HighestPokemonId = 151;

		public static readonly Dictionary<string, string> TypeBattles = new Dictionary<string, string>()
		{
			{ "water", "fire" },
			{ "fire", "grass" },
			{ "grass", "electric" },
			{ "electric", "water" },
			{ "ghost", "psychic" },
			{ "psychic", "fighting" },
			{ "fighting", "dark" },
			{ "dark", "ghost" }
		};

		public static readonly List<string> SortOrders = new List<string>() { "wins", "losses", "id", "name", "ties" };
	}
}
