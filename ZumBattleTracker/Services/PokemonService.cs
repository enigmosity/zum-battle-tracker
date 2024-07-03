namespace ZumBattleTracker.Services
{
	public class PokemonService
	{
		public PokemonService(Random random) 
		{
			_randomService = random;
		}

		private readonly HttpClient httpClient = new HttpClient();
		private readonly Random _randomService;




		public async Task<List<Pokemon>> GetPokemon()
		{
			// generate 8 ids from 1 - 151 

			var pokemonList = new List<Pokemon>();

			while (pokemonList.Count < 8)
			{
				var id = _randomService.Next(Constants.LowestPokemonId, Constants.HighestPokemonId);
				var pokemon = await GetSinglePokemon(id);
				pokemon.SetType();
				pokemonList.Add(pokemon);
			}

			return pokemonList;
		}

		public async Task<Pokemon> GetSinglePokemon(int id)
		{
			// set up an http client to interact with the pokeapi

			try
			{
				using HttpResponseMessage response = await httpClient.GetAsync($"{Constants.PokeApiEndpoint}{id}");
				response.EnsureSuccessStatusCode();
				var responseBody = await response.Content.ReadFromJsonAsync<Pokemon>();
				// Above three lines can be replaced with new helper method below
				// string responseBody = await client.GetStringAsync(uri);

				if (responseBody != null)
				{
					return responseBody;
				}
			}
			catch (HttpRequestException e)
			{
				Console.WriteLine("\nException Caught!");
				Console.WriteLine("Message :{0} ", e.Message);
			}
			// TODO: come back and tidy this up
			return null;

		}
	}
}
