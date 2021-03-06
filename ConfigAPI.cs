using System;
using System.Collections.Generic;
using System.IO;
using GameSave;
using UnityEngine;
using Mod;

public static partial class ConfigAPI {
	public static void Init(string name, object config) {
		ConfigAPI.Load(name, config);
		ConfigAPI.EchoAll(name, config);
		configs.Add(name, config);
	}
	public static void Init(string name, object config, string echo) {
		ConfigAPI.Load(name, config);
		ConfigAPI.EchoAll(name, config);
		Debug.Log("ConfigAPI:"+name+": "+echo);
		configs.Add(name, config);
	}
	
	public static void Load(string name, object config) {
		Directory.CreateDirectory(configFolder);
		if (File.Exists(configFolder+"\\"+name+".ini")) {
			try {
				JsonUtility.FromJsonOverwrite(File.ReadAllText(configFolder+"\\"+name+".ini"), config);
			} catch (Exception ex) {
				Debug.LogError("ConfigAPI:"+name+": Error reading config file");
				Debug.LogError(ex.Message + Environment.NewLine + ex.StackTrace);
				Singleton<ModContentManager>.Instance.AddErrorLog("ConfigAPI:"+name+": Config file invalid, resetting it");
			}
		}
        File.WriteAllText(configFolder+"\\"+name+".ini", JsonUtility.ToJson(config, true));
	}
	public static void EchoAll(string name, object config) {
		Debug.Log("ConfigAPI:"+name+": "+JsonUtility.ToJson(config, true));
	}

	public static Dictionary<string, object> configs = new Dictionary<string, object>();
    readonly static public string configFolder = SaveManager.GetFullPath("ModConfigs");
}