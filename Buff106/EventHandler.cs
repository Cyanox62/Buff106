using MEC;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Linq;
using System.Collections.Generic;

namespace Buff106
{
	partial class EventHandler : IEventHandlerSetRole, IEventHandlerPlayerHurt, IEventHandlerRoundStart, IEventHandlerRoundEnd, IEventHandlerWaitingForPlayers, IEventHandlerPocketDimensionDie
	{
		private Plugin instance;
		private float scpMaxHealth;
		private bool isRoundStarted;
		private Dictionary<int, bool> scpList = new Dictionary<int, bool>();

		// Configs
		private float touchHealth;
		private int touchDamage;
		private float touchRange;
		private float touchRefresh;
		private bool affectTutorials;
		private int pdDieHealth;

		public EventHandler(Plugin plugin) => instance = plugin;

		public void OnWaitingForPlayers(WaitingForPlayersEvent ev) => LoadConfigs();
		public void OnRoundStart(RoundStartEvent ev) => isRoundStarted = true;
		public void OnRoundEnd(RoundEndEvent ev)
		{
			isRoundStarted = false;
			scpList.Clear();
		}

		public void OnSetRole(PlayerSetRoleEvent ev)
		{
			if (ev.Player.TeamRole.Role == Role.SCP_106 && !scpList.ContainsKey(ev.Player.PlayerId))
			{
				scpMaxHealth = ev.Player.GetHealth();
				scpList.Add(ev.Player.PlayerId, false);
			}
			else if (scpList.ContainsKey(ev.Player.PlayerId))
			{
				scpList.Remove(ev.Player.PlayerId);
			}
		}

		public void OnPlayerHurt(PlayerHurtEvent ev)
		{
			int hp = ev.Player.GetHealth();
			if (ev.Player.TeamRole.Role == Role.SCP_106 && hp > 0 && hp <= touchHealth && isRoundStarted && scpList.ContainsKey(ev.Player.PlayerId) && !scpList[ev.Player.PlayerId])
			{
				scpList[ev.Player.PlayerId] = true;
				Timing.RunCoroutine(CheckPlayerTouch(ev.Player));
			}
		}

		public void OnPocketDimensionDie(PlayerPocketDimensionDieEvent ev)
		{
			foreach (Player player in instance.Server.GetPlayers().Where(x => x.TeamRole.Role == Role.SCP_106))
			{
				player.AddHealth(pdDieHealth);
			}
		}
	}
}
