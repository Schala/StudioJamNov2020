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

using System.Collections;
using UnityEngine;

namespace StudioJamNov2020.Battle
{
    public enum WeaponType : byte
    {
        Knuckles = 0,
        Pistol,
        Rifle,
        OneHandedMelee,
        TwoHandedMelee,
        Thrown
    }

    public class Weapon : MonoBehaviour
    {
        [Header("Stats")]
        public float m_Range = 1f;
        public float m_Rate = 1f;
        public float m_Force = 15f;
        public float m_Durability = 100f;
        public int m_Damage = 50;
        public WeaponType m_Type = WeaponType.Knuckles;
        public AudioClip[] m_Audio = null;
        [HideInInspector] public Combatant m_Owner = null;
        AudioSource m_AudioSource = null;
        GameManager m_Game = null;

        [Header("Ranged")]
        [SerializeField] Transform m_ProjectileSpawnPoint = null;
        [SerializeField] float m_projectileSpeed = 5000f;
        [SerializeField] float m_projectileLifetime = 3f;

        private void Awake()
        {
            m_AudioSource = GetComponent<AudioSource>();
            m_Game = FindObjectOfType<GameManager>();
        }

		private void Start() => gameObject.SetActive(false);

		private void OnTriggerEnter(Collider other)
		{
            if (other.TryGetComponent(out Combatant combatant))
                if (combatant.m_Faction != m_Owner.m_Faction)
                    combatant.TakeDamage(Random.Range(Mathf.Max(m_Damage - 15, 1), m_Damage + 5));
		}

		public void Shoot()
        {
            if (m_Audio != null) m_AudioSource.clip = m_Audio[Random.Range(0, m_Audio.Length - 1)];
            m_AudioSource.pitch = Random.Range(0.5f, 1.5f);
            m_AudioSource.Play();
            var bullet = m_Game.m_Pool.Get("Bullet");
            if (bullet != null)
            {
                bullet.SetActive(true);
                var behavior = bullet.GetComponent<Projectile>();
                bullet.transform.position = m_ProjectileSpawnPoint.position;
                bullet.transform.rotation = Quaternion.LookRotation(m_Owner.transform.forward);
                behavior.m_Parent = this;
                behavior.m_Lifetime = m_projectileLifetime;
                bullet.GetComponent<Rigidbody>().AddForce(m_Owner.transform.forward * m_projectileSpeed, ForceMode.Impulse);
            }
        }
	}
}
