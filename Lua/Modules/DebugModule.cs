
using System;
using UnityEngine;
using MoonSharp.Interpreter;

namespace PlusLuaModding.Lua.Modules
{
	[MoonSharpUserData]
	public class DebugModule
	{
		PlusLuaMod mod;
		
		public DebugModule(PlusLuaMod mod) {
			this.mod = mod;
		}
		
		// funcs eh
		public void Log(object m) {
			mod.Log(m);
		}
		public void LogWarning(object m) {
			mod.LogWarning(m);
		}
		public void LogError(object m) {
			mod.LogError(m);
		}
	}
}
