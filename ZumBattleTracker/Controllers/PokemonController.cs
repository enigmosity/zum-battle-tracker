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
			
			[HttpGet]
			[Route("/pokemon/tournament/statistics/{sortBy}")]
			//[Route("Home/Index/{id?}")]
			public async Task<IEnumerable<PokemonModel>> Get(string sortBy)
			{
				if (string.IsNullOrEmpty(sortBy) || !Constants.SortOrders.Contains(sortBy))
				{
					// TODO: return the correct response for a bad sort order, rather than an exception
					throw new Exception();
				}
				var pokemon = await _pokemonService.GetPokemon();
				if (pokemon == null)
				{
					throw new SystemException();
				}

				var tournamentResults = _tournamentHelper.BeginTournament(pokemon);
				var processedResults = _tournamentHelper.TournamentResultProcesser(tournamentResults);

				var sortedResults = processedResults.Values.ToList();
				sortedResults.Sort(sortBy);

				return sortedResults;
			}
		}
}
