using System;
using Unity.Entities;

/// <summary>
/// Non-authorable data for a level chunk entity, including how many
/// vacancies it has for connecting to other chunks
/// </summary>
[Serializable]
public struct LevelChunkData : IComponentData
{
	public int vacancies;
}