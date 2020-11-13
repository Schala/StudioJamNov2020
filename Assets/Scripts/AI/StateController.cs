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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StudioJamNov2020.AI
{
    public class StateController : MonoBehaviour
    {
        public State m_CurrentState;
        public Transform m_Eyes;
        public State m_RemainState;
        [HideInInspector] public NavMeshAgent m_NavMeshAgent;
        [HideInInspector] public UnitController m_Unit;
        public List<Transform> m_Waypoints;
        [HideInInspector] public int m_NextWaypoint;
        [HideInInspector] public Transform m_Target;
        [HideInInspector] public float m_StateTimeElapsed;
        public bool m_AIActive;

        private void Awake()
        {
            m_Unit = GetComponent<UnitController>();
            m_NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (!m_AIActive) return;
            m_CurrentState.UpdateState(this);
        }

        private void Start() => m_NavMeshAgent.enabled = m_AIActive;

        private void OnDrawGizmos()
        {
            if (m_CurrentState == null || m_Eyes == null || m_Unit == null) return;
            Gizmos.color = m_CurrentState.m_SceneGizmoColor;
            Gizmos.DrawWireSphere(m_Eyes.position, m_Unit.m_LookSphereCastRadius);
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
