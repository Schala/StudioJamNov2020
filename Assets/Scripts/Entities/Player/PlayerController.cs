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
using UnityEngine.InputSystem;

namespace StudioJamNov2020.Entities.Player
{
	public class PlayerController : MonoBehaviour
	{
		[HideInInspector] UnitController m_Unit;

		private void Awake() => m_Unit = GetComponent<UnitController>();

		void FixedUpdate()
		{
			if (Mouse.current.leftButton.isPressed)
			{
				var cursorPoint = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

				if (Physics.Raycast(cursorPoint, out RaycastHit hit))
				{
					if (!(hit.collider.gameObject.CompareTag("Destructible") || hit.collider.gameObject.CompareTag("Enemy")))
						Move(hit.point);
				}
			}
		}
		
		void Move(Vector3 newPosition) => m_Unit.m_NavMeshAgent.SetDestination(newPosition);
	}
}
