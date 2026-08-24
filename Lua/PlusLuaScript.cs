
using System;
using System.IO;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using UnityEngine;
using PlusLuaModding.Lua.Proxies;
using PlusLuaModding.Lua.Modules;

namespace PlusLuaModding.Lua
{
	[Serializable]
	public class PlusLuaScript
	{
		[SerializeField]
		public string name;
		
		[SerializeField]
		public string code;
		
		[NonSerialized]
		public PlusLuaMod modpack;
		
		public Script baseScript;
		public EventService events;
		
		public void Initialize() {
			if (modpack == null) return;
			baseScript = new Script(CoreModules.Preset_Default); // new script instance;
			events = new EventService();
			
			var loader = new FileSystemScriptLoader();
			
			List<string> searchPaths = new List<string>();
			
			
			if (Directory.Exists(LuaModdingPlugin.modulesFolder)) {
				searchPaths.Add(Path.Combine(LuaModdingPlugin.modulesFolder, "?.lua"));
				string[] subDirs = Directory.GetDirectories(LuaModdingPlugin.modulesFolder, "*", SearchOption.AllDirectories);
				foreach (string sub in subDirs) {
					searchPaths.Add(Path.Combine(sub, "?.lua"));
				}
			} else {
				Directory.CreateDirectory(LuaModdingPlugin.modulesFolder);
			}
			
			loader.ModulePaths = searchPaths.ToArray();
			baseScript.Options.ScriptLoader = loader;
			
			baseScript.Globals["MOD_PATH"] = modpack.path;
			baseScript.Globals["MOD_VERSION"] = modpack.data.version;
			baseScript.Globals["MOD_NAME"] = modpack.data.name;
			baseScript.Globals["GAME_VERSION"] = Application.version;
			
			// unity table ig
			Table plusTable = new Table(baseScript);
			plusTable["events"] = events;
			plusTable["debug"] = new DebugModule(modpack);
			plusTable["input"] = new InputModule();
			plusTable["interop"] = new InteropModule();
			plusTable["core"] = new CoreGameModule();
			plusTable["environment"] = new EnvironmentModule();
			plusTable["utility"] = new UtilModule();
			baseScript.Globals["plus"] = plusTable;
			baseScript.Globals["vector3"] = typeof(Vector3Proxy);
			baseScript.Globals["vector2"] = typeof(Vector2Proxy);
			baseScript.Globals["intVector2"] = typeof(IntVector2Proxy);
			baseScript.Globals["color"] = typeof(ColorProxy);
			
			
			
			baseScript.DoString(code);
		}
		
		public PlusLuaScript(string name, string code) {
			this.name = name;
			this.code = code;
		}
		
		/// <summary>
		/// Clones script... wow useful summary
		/// </summary>
		/// <returns></returns>
		public PlusLuaScript Clone() {
			PlusLuaScript s = new PlusLuaScript(name, code);
			s.modpack = modpack;
			s.Initialize();
			return s;
		}
	}
}
