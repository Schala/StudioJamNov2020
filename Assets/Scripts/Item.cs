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

namespace StudioJamNov2020
{
	public enum Rarity : byte
	{
		Junk = 0,
		Common,
		Uncommon,
		Rare,
		Epic,
		Legendary
	}

	public abstract class Item : ScriptableObject
	{
		[Header("Vanity")]
		public string m_ItemName = string.Empty;
		public string m_FlavorText = string.Empty;
		public Sprite m_Icon = null;
		public int m_BuyPrice = 0;
		public int m_SellPrice = 0;
		public Rarity m_Rarity = Rarity.Junk;
	}
}
