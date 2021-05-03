using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

/// <summary>
/// Contains the specified rotation to emit smoke
/// </summary>
[Serializable]
public struct SmokeEmission : IComponentData
{
	public quaternion rotation;
}

/// <summary>
/// Keeps the smoke emitter rotation fixed to the specified rotation every tick
/// </summary>
[UpdateInGroup(typeof(PresentationSystemGroup))]
public class SmokeEmissionSystem : SystemBase
{
	protected override void OnUpdate()
	{
		Entities.ForEach((Entity entity, ref Rotation rotation, in SmokeEmission emission) =>
		{
			rotation.Value = emission.rotation;
		});
	}
}
