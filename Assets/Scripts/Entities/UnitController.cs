﻿/*
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
using UnityEngine.AI;

namespace StudioJamNov2020.Entities
{
    public class UnitController : MonoBehaviour
    {
        [Header("Movement")]
        public float m_MoveSpeed = 1f;
        public float m_TurnSpeed = 150f;

        [HideInInspector] public NavMeshAgent m_NavMeshAgent;

        private void Awake() => m_NavMeshAgent = GetComponent<NavMeshAgent>();

		// to be moved
		/*IEnumerator Fire()
        {
            canShoot = false;
            var bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * attackForce, ForceMode.Impulse);
            yield return new WaitForSeconds(attackRate);
            canShoot = true;
        }*/
	}
}