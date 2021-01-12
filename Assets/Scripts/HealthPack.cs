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

public class HealthPack : MonoBehaviour
{
	[SerializeField] int m_Health = 50;
	[SerializeField] AudioClip m_Collect = null;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.TryGetComponent(out Combatant combatant))
			if (combatant.CompareTag("Player"))
			{
				combatant.TakeDamage(-m_Health); // negative damage = heal
				AudioSource.PlayClipAtPoint(m_Collect, transform.position, 1f);
				Destroy(gameObject);
			}
	}
}
