using Smod2.Attributes;

namespace Buff106
{
	[PluginDetails(
	author = "Cyanox",
	name = "Buff106",
	description = "Buffs 106",
	id = "cyan.buff106",
	version = "1.0.0",
	SmodMajor = 3,
	SmodMinor = 0,
	SmodRevision = 0
	)]
	public class Plugin : Smod2.Plugin
	{
		public override void OnDisable() { }

		public override void OnEnable() { }

		public override void Register()
		{
			AddEventHandlers(new EventHandler(this));

			AddConfig(new Smod2.Config.ConfigSetting("b106_touch_health", 150, false, true, ""));
			AddConfig(new Smod2.Config.ConfigSetting("b106_touch_damage", 30, false, true, ""));
			AddConfig(new Smod2.Config.ConfigSetting("b106_touch_range", 1f, false, true, ""));
			AddConfig(new Smod2.Config.ConfigSetting("b106_touch_refresh", 0.3f, false, true, ""));
			AddConfig(new Smod2.Config.ConfigSetting("b106_affect_tutorials", false, false, true, ""));
			AddConfig(new Smod2.Config.ConfigSetting("b106_pd_death_health", 10, false, true, ""));
		}
	}
}
