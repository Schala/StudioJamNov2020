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
        public Weapon m_Weapon = null;
        public Weapon m_SecondaryWeapon = null;
        public Weapon m_LeftHand = null;
        public Weapon m_RightHand = null;
        public int m_MaxHealth = 100;
        public int m_MaxMana = 10;
        public int m_Strength = 1; // affects melee damage
        public int m_Dexterity = 1; // affects ranged damage
        public CombatFlags m_Flags = CombatFlags.None;

        [HideInInspector] public GameObject m_Target = null;
        [HideInInspector] public int m_CurrentHealth;
        [HideInInspector] public int m_CurrentMana;
        [HideInInspector] public int m_TotalArmor = 0; // damage mitigation
        [HideInInspector] public float m_Rate;
        [HideInInspector] public float m_Range;
        Animator m_Animator = null;
        bool m_OnCoolDown = false;

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            if (m_Weapon == null) m_Weapon = m_LeftHand;
            if (m_SecondaryWeapon == null) m_SecondaryWeapon = m_RightHand;
        }

        private void Update() => Debug.DrawRay(transform.position, transform.forward * m_Range, Color.red);

		void Start()
        {
            m_CurrentHealth = m_MaxHealth;
            m_CurrentMana = m_MaxMana;
            if (m_Weapon != null && m_SecondaryWeapon != null) UpdateWeapon();
        }

        public void UpdateWeapon()
        {
            // find a good middle ground
            m_Rate = (m_Weapon.m_Rate + m_SecondaryWeapon.m_Rate) / 2f;
            m_Range = (m_Weapon.m_Range + m_SecondaryWeapon.m_Rate) / 2f;
        }

        public bool IsInRange()
        {
            if (m_Target == null) return true; // crash mitigation
            return Vector3.Distance(transform.position, m_Target.transform.position) < m_Range;
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

        public void ActivateWeapons()
        {
            m_Weapon.GetComponent<Collider>().enabled = true;
            m_SecondaryWeapon.GetComponent<Collider>().enabled = true;
        }

        public void DeactivateWeapons()
        {
            m_Weapon.GetComponent<Collider>().enabled = false;
            m_SecondaryWeapon.GetComponent<Collider>().enabled = false;
        }

        public IEnumerator Attack()
        {
            if (m_OnCoolDown) yield break;

            switch (m_Weapon.m_Type)
            {
                case WeaponType.Knuckles:
                    var punchVariant = Mathf.RoundToInt(UnityEngine.Random.value * 5f);
                    m_Animator.SetInteger(PunchVariantHash, punchVariant);
                    m_Animator.SetTrigger(PunchHash);
                    break;
                default: break;
            }

            m_OnCoolDown = true;
            if (!gameObject.CompareTag("Enemy")) m_Target = null;

            yield return new WaitForSeconds(m_Rate);
            m_OnCoolDown = false;
        }
    }
}
