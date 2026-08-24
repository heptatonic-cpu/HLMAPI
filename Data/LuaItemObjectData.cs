using System;
using Newtonsoft.Json;

namespace PlusLuaModding.Data
{
    public class LuaItemObjectData
    {
        public string name { get; set; }
        public string description { get; set; }
        public string version { get; set; }
        public LuaItemObjectProperties properties { get; set; }
        public LuaItemObjectGeneratorProperties generatorProperties { get; set; }
        
        [JsonIgnore]
        public string scriptFilePath { get; set; }
    }
    
    public class LuaItemObjectProperties 
    {
        public string itemEnum { get; set; }
        public string itemFlag { get; set; }
        public string[] tags { get; set; }
        public int generatorCost { get; set; }
        public int price { get; set; }
        public string smallSpriteKey { get; set; }
        public string largeSpriteKey { get; set; }
        public string pickupSoundKey { get; set; }
        public bool overridable { get; set; }
        public bool instantUse { get; set; }
    }
    
    // long ahh freaking class name
    public class LuaItemObjectGeneratorProperties 
    {
        public int weight { get; set; }
        public bool generateInEveryFloor { get; set; }
        public string[] generateInFloors { get; set; }
        
        [JsonIgnore]
        public WeightedItemObject weightedSelf { get; set; }
    }
}