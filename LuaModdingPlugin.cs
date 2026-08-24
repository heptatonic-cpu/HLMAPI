
using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using MTM101BaldAPI;
using MoonSharp.Interpreter;
using PlusLuaModding.Lua;
using UnityEngine;

namespace PlusLuaModding
{
	[BepInPlugin("heptatonic.bbplus.plusluamodding", "Plus Lua Modding", "1.0.0.0")]
	[BepInDependency("mtm101.rulerp.bbplus.baldidevapi")]
    [BepInDependency("mtm101.rulerp.baldiplus.levelstudioloader")]
	public class LuaModdingPlugin : BaseUnityPlugin
	{
		public static LuaModdingPlugin Instance;
		public static List<PlusLuaMod> activeMods = new List<PlusLuaMod>();
		
		public static string packsFolder {
			get {
				return Path.Combine(baseFolder, "packs");
			}
		}
		
		public static string modulesFolder {
			get {
				return Path.Combine(baseFolder, "modules");
			}
		}
		
		public static string baseFolder {
			get {
				return Path.Combine(Paths.GameRootPath, "modding");
			}
		}
		
		void Awake() {
			Instance = this;
			Harmony harmony = new Harmony("heptatonic.bbplus.plusluamodding");
			harmony.PatchAllConditionals();
			
			UserData.RegisterAssembly();
			UserData.RegisterType<GameObject>();
			StartCoroutine(WaitFor());
		}
		
		IEnumerator WaitFor() {
			yield return null;
			Debug.Log("Frame");
			LoadModpacks();
		}
	
		void LoadModpacks() {
			foreach (string modFolder in Directory.GetDirectories(packsFolder)) {
				PlusLuaMod mod = new PlusLuaMod(modFolder);
				mod.InitializeMod();
				activeMods.Add(mod);
			}
		}
		
		public static void TriggerGlobal(string eventName, params object[] args) {
			if (activeMods.Count == 0) return;
			foreach (var mod in activeMods) {
				foreach (var scr in mod.scripts.Values) {
					scr.events.Trigger(eventName, args);
				}
			}
		}
		
		void Update() {
			TriggerGlobal("Update", Time.deltaTime);
		}
	}
}