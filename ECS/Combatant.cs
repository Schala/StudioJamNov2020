using System;
using Unity.Entities;

[Flags]
public enum CombatFlags : byte
{
    None = 0,
    Dead = 1,
    Incapacitated = 2,
    Poisoned = 4,
    Possessed = 8,
    Invulnerable = 16,
    Elite = 32,
    Boss = 64
}

[GenerateAuthoringComponent]
public struct Combatant : IComponentData
{
    public Weapon primaryWeapon;
    public Weapon secondaryWeapon;
    public Weapon leftHand;
    public Weapon rightHand;
    public CombatFlags flags;
}
