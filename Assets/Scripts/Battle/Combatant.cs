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

using StudioJamNov2020.AI;
using StudioJamNov2020.Entities;
using StudioJamNov2020.UI;
using System;
using System.Collections;
using TMPro;
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
        static readonly int AttackHash = Animator.StringToHash("Attack");
        static readonly int PunchVariantHash = Animator.StringToHash("Punch Variant");
        static readonly int Melee2HVariant = Animator.StringToHash("2H Melee Variant");
        static readonly int AttackingHash = Animator.StringToHash("Attacking");

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
        public int m_Faction = 0;

        [Header("Audio")]
        public AudioClip[] m_Death = null;

        [HideInInspector] public GameObject m_Target = null;
        [HideInInspector] public int m_CurrentHealth;
        [HideInInspector] public int m_CurrentMana;
        [HideInInspector] public int m_TotalArmor = 0; // damage mitigation
        [HideInInspector] public float m_Rate;
        [HideInInspector] public float m_Range;
        Animator m_Animator = null;
        public HealthBar m_HealthBar = null;
        bool m_OnCoolDown = false;
        bool m_DeathAudioPlayed = false;

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
            m_HealthBar.Current = m_MaxHealth;
            if (m_Weapon != null && m_SecondaryWeapon != null) UpdateWeapon();
            if (m_Weapon != null) m_Weapon.m_Owner = this;
            if (m_SecondaryWeapon != null) m_SecondaryWeapon.m_Owner = this;
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
            m_HealthBar.Current = m_CurrentHealth;

            if (m_CurrentHealth <= 0)
            {
                m_Animator.enabled = false;
                m_HealthBar.Current = 0;
                m_Flags |= CombatFlags.Dead;

                if (!CompareTag("Player"))
                {
                    GetComponent<StateController>().enabled = false;
                    GetComponent<Collider>().enabled = false; // prevent targeting
                    GetComponent<UnitController>().Stop();
                    if (!m_DeathAudioPlayed && TryGetComponent(out AudioSource audio))
                    {
                        var gameMgr = FindObjectOfType<GameManager>();
                        var scoreText = gameMgr.m_ScoreText.GetComponent<TMP_Text>();
                        gameMgr.m_Score += 50;
                        scoreText.text = gameMgr.m_Score.ToString();
                        audio.Stop();
                        audio.clip = m_Death[0];
                        audio.Play();
                        m_DeathAudioPlayed = true;
                    }
                    Destroy(gameObject, 5);
                }
            }
        }

        public void ShowWeapons(bool showing)
        {
            m_Weapon.gameObject.SetActive(showing);
            m_SecondaryWeapon.gameObject.SetActive(showing);
        }

		public void Shoot() => m_Weapon.Shoot();

        public IEnumerator Attack()
        {
            if (m_OnCoolDown) yield break;

            ShowWeapons(true);
            m_Animator.SetLayerWeight(0, 0f);

            switch (m_Weapon.m_Type)
            {
                case WeaponType.Knuckles:
                    var punchVariant = Mathf.RoundToInt(UnityEngine.Random.Range(0, 4));
                    m_Animator.SetLayerWeight(1, 1f);
                    m_Animator.SetInteger(PunchVariantHash, punchVariant);
                    m_Animator.SetTrigger(AttackHash);
                    break;
                case WeaponType.TwoHandedMelee:
                    var melee2HVariant = Mathf.RoundToInt(UnityEngine.Random.Range(0, 2));
                    m_Animator.SetLayerWeight(2, 1f);
                    m_Animator.SetInteger(Melee2HVariant, melee2HVariant);
                    m_Animator.SetTrigger(AttackHash);
                    break;
                case WeaponType.Pistol:
                    m_Animator.SetLayerWeight(3, 1f);
                    m_Animator.SetBool(AttackingHash, true);
                    break;
                default: break;
            }

            m_OnCoolDown = true;

            yield return new WaitForSeconds(m_Rate);

            m_OnCoolDown = false;
        }

        public void Disengage()
        {
            ShowWeapons(false);
            m_Animator.SetBool(AttackingHash, false);
            m_Animator.SetLayerWeight(0, 1f);

            switch (m_Weapon.m_Type)
            {
                case WeaponType.Knuckles: m_Animator.SetLayerWeight(1, 0f); break;
                case WeaponType.TwoHandedMelee: m_Animator.SetLayerWeight(2, 0f); break;
                case WeaponType.Pistol: m_Animator.SetLayerWeight(3, 0f); break;
                default: break;
            }
        }
    }
}
