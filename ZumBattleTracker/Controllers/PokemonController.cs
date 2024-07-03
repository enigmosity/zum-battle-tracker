using Microsoft.AspNetCore.Mvc;
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
			
			[HttpGet]
			[Route("/pokemon/tournament/statistics/")]
			public async Task<IActionResult> Get(string sortBy, string? sortDirection = Constants.SortDirectionDescending)
			{
				if (string.IsNullOrEmpty(sortBy) || !Constants.SortOrders.Contains(sortBy))
				{
					return Content("sortBy parameter is invalid");
				}

				if (sortDirection != Constants.SortDirectionDescending && sortDirection != Constants.SortDirectionAscending) {
					return Content("sortDirection parameter is invalid");
				}

				var pokemon = await _pokemonService.GetPokemon();
				if (pokemon == null)
				{
					throw new SystemException();
				}

				var tournamentResults = _tournamentHelper.BeginTournament(pokemon);
				var processedResults = _tournamentHelper.TournamentResultProcesser(tournamentResults);

				var sortedResults = processedResults.Values.ToList();

				return Ok(sortedResults.Sort(sortBy, sortDirection));
			}
		}
}
