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

using StudioJamNov2020.World;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StudioJamNov2020
{
    public class GameManager : MonoBehaviour
    {
        public float m_PoisonTick = 2f;
        public float m_DurabilityLossRate = 0.1f;
        public AudioClip m_GameOverBGM = null;
        public Canvas m_UICanvas = null;
        public LevelChunk[] m_LevelChunks = null;
        public GameObject m_GameOverText = null;
        public GameObject m_HealthText = null;
        [SerializeField] int m_MaxChunks = 64;
        List<LevelChunk> m_SpawnedChunks = null;
        int m_PauseDelay = 300;
        bool m_Paused = false;

        private void Awake() => DontDestroyOnLoad(gameObject);

        private void Start() => GenerateChunks();

		private void Update()
		{
            if (m_PauseDelay > 0) m_PauseDelay--;

            if (m_PauseDelay <= 0 && Keyboard.current.enterKey.wasPressedThisFrame)
            {
                m_Paused = !m_Paused;
                Time.timeScale = m_Paused ? 0f : 1f;
                m_PauseDelay = 300;
            }
		}

        void GenerateChunks()
        {
            Debug.Assert(m_LevelChunks != null);

            m_SpawnedChunks = new List<LevelChunk>();

        }
	}
}
