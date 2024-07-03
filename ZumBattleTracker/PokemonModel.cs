﻿using ZumBattleTracker.Services;

namespace ZumBattleTracker
{
	public class PokemonModel
	{
        public PokemonModel(Pokemon pokemon)
        {
            Id = pokemon.Id;
			Name = pokemon.Name;
        }
        public int Id { get; set; }
		public string Name { get; set; }
		public int Wins { get; set; }
		public int Losses { get; set; }
		public int Ties { get; set; }
	}
}
