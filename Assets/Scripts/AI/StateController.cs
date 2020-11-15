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

using StudioJamNov2020.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace StudioJamNov2020.AI
{
    public class StateController : MonoBehaviour
    {
        public Transform m_Eyes;
        public List<Transform> m_Waypoints;
        public bool m_AIActive;

        [Header("States")]
        public State m_CurrentState;
        public State m_RemainState;

        [Header("Looking")]
        public float m_LookSphereCastRadius = 1f;
        public float m_LookRange = 40f;

        [Header("Searching")]
        public float m_SearchDuration = 4f;

        [HideInInspector] public UnitController m_Unit;
        [HideInInspector] public int m_NextWaypoint;
        [HideInInspector] public Transform m_Target;
        [HideInInspector] public float m_StateTimeElapsed;

        private void Awake() => m_Unit = GetComponent<UnitController>();

        void Update()
        {
            if (!m_AIActive) return;
            m_CurrentState.UpdateState(this);
        }

        private void Start() => m_Unit.m_NavMeshAgent.enabled = m_AIActive;

        private void OnDrawGizmos()
        {
            if (m_CurrentState == null || m_Eyes == null || m_Unit == null) return;
            Gizmos.color = m_CurrentState.m_SceneGizmoColor;
            Gizmos.DrawWireSphere(m_Eyes.position, m_LookSphereCastRadius);
        }

        public void TransitionToState(State newState)
        {
            if (newState == m_RemainState) return;
            m_CurrentState = newState;
            OnExitState();
        }

        public bool CheckIfCountDownElapsed(float duration)
        {
            m_StateTimeElapsed += Time.deltaTime;
            return m_StateTimeElapsed >= duration;
        }

        private void OnExitState() => m_StateTimeElapsed = 0f;
    }
}
