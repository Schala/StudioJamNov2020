using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public enum WeaponType : byte
{
    Knuckles = 0,
    Pistol,
    Rifle,
    OneHandedMelee,
    TwoHandedMelee,
    Thrown
}

/// <summary>
/// Equippable weapon
/// </summary>
public struct Weapon
{
    public float3 projectileSpawnPoint;
    public float range;
    public float rate;
    public float force;
    public float durability;
    public int damage;
    public WeaponType type;

    /// <summary>
    /// Creates a Weapon blob asset
    /// </summary>
    /// <param name="projectileSpawnPoint">If weapon is ranged, where the projectile will spawn</param>
    /// <param name="range">Weapon's attack range</param>
    /// <param name="rate">Weapon's speed</param>
    /// <param name="force">Force applied on hit</param>
    /// <param name="durability">Weapon's durability</param>
    /// <param name="damage">Weapon's average damage</param>
    /// <param name="type">Weapon's type</param>
    /// <param name="allocator">Memory allocator type</param>
	/// <returns>Reference to the newly created blob asset</returns>
    public static BlobAssetReference<Weapon> CreateBlob(float3 projectileSpawnPoint, float range, float rate, float force, float durability, int damage, WeaponType type, Allocator allocator)
    {
        using var blob = new BlobBuilder(Allocator.TempJob);

        ref var weapon = ref blob.ConstructRoot<Weapon>();
        weapon.projectileSpawnPoint = projectileSpawnPoint;
        weapon.range = range;
        weapon.rate = rate;
        weapon.force = force;
        weapon.durability = durability;
        weapon.damage = damage;
        weapon.type = type;

        return blob.CreateBlobAssetReference<Weapon>(allocator);
    }
}

/// <summary>
/// Mutable weapon data
/// </summary>
public struct WeaponData : IComponentData
{
    public int currentDurability;
}
