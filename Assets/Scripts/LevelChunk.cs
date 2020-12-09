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

namespace StudioJamNov2020.World
{
    /*public enum LevelChunkType : byte
    {
        Normal = 0,
        Entrance,
        End,
        Exit
    }

    [Flags]
    public enum LevelChunkTypeMask : byte
    {
        Normal = 1,
        Entrance = 2,
        End = 4,
        Exit = 8,
        All = 15
    }

	[Serializable]
    public class LevelChunkEntry
    {
        public LevelChunk m_Chunk;

        [Range(0f, 100f)]
        public float m_MinProbability = 0f;

        [Range(0f, 100f)]
        public float m_MaxProbability = 0f;
    }*/

    public class LevelChunk : MonoBehaviour
    {
        /*public LevelChunkType m_Type = LevelChunkType.Normal;
        public Transform[] m_Waypoints = null;
        public Transform[] m_Exits = null;
        public LevelChunkTypeMask m_ValidChildTypeMask;
        [HideInInspector] public int m_Vacancies = 0;

        private void Awake()
        {
            // All exits are open
            for (int i = 0; i < m_Exits.Length; i++)
                m_Vacancies |= 1 << i;
        }

		public Transform CheckVacantPoint(out int index)
        {
            for (int i = 0; i < m_Exits.Length; i++)
            {
                if ((m_Vacancies & (1 << i)) == 0)
                {
                    index = i;
                    return m_Exits[i];
                }
            }
            index = -1;
            return null;
        }

        public bool CheckValidChild(LevelChunk chunk) => ((int)chunk.m_Type & (int)m_ValidChildTypeMask) == 0;

        public void MatchExits(Transform prev, Transform next)
        {
            var correctiveRotation = Azimuth(-prev.forward) - Azimuth(next.forward);
            transform.RotateAround(next.position, Vector3.up, correctiveRotation);
            var correctivePosition = prev.position - next.position;
            transform.position += correctivePosition;
        }

        public static float Azimuth(Vector3 vector) => Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);*/
	}
}
