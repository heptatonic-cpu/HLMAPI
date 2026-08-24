
using System;
using UnityEngine;
using MoonSharp.Interpreter;
using PlusLuaModding.Lua.Proxies;

namespace PlusLuaModding.Lua
{
	public class LuaItem : Item
	{
		// properties to fetch the script
		[SerializeField]
		public string modpackName;
		[SerializeField]
		public string scriptNameToFetch;
		
		PlusLuaScript clonedLuaScript;
		
		public override bool Use(PlayerManager pm)
		{
			this.pm = pm;
			Debug.Log(scriptNameToFetch);
			if (clonedLuaScript == null) {
				PlusLuaScript fetched = PlusLuaMod.packRegistry[modpackName].scripts[scriptNameToFetch];
				if (fetched == null) return false;
				clonedLuaScript = fetched.Clone();
				clonedLuaScript.baseScript.Globals["self"] = gameObject;
			}
			DynValue func = clonedLuaScript.baseScript.Globals.Get("onUse");
			if (func.IsNotNil() && func.Type == DataType.Function) {
				DynValue result = func.Function.Call(new PlayerProxy(pm));
				
				if (result != null && result.Type == DataType.Boolean) {
					return result.Boolean;
				}
			}
			return false;
		}
		
		public override void PostUse(PlayerManager pm)
		{
			DynValue func = clonedLuaScript.baseScript.Globals.Get("onUsePost");
			if (func.IsNotNil() && func.Type == DataType.Function) {
				func.Function.Call();
			}	
		}
		
		void Update() {
			DynValue func = clonedLuaScript.baseScript.Globals.Get("itemUpdate");
			if (func.IsNotNil() && func.Type == DataType.Function) {
				func.Function.Call(Time.deltaTime);
			}	
		}
	}
	
	public class LuaNPC : NPC
	{
		// properties to fetch the script
		[SerializeField]
		public string modpackName;
		[SerializeField]
		public string scriptNameToFetch;
		
		PlusLuaScript clonedLuaScript;
		
		public override void Initialize()
		{
			base.Initialize();
			if (clonedLuaScript == null) {
				PlusLuaScript fetched = PlusLuaMod.packRegistry[modpackName].scripts[scriptNameToFetch];
				if (fetched == null) return;
				clonedLuaScript = fetched.Clone();
				clonedLuaScript.baseScript.Globals["self"] = new NPCProxy(this);
			}
			DynValue func = clonedLuaScript.baseScript.Globals.Get("initialize");
			if (func.IsNotNil() && func.Type == DataType.Function) func.Function.Call();
		}
		
		public override void PlayerInSight(PlayerManager player)
		{
			base.PlayerInSight(player);
			DynValue func = clonedLuaScript.baseScript.Globals.Get("playerInSight");
			if (func.IsNotNil() && func.Type == DataType.Function) func.Function.Call(new PlayerProxy(player));
		}
		
		public override void PlayerSighted(PlayerManager player)
		{
			base.PlayerSighted(player);
			DynValue func = clonedLuaScript.baseScript.Globals.Get("playerSighted");
			if (func.IsNotNil() && func.Type == DataType.Function) func.Function.Call(new PlayerProxy(player));
		}
		
		public override void PlayerLost(PlayerManager player)
		{
			base.PlayerLost(player);
			DynValue func = clonedLuaScript.baseScript.Globals.Get("playerLost");
			if (func.IsNotNil() && func.Type == DataType.Function) func.Function.Call(new PlayerProxy(player));
		}
		
		public override void Unsighted()
		{
			base.Unsighted();
			DynValue func = clonedLuaScript.baseScript.Globals.Get("unsighted");
			if (func.IsNotNil() && func.Type == DataType.Function) func.Function.Call();
		}
		
		public override void DestinationEmpty()
		{
			base.DestinationEmpty();
			DynValue func = clonedLuaScript.baseScript.Globals.Get("destinationEmpty");
			if (func.IsNotNil() && func.Type == DataType.Function) func.Function.Call();
		}
		
		public override void Hear(GameObject source, Vector3 position, int value)
		{
			base.Hear(source, position, value);
			DynValue func = clonedLuaScript.baseScript.Globals.Get("hear");
			if (func.IsNotNil() && func.Type == DataType.Function) func.Function.Call(new GameObjectProxy(source), new Vector3Proxy(position), value);
		}
		
		public override float DistanceCheck(float val)
		{
			DynValue func = clonedLuaScript.baseScript.Globals.Get("distanceCheck");
			if (func.IsNotNil() && func.Type == DataType.Function) return (float)func.Function.Call(val).Number;
			return base.DistanceCheck(val);
		}
		
		
	}
	
	public class LuaNPCState : NavigationState
}
