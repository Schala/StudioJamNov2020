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
        Elite = 32
    }

    public class Combatant : MonoBehaviour
    {
        public Weapon m_Weapon = null;
        public int m_MaxHealth = 100;
        public int m_MaxMana = 10;

        [Header("Combat")]
        [HideInInspector] public int m_CurrentHealth;
        [HideInInspector] public int m_CurrentMana;
        [HideInInspector] public CombatFlags m_Flags = CombatFlags.None;

        void Start()
        {
            m_CurrentHealth = m_MaxHealth;
            m_CurrentMana = m_MaxMana;
        }

        public void TakeDamage(int amount)
        {
            m_CurrentHealth = Mathf.Max(m_CurrentHealth - amount, 0);
            if (m_CurrentHealth == 0) m_Flags |= CombatFlags.Dead;
        }
    }
}
