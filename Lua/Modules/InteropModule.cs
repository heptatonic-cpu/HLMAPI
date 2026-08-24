
using System;
using MoonSharp.Interpreter;
using UnityEngine;
using HarmonyLib;
using PlusLuaModding.Lua.Proxies;

namespace PlusLuaModding.Lua.Modules
{
	[MoonSharpUserData]
	public class InteropModule
	{
		public DynValue FetchType(Script script, string name) {
			Type t = AccessTools.TypeByName(name);
			if (t != null) {
				if (!UserData.IsTypeRegistered(t)) {
					UserData.RegisterType(t);
				}
				return UserData.CreateStatic(t);
			}
			
			return DynValue.Nil;
		}
		
		public GameObjectProxy FindObject(string name)
		{
			return new GameObjectProxy(GameObject.Find(name));
		}
		
		public void Destroy(GameObject a) {
			UnityEngine.Object.Destroy(a);
		}
	}
}
