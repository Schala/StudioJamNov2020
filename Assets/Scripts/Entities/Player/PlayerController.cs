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
using UnityEngine.InputSystem;

namespace StudioJamNov2020.Entities.Player
{
	public class PlayerController : MonoBehaviour
	{
		UnitController m_Unit;
		Combatant m_Combatant;
		RaycastHit m_LastHit;

		private void Awake()
		{
			m_Unit = GetComponent<UnitController>();
			m_Combatant = GetComponent<Combatant>();
		}

		void FixedUpdate()
		{
			if (Mouse.current.leftButton.isPressed)
			{
				var cursorPoint = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

				if (Physics.Raycast(cursorPoint, out m_LastHit))
				{
					if (m_LastHit.collider.CompareTag("Destructible") || m_LastHit.collider.CompareTag("Enemy"))
						m_Combatant.Attack(m_LastHit.collider.gameObject);
					else m_Unit.Move(m_LastHit.point);
				}
			}
		}
	}
}
