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
    [CreateAssetMenu(menuName = "Game Item/Weapon")]
    public class Weapon : ScriptableObject
    {
        [Header("Appearance")]
        [SerializeField] string m_Name = string.Empty;
        [SerializeField] Sprite m_Icon = null;

        [Header("Stats")]
        public float m_Range = 1f;
        public float m_Rate = 1f;
        public float m_Force = 15f;
        public int m_Damage = 50;

        [Header("Children")]
        [SerializeField] GameObject m_Projectile = null;
    }
}
