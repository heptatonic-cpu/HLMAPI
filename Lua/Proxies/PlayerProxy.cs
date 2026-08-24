
using System;
using MoonSharp.Interpreter;
using MTM101BaldAPI.PlusExtensions;
using UnityEngine;

namespace PlusLuaModding.Lua.Proxies
{
	[MoonSharpUserData]
	public class PlayerProxy
	{
		public PlayerProxy(PlayerManager pm) 
		{
			this.pm = pm;
			moveStatMod = pm.GetMovementStatModifier();
			moveMod = new MovementModifier(Vector3.zero, 1f);
			moveMod.ignoreGrounded = false;
			moveMod.ignoreAirborne = false;
			pm.plm.Entity.ExternalActivity.moveMods.Add(moveMod);
		}
		
	 	PlayerManager pm;
		MovementModifier moveMod; 
		PlayerMovementStatModifier moveStatMod;
		
		public GameObjectProxy gameObject
		{
			get { return new GameObjectProxy(pm.gameObject); }
		}
		
		public bool squished 
		{
			get { return pm.plm.Entity.Squished; }	
		}
		
		public string ruleBreak {
			get { return pm.ruleBreak; }
		}
		
		public bool disobeying {
			get { return pm.Disobeying; }
		}
		
		public int playerNumber {
			get { return pm.playerNumber; }
		}
		
		// just straight up copied these variables from level studio
		public float baseWalkSpeed
        {
			get { return moveStatMod.baseStats["walkSpeed"]; }
            set { moveStatMod.ChangeBaseStat("walkSpeed", value); }
        }

        public float baseRunSpeed
        {
        	get { return moveStatMod.baseStats["runSpeed"]; }
            set { moveStatMod.ChangeBaseStat("runSpeed", value); }
        }

        public float baseStaminaDrop
        {
        	get { return moveStatMod.baseStats["staminaDrop"]; }
            set { moveStatMod.ChangeBaseStat("staminaDrop", value); }
        }

        public float baseStaminaMax
        {
        	get { return moveStatMod.baseStats["staminaMax"]; }
            set { moveStatMod.ChangeBaseStat("staminaMax", value); }
        }

        public float baseStaminaRise
        {
        	get { return moveStatMod.baseStats["staminaRise"]; }
            set { moveStatMod.ChangeBaseStat("staminaRise", value); }
        }

        public float walkSpeed
        {
        	get { return pm.plm.walkSpeed; } 
        }

        public float runSpeed
        {
        	get { return pm.plm.runSpeed; }
        }

        public float staminaDrop
        {
        	get { return pm.plm.staminaDrop; }
        }

        public float staminaMax
        {
        	get { return pm.plm.staminaMax; }
        }

        public float staminaRise
        {
        	get { return pm.plm.staminaRise; }
        }

        public float stamina
        {
            get { return pm.plm.stamina; }
            set {  pm.plm.stamina = value; }
        }
		
		public void Squish(float time) 
		{
			pm.plm.Entity.Squish(time);
		}
		
		public void Unsquish() 
		{
			pm.plm.Entity.Unsquish();
		}
		
		public void SetHidden(bool val)
		{
			pm.plm.Entity.SetHidden(val);
		}
		
		public void SetFrozen(bool val) 
		{
			pm.plm.Entity.SetFrozen(val);
		}
		
		public void SetInteractionState(bool val) 
		{
			pm.plm.Entity.SetInteractionState(val);
		}
		
		public void Teleport(Vector3Proxy position) 
		{
			pm.Teleport(position.ToVector());
		}
		
		public void RuleBreak(string rule, float time) 
		{
			pm.RuleBreak(rule, time);
		}
	}
}
