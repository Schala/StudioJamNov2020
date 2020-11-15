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

using UnityEngine;

namespace StudioJamNov2020.AI.Actions
{
	[CreateAssetMenu(menuName = "AI/Actions/Move")]
	public class MoveAction : Action
	{
		public override void Act(StateController controller)
		{
			controller.m_Unit.m_NavMeshAgent.SetDestination(controller.m_Waypoints[controller.m_NextWaypoint].position);
			controller.m_Unit.m_NavMeshAgent.isStopped = false;

			if (controller.m_Unit.m_NavMeshAgent.remainingDistance <= controller.m_Unit.m_NavMeshAgent.stoppingDistance && !controller.m_Unit.m_NavMeshAgent.pathPending)
				controller.m_NextWaypoint = (controller.m_NextWaypoint + 1) % controller.m_Waypoints.Count;
		}
	}
}