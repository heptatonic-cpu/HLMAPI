
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MTM101BaldAPI;
using MTM101BaldAPI.ObjectCreation;
using MTM101BaldAPI.Registers;
using PlusLuaModding.Data;
using UnityEngine;

namespace PlusLuaModding.Lua.Builders
{
	public static class LuaObjectBuilder
	{
		public static ItemObject Create(LuaItemObjectData a, PlusLuaMod modpack)
		{
			ItemBuilder ib = new ItemBuilder(LuaModdingPlugin.Instance.Info)
				.SetItemComponent<LuaItem>()
				.SetNameAndDescription(a.name, a.description)
				.SetGeneratorCost(a.properties.generatorCost)
				.SetEnum(a.properties.itemEnum)
				.SetMeta(EnumExtensions.GetFromExtendedName<ItemFlags>(a.properties.itemFlag), a.properties.tags)
				.SetShopPrice(a.properties.price)
				.SetSprites(modpack.assetMan.Get<Sprite>(a.properties.smallSpriteKey), modpack.assetMan.Get<Sprite>(a.properties.largeSpriteKey));
				
			if (!string.IsNullOrEmpty(a.properties.pickupSoundKey)) {
				SoundObject aud = modpack.assetMan.Get<SoundObject>(a.properties.pickupSoundKey);
				if (aud != null)
					ib.SetPickupSound(aud);
			}
			if (!a.properties.overridable)
				ib.SetAsNotOverridable();
			if (a.properties.instantUse)
				ib.SetAsInstantUse();
			
			ItemObject itm = ib.Build();
			LuaItem i = (LuaItem)itm.item;
			i.modpackName = modpack.data.name;
			i.scriptNameToFetch = Path.GetFileNameWithoutExtension(a.scriptFilePath);
			
			if (a.generatorProperties != null) {
				WeightedItemObject w = new WeightedItemObject {
					selection = itm,
					weight = a.generatorProperties.weight
				};
				a.generatorProperties.weightedSelf = w;
			}
			return itm;
		}
		
		public static NPC CreateNPC(LuaNPCData a, PlusLuaMod modpack) {
			NPCBuilder<LuaNPC> nb = new NPCBuilder<LuaNPC>(LuaModdingPlugin.Instance.Info)
				.SetName(a.name)
				.SetEnum(a.properties.npcEnum);
			if (a.properties.hasLooker) nb.AddLooker();
			if (a.properties.hasTrigger) nb.AddTrigger();
			if (a.properties.hasHeatmap) nb.AddHeatmap();
			if (a.properties.canWanderEnterRooms) nb.SetWanderEnterRooms();
			if (a.properties.hasAcceleration) nb.EnableAcceleration();
			if (a.properties.disableAutoRotation) nb.DisableAutoRotation();
			if (a.properties.disableNavigationPrecision) nb.DisableNavigationPrecision();
			
			if (a.properties.npcFlags != null & a.properties.npcFlags.Length != 0) {
				foreach (string flag in a.properties.npcFlags) {
					nb.AddMetaFlag(EnumExtensions.GetFromExtendedName<NPCFlags>(flag));
				}
			}
			
			if (a.properties.npcTags != null && a.properties.npcTags.Length != 0) nb.SetMetaTags(a.properties.npcTags);
			
			if (a.properties.forcedCaptionColor && a.properties.forcedColor != null && a.properties.forcedColor.Length >= 4) {
				Color color = new Color(a.properties.forcedColor[0],a.properties.forcedColor[1],a.properties.forcedColor[2],a.properties.forcedColor[3]);
				nb.SetForcedSubtitleColor(color);
			}
			
			LuaNPC npc = nb.Build();
			npc.modpackName = modpack.data.name;
			npc.scriptNameToFetch = Path.GetFileNameWithoutExtension(a.scriptFilePath);
			if (!string.IsNullOrEmpty(a.properties.baseSpriteKey)) {
				Sprite spr = modpack.assetMan.Get<Sprite>(a.properties.baseSpriteKey);
				npc.spriteRenderer[0].sprite = spr;
			}
			
			if (a.generatorProperties != null) {
				WeightedNPC w = new WeightedNPC {
					selection = npc,
					weight = a.generatorProperties.weight
				};
				a.generatorProperties.weightedSelf = w;
			}
			return npc;
		}
		
	}
}
