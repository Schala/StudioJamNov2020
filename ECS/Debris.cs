using Unity.Entities;

/// <summary>
/// A broken-off fragment of a destructable entity
/// </summary>
[GenerateAuthoringComponent]
public struct Debris : IComponentData
{
	public float lifetimeRemaining;
}