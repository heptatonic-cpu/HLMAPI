
using System;
using HarmonyLib;
using UnityEngine;

namespace PlusLuaModding.Patches
{
	[HarmonyPatch]
	public class MainPatches
	{
		[HarmonyPatch(typeof(ItemManager), "UseItem")]
		[HarmonyPrefix]
		public static void ItemUse(ItemManager __instance)
		{
			ItemObject itm = __instance.items[__instance.selectedItem];
			string name = itm.name.ToLower();
			if (itm == __instance.nothing) {
				name = "nothing";
			}
			LuaModdingPlugin.TriggerGlobal("UseItem", name);
		}
		
		[HarmonyPatch(typeof(CoreGameManager), "Update")]
		[HarmonyPrefix]
		public static void CGMUpdate(CoreGameManager __instance)
		{
			LuaModdingPlugin.TriggerGlobal("CoreGameManagerUpdate", Time.deltaTime);
		}
		
		[HarmonyPatch(typeof(BaseGameManager), "Update")]
		[HarmonyPrefix]
		public static void BGMUpdate(BaseGameManager __instance)
		{
			LuaModdingPlugin.TriggerGlobal("BaseGameManagerUpdate", Time.deltaTime);
		}
	}
}
