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

namespace StudioJamNov2020.Entities
{
    public class StreetLamp : MonoBehaviour
    {
        [SerializeField] float m_LightFlickerFrequencyMin = 5f;
        [SerializeField] float m_LightFlickerFrequencyMax = 10f;
        [SerializeField] float m_LightOffInterval = 0.25f;
        [SerializeField] float m_LightOffIntensity = 2f;
        Renderer m_Renderer = null;
        Color m_EmissionOn;
        Color m_EmissionOff;
        float m_LightFlickerRandom = 0f;
        float m_LightFlickerDelta = 0f;
        float m_LightOffDelta = 0f;

        void Awake()
        {
            m_Renderer = GetComponentInChildren<Renderer>();
            m_EmissionOn = m_Renderer.material.GetColor("_EmissionColor");
            m_EmissionOff = m_Renderer.material.GetColor("_EmissionColor") / m_LightOffIntensity;
        }

        void Update()
        {
            if (m_LightFlickerDelta >= m_LightFlickerRandom)
            {
                if (m_LightOffDelta >= m_LightOffInterval)
                {
                    DynamicGI.SetEmissive(m_Renderer, m_EmissionOn);
                    m_LightOffDelta = 0f;
                    m_LightFlickerRandom = Random.Range(m_LightFlickerFrequencyMin, m_LightFlickerFrequencyMax);
                    m_LightFlickerDelta = 0f;
                }
                else
                {
                    DynamicGI.SetEmissive(m_Renderer, m_EmissionOff);
                    m_LightOffDelta += Time.deltaTime;
                }
            }
            else
                m_LightFlickerDelta += Time.deltaTime;
        }
    }
}
