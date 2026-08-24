
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using MTM101BaldAPI;
using MTM101BaldAPI.AssetTools;
using MTM101BaldAPI.Registers;
using UnityEngine;
using PlusLuaModding.Data;
using PlusLuaModding.Lua.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HarmonyLib;

namespace PlusLuaModding.Lua
{
	[Serializable]
	public class PlusLuaMod
	{
		public PlusLuaModData data;
		public string path;
		
		public List<LuaItemObjectData> itemDatas = new List<LuaItemObjectData>();
		public List<LuaNPCData> npcDatas = new List<LuaNPCData>();
		
		public AssetManager assetMan = new AssetManager();
		
		public Dictionary<string, PlusLuaScript> scripts = new Dictionary<string, PlusLuaScript>();
		
		public static Dictionary<string, PlusLuaMod> packRegistry = new Dictionary<string, PlusLuaMod>();
		
		/// <summary>
		/// The modpack's scripts folder path.
		/// </summary>
		public string scriptsPath {
			get {
				return Path.Combine(path, "scripts");
			}
		}
		
		/// <summary>
		/// The modpack's audio folder path
		/// </summary>
		public string audioPath {
			get {
				return Path.Combine(path, "audios");
			}
		}
		
		/// <summary>
		/// The modpack's textures folder path
		/// </summary>
		public string texturesPath {
			get {
				return Path.Combine(path, "textures");
			}
		}
		
		/// <summary>
		/// The modpack's data folder path
		/// </summary>
		public string dataPath {
			get {
				return Path.Combine(path, "data");
			}
		}
		
		public PlusLuaMod(string path) {
			this.path = path;
			
		}
		
		public void FetchAll(bool forceInit) {
			scripts.Clear(); // clear the list first
			
			string[] filePaths = Directory.GetFiles(path, "*.lua", SearchOption.AllDirectories);
			for (int i = 0; i < filePaths.Length; i++) {
				PlusLuaScript scr = new PlusLuaScript(Path.GetFileNameWithoutExtension(filePaths[i]), File.ReadAllText(filePaths[i]));
				scripts.Add(Path.GetFileNameWithoutExtension(filePaths[i]), scr);
				Debug.Log("Script loaded: " + Path.GetFileNameWithoutExtension(filePaths[i]));
			}
			if (forceInit) InitAll();
		}
		
		public void InitializeMod() {
			data = JsonConvert.DeserializeObject<PlusLuaModData>(File.ReadAllText(Path.Combine(path, "pack.json")));
			packRegistry.Add(data.name, this);
			FetchAll(true);
			LoadingEvents.RegisterOnAssetsLoaded(LuaModdingPlugin.Instance.Info, OnAssetLoad(), LoadingEventOrder.Pre);
		}
		
		public void InitAll() {
			foreach (var scr in scripts.Values) {
				scr.modpack = this;
				scr.Initialize();
			}
		}
		
		public void GetJson() {
			

		}
		
		public void AddAllAssets() {
			// Sprite Loading
			List<string> texFolderDirs = new List<string>();
			texFolderDirs.AddRange(Directory.GetDirectories(texturesPath, "*", SearchOption.AllDirectories));
			texFolderDirs.Add(texturesPath);
			
			foreach (string dir in texFolderDirs) {
				string[] files = Directory.GetFiles(dir, "*.png");
				
				foreach (string file in files) {
					string texDataPath = Path.ChangeExtension(file, ".json");
					
					SpriteData sprData = new SpriteData();
					if (File.Exists(texDataPath)) {
						sprData = JsonConvert.DeserializeObject<SpriteData>(File.ReadAllText(texDataPath));
					}
					Sprite spr = AssetLoader.SpriteFromFile(file,
					                                        new Vector2(sprData.offsets[0], sprData.offsets[1]),
					                                        sprData.pixelsPerUnit);
					if (string.IsNullOrEmpty(sprData.key)) {
						sprData.key = spr.name;
					}
					assetMan.Add<Sprite>(sprData.key, spr);
				}
			}
			// end of Sprite loading
			// Audio Loading
			List<string> audFolderDirs = new List<string>();
			audFolderDirs.AddRange(Directory.GetDirectories(audioPath, "*", SearchOption.AllDirectories));
			audFolderDirs.Add(audioPath);
			
			foreach (string dir in audFolderDirs) {
				string[] files = Directory.GetFiles(dir, "*.wav");
				
				foreach (string file in files) {
					string audDataPath = Path.ChangeExtension(file, ".json");
					AudioData audData = new AudioData();
					
					if (File.Exists(audDataPath)) {
						audData = JsonConvert.DeserializeObject<AudioData>(File.ReadAllText(audDataPath));
					}
					SoundObject so = ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromFile(file),
					                                                  audData.caption,
					                                                  EnumExtensions.GetFromExtendedName<SoundType>(audData.soundType),
					                                                  new Color(audData.captionColor[0],audData.captionColor[1],audData.captionColor[2],audData.captionColor[3]),
					                                                  audData.sublength);
					
					
					if (string.IsNullOrEmpty(audData.key)) {
						audData.key = so.name;
					}
					assetMan.Add<SoundObject>(audData.key, so);
				}
			}
			// end of Audio loading
			// Item building
			string itemDataDirPath = Path.Combine(dataPath, "items");
			if (Directory.Exists(itemDataDirPath)) {
				foreach (string itemDataPath in Directory.GetFiles(itemDataDirPath, "*.json", SearchOption.AllDirectories)) {
					LuaItemObjectData dat = JsonConvert.DeserializeObject<LuaItemObjectData>(File.ReadAllText(itemDataPath));
					string itemScriptPath = Path.Combine(scriptsPath, "items", Path.GetFileNameWithoutExtension(itemDataPath) + ".lua");
					if (File.Exists(itemScriptPath)) {
						dat.scriptFilePath = itemScriptPath;
					} else {
						Debug.LogError("Item does not have a script file inside the modpack's script/items folder. Did you forgot to make one?");
						return;
					}
					itemDatas.Add(dat);
					LuaObjectBuilder.Create(dat, this);
				}
			}
				
			// end of Item building
			// NPC building
			string npcDataDirPath = Path.Combine(dataPath, "npcs");
			if (Directory.Exists(npcDataDirPath)) {
				foreach (string npcDataPath in Directory.GetFiles(npcDataDirPath, "*.json", SearchOption.AllDirectories))
				{
					LuaNPCData npcDat = JsonConvert.DeserializeObject<LuaNPCData>(File.ReadAllText(npcDataPath));
					string npcScriptPath = Path.Combine(scriptsPath, "npcs", Path.GetFileNameWithoutExtension(npcDataPath) + ".lua");
					if (File.Exists(npcScriptPath)) {
						npcDat.scriptFilePath = npcScriptPath;
					} else { 
						Debug.LogError("NPC does not have a script file inside the modpack's script/npcs folder. Did you forgot to make one?");
						return;
					}
					npcDatas.Add(npcDat);
					LuaObjectBuilder.CreateNPC(npcDat, this);
					Log(npcDat.name + " is made");
				}
			}
		}
		
		IEnumerator OnAssetLoad() {
			yield return 3;
			yield return "Loading modpack contents > [" + data.name + "]";
			AddAllAssets();
			GeneratorManagement.Register(LuaModdingPlugin.Instance, GenerationModType.Addend, OnGeneratorRegister);
			yield break;
		}
		
		public void OnGeneratorRegister(string levelName, int levelNo, SceneObject scene) {
			foreach (var clo in scene.GetCustomLevelObjects()) {
				foreach (LuaItemObjectData dat in itemDatas) {
					if (!dat.generatorProperties.generateInEveryFloor) {
						foreach (string floor in dat.generatorProperties.generateInFloors) {
							if (levelName == floor) {
								clo.potentialItems = clo.potentialItems.AddToArray(dat.generatorProperties.weightedSelf);
								Log("Item is in specified floor. {" + floor + "}");
							}
						}
					} else {
						clo.potentialItems = clo.potentialItems.AddToArray(dat.generatorProperties.weightedSelf);
					}
				}
				
				foreach (LuaNPCData dat in npcDatas) {
					if (!dat.generatorProperties.generateInEveryFloor) {
						foreach (string floor in dat.generatorProperties.generateInFloors) {
							if (levelName == floor) {
								scene.potentialNPCs.Add(dat.generatorProperties.weightedSelf);
								Log("NPC is in specified floor. {" + floor + "}");
							}
						}
					} else {
						scene.potentialNPCs.Add(dat.generatorProperties.weightedSelf);
					}
				}
			}
		}
		
		// moved these logging thingies here
		public void Log(object m) {
			Debug.Log(string.Format("[{0}] {1}", data.name, m));
		}
		public void LogWarning(object m) {
			Debug.LogWarning(string.Format("[{0}] {1}", data.name, m));
		}
		public void LogError(object m) {
			Debug.LogError(string.Format("[{0}] {1}", data.name, m));
		}
		
	}
}
