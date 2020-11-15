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

using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StudioJamNov2020.Entities.Player
{
	public class PlayerController : MonoBehaviour
	{
		PlayerControls m_Controls;
		[SerializeField] LineRenderer m_LineRenderer;
		[HideInInspector] UnitController m_Unit;

		private void OnEnable() => m_Controls.Player.Enable();
		private void OnDisable() => m_Controls.Player.Disable();

		void Awake() => m_Controls = new PlayerControls();

		void Start()
		{
			var cameraLook = FindObjectOfType<CinemachineVirtualCamera>();
			cameraLook.m_Follow = transform;
			cameraLook.m_LookAt = transform;
		}

		void FixedUpdate()
		{
			if (Mouse.current.leftButton.isPressed)
			{
				var cursorPoint = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

				if (Physics.Raycast(cursorPoint, out RaycastHit hit))
				{
					if (hit.collider.gameObject.CompareTag("Destructible") || hit.collider.gameObject.CompareTag("Enemy"))
					{
						if (canShoot)
						{
							transform.LookAt(hit.point, Vector3.up);
							CmdFire();
						}
					}
					else Move(hit.point);
				}
			}
			else
				m_LineRenderer.enabled = false;
		}
		
		void Move(Vector3 newPosition)
		{
			// enable and reposition the visual aid line
			m_LineRenderer.enabled = true;
			m_LineRenderer.SetPosition(0, transform.position);
			m_LineRenderer.SetPosition(1, Vector3.MoveTowards(transform.position, newPosition, Mathf.Infinity));

			m_Unit.m_NavMeshAgent.SetDestination(newPosition);
		}
	}
}
