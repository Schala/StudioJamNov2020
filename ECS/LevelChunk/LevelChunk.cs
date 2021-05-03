using System;
using Unity.Entities;

/// <summary>
/// Describes the type of a level chunk
/// </summary>
public enum LevelChunkType : byte
{
    Normal = 0,
    Entrance,
    End,
    Exit
}

/// <summary>
/// Level chunk type combinations, used for validation
/// </summary>
[Flags]
public enum LevelChunkTypeMask : byte
{
    Normal = 1,
    Entrance = 2,
    End = 4,
    Exit = 8,
    All = 15
}

/// <summary>
/// A fragment of a procedurally generated level, containing the probability of occurence,
/// what type of chunk it is and what it can interact with
/// </summary>
[GenerateAuthoringComponent]
public struct LevelChunk : IComponentData
{
    public float minProbability;
    public float maxProbability;
    public LevelChunkType type;
    public LevelChunkTypeMask validChildTypeMask;
}