
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using PlusLuaModding.Lua.Proxies;

namespace PlusLuaModding.Lua.Modules
{
	[MoonSharpUserData]
	public class EnvironmentModule
	{
		public EnvironmentController Instance {
			get {
				return Singleton<BaseGameManager>.Instance.Ec;
			}
		}
		
		public bool InstanceExists()
		{
			return Instance != null;
		}
		
		public Vector3Proxy GetRandomCellCenterPosition(bool includeOffLimits, bool includeWithObjects, bool useEntitySafeCell) {
			if (Instance == null) {
				Debug.LogWarning("An instance of EnvironmentController doesn't exist yet, returning null");
				return null;
			}
			return new Vector3Proxy(Instance.RandomCell(includeOffLimits, includeWithObjects, useEntitySafeCell).CenterWorldPosition);
		}
		
		public Vector3Proxy GetRandomCellFloorPosition(bool includeOffLimits, bool includeWithObjects, bool useEntitySafeCell) {
			if (Instance == null) {
				Debug.LogWarning("An instance of EnvironmentController doesn't exist yet, returning null");
				return null;
			}
			
			return new Vector3Proxy(Instance.RandomCell(includeOffLimits, includeWithObjects, useEntitySafeCell).FloorWorldPosition);
		}
		
		public void AddTimeScale(float npcTimeScale, float environmentTimeScale, float playerTimeScale) {
			if (Instance == null) {
				Debug.LogWarning("An instance of EnvironmentController doesn't exist yet, returning null");
				return;
			}
			
			Instance.AddTimeScale(new TimeScaleModifier(npcTimeScale,environmentTimeScale, playerTimeScale));
		}
		
		public CellProxy[] GetAllCells() {
			List<Cell> cells = Instance.AllCells();
			List<CellProxy> cellProxies = new List<CellProxy>();
			foreach (Cell cell in cells) cellProxies.Add(new CellProxy(cell));
			return cellProxies.ToArray();
		}
		
		public void CompleteMap() {
			if (Instance == null) {
				Debug.LogWarning("An instance of EnvironmentController doesn't exist yet, returning null");
				return;
			}
			
			Instance.map.CompleteMap();
		}
		
		public NPCProxy GetNPCByName(string name) {
			if (Instance == null) {
				Debug.LogWarning("An instance of EnvironmentController doesn't exist yet, returning null");
				return null;
			}
			return new NPCProxy(Instance.Npcs.First(n => n.name.Replace("(Clone)", "") == name));
		}
	}
}
