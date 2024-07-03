using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using ZumBattleTracker.Helpers;
using ZumBattleTracker.Services;

namespace ZumBattleTracker.Controllers
{
		[ApiController]
		[Route("[controller]")]
		public class PokemonController : ControllerBase
		{
			private readonly ILogger<PokemonController> _logger;
			private readonly PokemonService _pokemonService;
		private readonly TournamentHelper _tournamentHelper;

			public PokemonController(ILogger<PokemonController> logger, PokemonService pokeService, TournamentHelper tournamentHelper)
			{
				_logger = logger;
				_pokemonService = pokeService;
				_tournamentHelper = tournamentHelper;
			}
			
			// ensure this is the correct get - check how it impacts routing and naming scheme for API calls
			[HttpGet(Name = "GetPokemon")]
			public async Task<IEnumerable<PokemonModel>> Get()
			{
				var pokemon = await _pokemonService.GetPokemon();
				if (pokemon == null)
				{
					throw new SystemException();
				}

				var tournamentResults = _tournamentHelper.BeginTournament(pokemon);
				var processedResults = _tournamentHelper.TournamentResultProcesser(tournamentResults);

				return processedResults.Values.ToList();
			}
		}
}
