using System;
using Unity.Entities;

/// <summary>
/// Mana points for a character
/// </summary>
[Serializable]
public struct Mana : IComponentData
{
	public int max;
	public int current;
}