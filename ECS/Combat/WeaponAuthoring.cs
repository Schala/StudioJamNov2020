using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Authoring component for Weapon
/// </summary>
public class WeaponAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
	[SerializeField] Vector3 projectileSpawnPoint = Vector3.zero;
	[SerializeField] float range = 1f;
	[SerializeField] float rate = 1f;
	[SerializeField] float force = 15f;
	[SerializeField] float durability = 100f;
	[SerializeField] int damage = 5;
	[SerializeField] WeaponType type = WeaponType.Knuckles;

	/// <summary>
	/// Takes our authored values and applies them to the weapon
	/// </summary>
	/// <param name="entity"></param>
	/// <param name="dstManager"></param>
	/// <param name="conversionSystem"></param>
	public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
	{
	}
}
