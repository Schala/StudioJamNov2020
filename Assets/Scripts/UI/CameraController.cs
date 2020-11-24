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

using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StudioJamNov2020.UI
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] float m_ZoomSpeed = 0.05f;
        [SerializeField] float m_StartZoom = 10f;
        [SerializeField] float m_MinZoom = 20f;
        [SerializeField] float m_MaxZoom = 5f;
        CinemachineFollowZoom m_FollowZoom;

        void Awake() => m_FollowZoom = GetComponent<CinemachineFollowZoom>();
        void Start() => m_FollowZoom.m_Width = m_StartZoom;

        public void LateUpdate()
        {
            var scrollInput = Mouse.current.scroll.ReadValue();
            if (scrollInput.y == 0) return;
            m_FollowZoom.m_Width += scrollInput.y * -m_ZoomSpeed;
            m_FollowZoom.m_Width = Mathf.Clamp(m_FollowZoom.m_Width, m_MaxZoom, m_MinZoom);
        }
    }
}
