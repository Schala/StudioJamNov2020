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
using System.Collections.Generic;
using UnityEngine;

namespace StudioJamNov2020
{
    [Serializable]
    public class ObjectPoolItem
    {
        public int m_Amount;
        public GameObject m_Object = null;
        public bool m_Expandable;
    }

    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] List<ObjectPoolItem> m_Objects = null;
        List<GameObject> m_Pool = null;

        private void Awake() => m_Pool = new List<GameObject>();

        private void Start()
        {
            for (int i = 0; i < m_Objects.Count; i++)
            {
                for (int j = 0; j < m_Objects[i].m_Amount; j++)
                {
                    var obj = Instantiate(m_Objects[i].m_Object);
                    obj.name = $"{obj.name} {j + 1}";
                    obj.SetActive(false);
                    m_Pool.Add(obj);
                }
            }
        }

        public GameObject Get(string tag)
        {
            for (int i = 0; i < m_Pool.Count; i++)
            {
                if (!m_Pool[i].activeInHierarchy && m_Pool[i].CompareTag(tag))
                    return m_Pool[i];
            }

            for (int i = 0; i < m_Objects.Count; i++)
            {
                if (m_Objects[i].m_Object.CompareTag(tag))
                {
                    if (m_Objects[i].m_Expandable)
                    {
                        var obj = Instantiate(m_Objects[i].m_Object);
                        obj.SetActive(false);
                        m_Pool.Add(obj);
                        return obj;
                    }
                }
            }

            return null;
        }
    }
}
