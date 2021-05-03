using System;
using Unity.Entities;

[Serializable]
public struct Vanish : IComponentData
{
	public float decayTime;
	public float fadeTime;
	public float fadeDelta;
}