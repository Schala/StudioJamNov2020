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

using System;
using UnityEngine;

namespace StudioJamNov2020.Battle
{
    [Flags]
    public enum CombatFlags : byte
    {
        None = 0,
        Dead = 1,
		Incapacitated = 2,
		Poisoned = 4,
        Possessed = 8,
        Invulnerable = 16,
        Elite = 32,
        Boss = 64
    }

    public class Combatant : MonoBehaviour
    {
        static readonly GameObject UnarmedPrefab = Resources.Load<GameObject>("Prefabs/Unarmed");

        [Header("Combat")]
        public int m_MaxHealth = 100;
        public int m_MaxMana = 10;
        public int m_Strength = 1; // affects melee damage
        public int m_Dexterity = 1; // affects ranged damage

        [HideInInspector] public Weapon m_Weapon = null;
        [HideInInspector] public int m_CurrentHealth;
        [HideInInspector] public int m_CurrentMana;
        [HideInInspector] public int m_TotalArmor = 0; // damage mitigation
        [HideInInspector] public CombatFlags m_Flags = CombatFlags.None;

        private void Awake() => m_Weapon = GetComponentInChildren<Weapon>();

		void Start()
        {
            m_CurrentHealth = m_MaxHealth;
            m_CurrentMana = m_MaxMana;

            if (m_Weapon == null)
                // Don't need the instance itself, just the Weapon component. Let's spawn it at the parent's origin, though.
                m_Weapon = Instantiate(UnarmedPrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<Weapon>();
        }

        public void TakeDamage(int amount)
        {
            m_CurrentHealth = Mathf.Max(m_CurrentHealth - amount, 0);
            if (m_CurrentHealth == 0) m_Flags |= CombatFlags.Dead;
        }
    }
}
