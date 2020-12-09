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
using UnityEngine.UI;

namespace StudioJamNov2020.UI
{
	public class HealthBar : MonoBehaviour
	{
		public Gradient m_Gradient;
		public Slider m_Slider;
		public Image m_Fill;
		int m_MaxValue;
		int m_Current;

		public int Current
		{
			get
			{
				return m_Current;
			}

			set
			{
				if (value < 0) return;

				m_Current = value;
				m_Slider.value = value;
				m_Fill.color = m_Gradient.Evaluate(m_Slider.normalizedValue);
			}
		}

		public int MaxValue
		{
			get
			{
				return m_MaxValue;
			}

			set
			{
				if (value < m_Current) return;

				m_MaxValue = value;
				m_Slider.maxValue = value;
				m_Fill.color = m_Gradient.Evaluate(1f);
			}
		}
	}
}
