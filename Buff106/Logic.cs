using Smod2.API;
using System.Collections.Generic;
using System.Linq;
using MEC;

namespace Buff106
{
	partial class EventHandler
	{
		private void LoadConfigs()
		{
			touchHealth = instance.GetConfigInt("b106_touch_health");
			touchDamage = instance.GetConfigInt("b106_touch_damage");
			touchRange = instance.GetConfigFloat("b106_touch_range");
			touchRefresh = instance.GetConfigFloat("b106_touch_refresh");
			affectTutorials = instance.GetConfigBool("b106_affect_tutorials");
			pdDieHealth = instance.GetConfigInt("b106_pd_death_health");
		}

		private void SendToPD(Player player)
		{
			player.Damage(touchDamage, DamageType.SCP_106);
			player.Teleport(Vector.Down * 1997f);
		}

		private IEnumerator<float> CheckPlayerTouch(Player scp106)
		{
			while (scp106.GetHealth() <= touchHealth && isRoundStarted && scp106?.TeamRole.Role == Role.SCP_106)
			{
				IEnumerable<Player> pList = instance.Server.GetPlayers().Where(x => x.TeamRole.Team != Smod2.API.Team.SCP);
				if (!affectTutorials) pList = pList.Where(x => x.TeamRole.Team != Smod2.API.Team.TUTORIAL);
				foreach (Player player in pList.Where(x => Vector.Distance(scp106.GetPosition(), x.GetPosition()) <= touchRange))
				{
					SendToPD(player);
				}
				yield return Timing.WaitForSeconds(touchRefresh);
			}
		}
	}
}
