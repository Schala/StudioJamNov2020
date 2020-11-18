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
using UnityEngine.AI;

namespace StudioJamNov2020.Entities
{
    public class UnitController : MonoBehaviour
    {
        static readonly int ForwardSpeedHash = Animator.StringToHash("Forward Speed");

        [Header("Movement")]
        public float m_MoveSpeed = 1f;
        public float m_TurnSpeed = 20f;

        [HideInInspector] public NavMeshAgent m_NavMeshAgent;
        Animator m_Animator;

        private void Awake()
        {
            m_NavMeshAgent = GetComponent<NavMeshAgent>();
            m_NavMeshAgent.speed = m_MoveSpeed;
            m_NavMeshAgent.angularSpeed = m_TurnSpeed;
            m_NavMeshAgent.acceleration = m_TurnSpeed * 0.05f;

            m_Animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (m_Animator != null) UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            var localVelocity = transform.InverseTransformDirection(m_NavMeshAgent.velocity);
            var forwardSpeed = localVelocity.z;
            m_Animator.SetFloat(ForwardSpeedHash, forwardSpeed);
        }

        public void Move(Vector3 newPosition)
        {
            m_NavMeshAgent.isStopped = false;
            m_NavMeshAgent.SetDestination(newPosition);
        }

        public bool HasArrived() => m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance && !m_NavMeshAgent.pathPending;

        public void Stop() => m_NavMeshAgent.isStopped = true;
    }
}
