using Microsoft.AspNetCore.Mvc;
using ZumBattleTracker.Services;

namespace ZumBattleTracker.Controllers
{
		[ApiController]
		[Route("[controller]")]
		public class PokemonController : ControllerBase
		{
			private static readonly string[] Pokemon = new[]
			{
				"Ratata", "Pikachu", "Squirtle", "Pokemon1", "Mild", "Warm", "Balmy", "Hot"
			};

			private readonly ILogger<PokemonController> _logger;
			private readonly PokemonService _pokemonService;

			public PokemonController(ILogger<PokemonController> logger,PokemonService pokeService)
			{
				_logger = logger;
				_pokemonService = pokeService;
			}
			
			// ensure this is the correct get - check how it impacts routing and naming scheme for API calls
			[HttpGet(Name = "GetPokemon")]
			public async Task<IEnumerable<string>> Get()
			{
				var pokemon = await _pokemonService.GetSinglePokemon(4);
				if (pokemon == null)
				{
					return new List<string>();
				}
				return new List<string> { pokemon.ToString() };
			}
		}
}
