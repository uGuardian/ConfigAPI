using System;

// REQUIRED: Rename the namespace to your own
namespace Template {
	public class ConfigInitializer : ModInitializer {
		// REQUIRED: You need to either call the included method InitConfig or ConfigAPI.Init in a ModInitializer
		// The included method InitConfig stops your modinitializer from crashing or throwing an error if ConfigAPI isn't loaded
		public override void OnInitializeMod() {
			// REQUIRED: Put the name of your config file, and then the instance of the config class
			this.InitConfig("Template", TemplateConfig.Instance);
		}


		// Checks to see if ConfigAPI is enabled, and then inits the config.
		internal void InitConfig(string name, object config) {
			List<String> assembly = new List<String>();
			foreach (var a in AppDomain.CurrentDomain.GetAssemblies()) {
				assembly.Add(a.GetName().Name);
			}
			if (assembly.Contains("ConfigAPI")) {
				var tempInstance = new ConfigHandler();
				tempInstance.Init(name, config);
			}
		}
		// The actual init code is called in a separate class by InitConfig to prevent errors.
		internal class ConfigHandler {
			internal void Init(string name, object config) {
				// If you directly call this yourself, put the name of your config file, and then the instance of the config class
				ConfigAPI.Init(name, config);
			}
		}
	}

	[Serializable]
	// REQUIRED: Change the class name
	public class TemplateConfig {
		// REQUIRED: Add your variables and their default values here, and call them via the instance
		// Only public, non-static variables are officially supported
		// Officially supported types are Bool, String, and most standard number types like Int
		// Examples below:
		public bool myBool = false;
		public int myInt = 0;
		public string myString = "myString";

		// REQUIRED: Change both TemplateConfig to whatever the name of your class is
		public static TemplateConfig Instance = new TemplateConfig();
	}
}