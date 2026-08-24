
using System;
using System.Linq;
using UnityEngine;
using MoonSharp.Interpreter;

namespace PlusLuaModding.Lua.Modules
{
	[MoonSharpUserData]
	public class UtilModule
	{
		public float RandomFloat(float min, float max)
		{
			return UnityEngine.Random.Range(min, max);
		}
		
		public int RandomInt(int min, int max)
		{
			return UnityEngine.Random.Range(min, max);
		}
		
		// temporary
		public void PlaySoundInWorld(string name) {
			CoreGameManager.Instance.audMan.PlaySingle(Resources.FindObjectsOfTypeAll<SoundObject>().First(a => a.name == name));
		}
	}
}
