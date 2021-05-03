using System;
using Unity.Entities;

/// <summary>
/// Health points for a character
/// </summary>
[Serializable]
public struct Health : IComponentData
{
	public int max;
	public int current;
}
