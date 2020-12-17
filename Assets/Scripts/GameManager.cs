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
using UnityEngine.InputSystem;

namespace StudioJamNov2020
{
    public class GameManager : MonoBehaviour
    {
        public float m_PoisonTick = 2f;
        public float m_DurabilityLossRate = 0.1f;
        public AudioClip m_GameOverBGM = null;
        public Canvas m_UICanvas = null;
        //public LevelChunkEntry[] m_LevelChunks = null;
        public GameObject m_GameOverText = null;
        public GameObject m_ScoreText = null;
        /*[SerializeField] int m_MaxChunks = 64;
        List<LevelChunk> m_SpawnedChunks = null;*/
        int m_PauseDelay = 300;
        public int m_Score = 0;
        bool m_Paused = false;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            /*for (int i = 0; i < m_LevelChunks.Length; i++)
                Debug.Assert(m_LevelChunks[i].m_MaxProbability != 0f);*/
        }

        //private void Start() => GenerateChunks();

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

        /*int GetValidChunk(int parentChunk)
        {
            var rng = Random.Range(0f, 100f);
            int chosenIndex = 0;

            for (int j = 0; j < m_LevelChunks.Length; j++)
                if (rng >= m_LevelChunks[j].m_MinProbability && rng <= m_LevelChunks[j].m_MaxProbability)
                    chosenIndex = j;

            if (parentChunk < 0 || m_LevelChunks[parentChunk].m_Chunk.CheckValidChild(m_LevelChunks[chosenIndex].m_Chunk))
                return chosenIndex;

            return GetValidChunk(parentChunk);
        }

        void GenerateChunks() // TODO: sphere cast check chunk collision
        {
            Debug.Assert(m_LevelChunks != null);

            m_SpawnedChunks = new List<LevelChunk>();
            int prevChunkIndex = -1;

            for (int i = 0; i < m_MaxChunks; i++)
            {
                LevelChunk chunk = null;
                var chosenIndex = GetValidChunk(prevChunkIndex);

                if (prevChunkIndex < 0)
                    chunk = Instantiate(m_LevelChunks[chosenIndex].m_Chunk, Vector3.zero, Quaternion.identity);
                else
                {
                    var prevChunk = m_LevelChunks[prevChunkIndex].m_Chunk;
                    var chunkPrefab = m_LevelChunks[chosenIndex].m_Chunk;
                    var vacancy = prevChunk.CheckVacantPoint(out int vacancyIndex);
                    var exitIndex = Random.Range(0, chunkPrefab.m_Exits.Length - 1);
                    chunk = Instantiate(chunkPrefab, Vector3.zero, Quaternion.identity);
                    var exit = chunk.m_Exits[exitIndex];

                    chunk.MatchExits(vacancy, exit);
                    prevChunk.m_Vacancies &= ~(1 << vacancyIndex);
                    chunk.m_Vacancies &= ~(1 << exitIndex);
                }

                if (chunk != null) m_SpawnedChunks.Add(chunk);
                prevChunkIndex = chosenIndex;
            }
        }*/
	}
}
