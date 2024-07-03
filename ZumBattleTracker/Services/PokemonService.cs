namespace ZumBattleTracker.Services
{
	public class PokemonService
	{
		public PokemonService() { }

		private readonly HttpClient httpClient = new HttpClient();


		public async void GetPokemon()
		{
			// generate 8 ids from 1 - 151 

			var ids = new List<int>() { 1,2,3,4,5,6,7,8 };

			// retrieve 8 pokemon

			foreach(int id in ids) 
			{
				var pokemon = await GetSinglePokemon(id);
			}
		}

		public async Task<Pokemon> GetSinglePokemon(int id)
		{
			// set up an http client to interact with the pokeapi

			try
			{
				using HttpResponseMessage response = await httpClient.GetAsync(Constants.PokeApiEndpoint);
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
			// TODO :come back and tidy this up
			return null;

		}
	}
}
