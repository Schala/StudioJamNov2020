/*
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3, or (at your option)
 * any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street - Fifth Floor, Boston, MA 02110-1301, USA.
 */

using StudioJamNov2020.Battle;
using UnityEngine;

namespace StudioJamNov2020.AI.Actions
{
	[CreateAssetMenu(menuName = "AI/Actions/Attack")]
	public class AttackAction : Action
	{
		public override void Act(StateController controller)
		{
			var combatant = controller.GetComponent<Combatant>();
			var attackRange = combatant.m_Weapon == null ? 1f : combatant.m_Weapon.m_Range;
			var attackRate = combatant.m_Weapon == null ? 1f : combatant.m_Weapon.m_Rate;

			Debug.DrawRay(controller.m_Eyes.position, controller.m_Eyes.forward.normalized * attackRange, Color.red);

			if (Physics.SphereCast(controller.m_Eyes.position, controller.m_LookSphereCastRadius, controller.m_Eyes.forward, out RaycastHit hit, attackRange)
				&& hit.collider.CompareTag("Player"))
			{
				if (controller.CheckIfCountDownElapsed(attackRate))
					; // set an attack animation trigger here
			}
		}
	}
}
