
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlusLuaModding.Data
{
	public class PlusLuaModData
	{
		public string name { get; set; } 
		public string description { get; set; }
		public string version { get; set; }
		public Dictionary<string, string> authors { get; set; }
		public Dictionary<string, string> dependencies { get; set; }
		
	}
}
