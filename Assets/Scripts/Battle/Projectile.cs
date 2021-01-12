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

namespace StudioJamNov2020.Battle
{
    public class Projectile : MonoBehaviour
    {
		[HideInInspector] public Weapon m_Parent = null;
		[HideInInspector] public float m_Lifetime = 0f;

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out Combatant combatant))
				if (combatant.m_Faction != m_Parent.m_Owner.m_Faction)
					combatant.TakeDamage(Random.Range(Mathf.Max(m_Parent.m_Damage - 3, 1), m_Parent.m_Damage + 3));
			gameObject.SetActive(false);
		}

		private void Update()
		{
			m_Lifetime -= Time.deltaTime;

			if (m_Lifetime <= 0f)
				gameObject.SetActive(false);
		}

		private void OnEnable() => GetComponent<Rigidbody>().isKinematic = false;

		private void OnDisable()
		{
			GetComponent<Rigidbody>().isKinematic = true;
			transform.position = Vector3.zero;
			m_Parent = null;
		}
	}
}
