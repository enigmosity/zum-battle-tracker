using System.Text.Json.Serialization;

namespace ZumBattleTracker.Services
{
	public class Pokemon
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		[JsonPropertyName("base_experience")]
		public int BaseExperience { get; set; }
		public List<TypeListObject> Types { get; set; }

		public void SetType()
		{
			Type = Types[0].SlotType.Name;
		}
	}

	public class TypeListObject
	{
		public int Slot { get; set; }
		[JsonPropertyName("type")]
		public Type SlotType { get; set; }
	}

	public class Type
	{
		public string Name { get; set; }
	}
}

//{
//    "base_experience": 62,
//    "types": [

//		{
//		"slot": 1,
//      "type": {
//			"name": "fire",
//          "url": "https://pokeapi.co/api/v2/type/10/"
//		}
//	}
//    ],
//    "weight": 85
//}