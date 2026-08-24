
using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace PlusLuaModding.Lua.Proxies
{
	[MoonSharpUserData]
	public class NPCProxy
	{
		[MoonSharpHidden]
		public NPC npc;
		
		public NPCProxy(NPC n) {
			npc = n;
		}
		
		public GameObjectProxy gameObject
		{
			get { return new GameObjectProxy(npc.gameObject); }
		}
		
		public float speed {
			get { return npc.Navigator.speed; }
			set { npc.Navigator.SetSpeed(value); }
		}
		
		public float maxSpeed {
			get { return npc.Navigator.maxSpeed; }
			set { npc.Navigator.maxSpeed = value; }
		}
		
		public bool hidden {
			get {
				return npc.Entity.Hidden;
			} set {
				npc.Entity.SetHidden(value);
			}
		}
		
		public float height {
			get {
				return npc.Entity.BaseHeight;
			} set {
				npc.Entity.SetHeight(value);
			}
		}
		
		
		public override string ToString()
		{
			return npc.name;
		}
		
			
		public void Squish(float time) {
			if (npc.Entity != null) npc.Entity.Squish(time);
		}
		
		public void Unsquish() {
			if (npc.Entity != null) npc.Entity.Unsquish();
		}
		
		public void SetSpriteColor(int a, ColorProxy color) {
			npc.spriteRenderer[a].color = color.ToColor();
		}
		
		public void SetWander(int priority) {
			npc.behaviorStateMachine.ChangeNavigationState(new NavigationState_WanderRandom(npc, priority));
		}
	}
}
