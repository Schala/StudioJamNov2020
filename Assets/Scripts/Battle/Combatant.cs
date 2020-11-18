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

using StudioJamNov2020.Entities.Player;
using System;
using System.Collections;
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
        static readonly int PunchHash = Animator.StringToHash("Punch");
        static readonly int PunchVariantHash = Animator.StringToHash("Punch Variant");

        [Header("Combat")]
        public int m_MaxHealth = 100;
        public int m_MaxMana = 10;
        public int m_Strength = 1; // affects melee damage
        public int m_Dexterity = 1; // affects ranged damage
        public CombatFlags m_Flags = CombatFlags.None;

        [HideInInspector] public Weapon m_Weapon = null;
        [HideInInspector] public GameObject m_Target = null;
        [HideInInspector] public int m_CurrentHealth;
        [HideInInspector] public int m_CurrentMana;
        [HideInInspector] public int m_TotalArmor = 0; // damage mitigation
        Animator m_Animator = null;
        public float m_Rate;
        public float m_Range;
        bool m_UnarmedCoolingDown = false;

        private void Awake()
        {
            m_Weapon = GetComponentInChildren<Weapon>();
            m_Animator = GetComponent<Animator>();

            ChangeWeapon();
        }

		void Start()
        {
            m_CurrentHealth = m_MaxHealth;
            m_CurrentMana = m_MaxMana;
        }

        public void ChangeWeapon()
        {
            if (m_Weapon != null)
            {
                m_Rate = m_Weapon.m_Rate;
                m_Range = m_Weapon.m_Range;
            }
            else // unarmed
            {
                m_Rate = 1f;
                m_Range = 2f;
            }
        }

        public bool IsInRange() => Vector3.Distance(transform.position, m_Target.transform.position) < m_Range;

		private void Update()
		{
		}

		public void TakeDamage(int amount)
        {
            if (m_Flags.HasFlag(CombatFlags.Invulnerable)) return;

            m_CurrentHealth = Mathf.Max(m_CurrentHealth - amount, 0);

            if (m_CurrentHealth == 0)
            {
                m_Flags |= CombatFlags.Dead;

                if (!CompareTag("Player"))
                {
                    GetComponent<Collider>().enabled = false; // prevent targeting
                    GetComponent<Entity>().m_IsActive = true; // decay and fade away
                }
            }
        }

        public void InvokeAttack() => Invoke(nameof(Attack), m_Rate);

        public void Attack()
        {
            if (m_Weapon == null) // unarmed
            {
                    var punchVariant = Mathf.RoundToInt(UnityEngine.Random.value * 5f);
                    print("blep!");
                    m_Animator.SetInteger(PunchVariantHash, punchVariant);
                    m_Animator.SetTrigger(PunchHash);
            }
            else
            {
            }

            m_Target = null;
        }
    }
}
