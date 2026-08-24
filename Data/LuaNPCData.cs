
using System;
using Newtonsoft.Json;

namespace PlusLuaModding.Data
{
	public class LuaNPCData
	{
		public string name { get; set; }
		public string version { get; set; }
		public LuaNPCProperties properties { get; set; }
		public LuaNPCGeneratorProperties generatorProperties { get; set; }
		
		 [JsonIgnore]
        public string scriptFilePath { get; set; }
		
		public LuaNPCData() {
			name = "Unknown";
		}
	}
	
	public class LuaNPCProperties
	{
		
		public bool hasLooker { get; set; }
		public bool hasTrigger { get; set; }
		public bool hasHeatmap { get; set; }
		public bool hasAcceleration { get; set; }
		public bool disableAutoRotation { get; set; }
		public bool disableNavigationPrecision { get; set; }
		public bool canWanderEnterRooms { get; set; }
		public bool forcedCaptionColor { get; set; }
		public bool airborne { get; set; }
		public bool stationary { get; set; }
		public float[] forcedColor { get; set; }
		public string[] npcFlags { get; set; }
		public string[] npcTags { get; set; }
		public string npcEnum { get; set; }
		public string baseSpriteKey { get; set; }
	}
	
	public class LuaNPCGeneratorProperties
	{
		public int weight { get; set; }
        public bool generateInEveryFloor { get; set; }
        public string[] generateInFloors { get; set; }
        
        [JsonIgnore]
        public WeightedNPC weightedSelf { get; set; }
	}
}
