using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HarmonyLib;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Mod;

public static partial class ConfigAPI {
	public class Initializer : ModInitializer {
        public override void OnInitializeMod() {
			Harmony harmony = new Harmony("LoR.uGuardian.ConfigAPI");
			harmony.PatchAll();
			RemoveError();
		}
	}
	private readonly static string resources = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+"\\..\\Resource";

	[HarmonyPatch(typeof(UIOptionWindow), "Open")]
	class patch {
		[HarmonyPostfix]
		public static void UIOptionWindow_Open(UIOptionWindow __instance)
		{
			try
			{
				if (UtilTools.DefFont == null)
				{
					UtilTools.DefFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
					UtilTools.DefFontColor = UIColorManager.Manager.GetUIColor(UIColor.Default);
				}
				if (UtilTools.DefFont_TMP == null)
				{
					UtilTools.DefFont_TMP = __instance.displayDropdown.itemText.font;
				}
				if (ModButton.btninstance == null)
				{
					Button button = UtilTools.CreateButton(__instance.root.transform, resources+"\\Image\\ModButton.png");
					button.gameObject.transform.localPosition = new Vector3(0f, -460f);
					button.gameObject.SetActive(false);
					ModButton.btninstance = button.gameObject.AddComponent<ModButton>();
					button.onClick.AddListener(delegate()
					{
						ModButton.btninstance.OnClick();
					});
					ModButton.btninstance.__instance = __instance;
					button.gameObject.SetActive(true);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message + Environment.NewLine + ex.StackTrace);
			}
		}
	}

	//Borrowed duplicate assembly error removal
	public static void RemoveError()
    {
        var list = new List<string>();
        var list2 = new List<string>();
        list.Add("0Harmony");
        using (var enumerator = Singleton<ModContentManager>.Instance.GetErrorLogs().GetEnumerator())
        {
            while (enumerator.MoveNext())
            {
                var errorLog = enumerator.Current;
                if (list.Exists(x => errorLog.Contains(x)))
                {
                    list2.Add(errorLog);
                }
            }
        }

        foreach (var item in list2)
        {
            Singleton<ModContentManager>.Instance.GetErrorLogs().Remove(item);
        }
    }
}