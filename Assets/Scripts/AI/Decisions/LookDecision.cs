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

using UnityEngine;

namespace StudioJamNov2020.AI.Decisions
{
	[CreateAssetMenu(menuName = "AI/Decisions/Look")]
	public class LookDecision : Decision
	{
		public override bool Decide(StateController controller)
		{
			Debug.DrawRay(controller.m_Eyes.position, controller.m_Eyes.forward.normalized, Color.green);

			if (Physics.SphereCast(controller.m_Eyes.position, controller.m_Unit.m_LookSphereCastRadius, controller.m_Eyes.forward, out RaycastHit hit,
				controller.m_Unit.m_LookRange) && hit.collider.CompareTag("Player"))
			{
				controller.m_Target = hit.transform;
				return true;
			}

			return false;
		}
	}
}
