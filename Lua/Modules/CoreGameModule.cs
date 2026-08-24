
using System;
using UnityEngine;
using MoonSharp.Interpreter;
using PlusLuaModding.Lua.Proxies;

namespace PlusLuaModding.Lua.Modules
{
	[MoonSharpUserData]
	public class CoreGameModule
	{
		public CoreGameManager Instance {
			get {
				return Singleton<CoreGameManager>.Instance;
			}
		}
		
		public bool disablePause {
			get {
				return Instance.disablePause;
			}
			set {
				Instance.disablePause = value;
			}
		}
		
		public string grade {
			get {
				return Instance.Grade;
			}
		}
		
		public int gradeVal {
			get {
				return Instance.GradeVal;
			}
		}
		
		public int lives {
			get {
				return Instance.Lives;
			}
		}	
		
		public bool InstanceExists()
		{
			return Instance != null;
		}
		
		public void AddPoints(int points, int player, bool playAnim, bool includeInLevelTotal, bool multiply) {
			if (Instance == null) {
				Debug.LogWarning("CoreGameManager doesn't exist yet. You should run the function if it exists.");
				return;
			}
			Instance.AddPoints(points, player, playAnim, includeInLevelTotal, multiply);
		}
		
		public void AddLives(int lives) {
			if (Instance == null) {
				Debug.LogWarning("CoreGameManager doesn't exist yet. You should run the function if it exists.");
				return;
			}
			Instance.AddLives(lives);
		}
		
		public void AddPoints(float mult) {
			if (Instance == null) {
				Debug.LogWarning("CoreGameManager doesn't exist yet. You should run the function if it exists.");
				return;
			}
			
			Instance.AddMultiplier(mult);
		}
		
		public int GetPoints(int playerNum) {
			if (Instance == null) {
				Debug.LogWarning("CoreGameManager doesn't exist yet. You should run the function if it exists.");
				return 0;
			}
			
			return Instance.GetPoints(playerNum);
		}
		
		public int GetPointsThisLevel(int playerNum) {
			if (Instance == null) {
				Debug.LogWarning("CoreGameManager doesn't exist yet. You should run the function if it exists.");
				return 0;
			}
			
			return Instance.GetPointsThisLevel(playerNum);
		}
		
		public PlayerProxy GetPlayer(int player) {
			if (Instance == null) {
				Debug.LogWarning("CoreGameManager doesn't exist yet. You should run the function if it exists.");
				return null;
			}
			return new PlayerProxy(Instance.GetPlayer(player));
		}
		
		public void SetRandomSeed() {
			if (Instance == null) {
				Debug.LogWarning("CoreGameManager doesn't exist yet. You should run the function if it exists.");
				return;
			}
			
			Instance.SetRandomSeed();
		}
	}
}
