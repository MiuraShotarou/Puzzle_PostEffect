using System;
using UnityEngine;
public abstract class EnemyDataBase : ScriptableObject
{
    public abstract string Name { get; }
    public abstract int[] AttackRange { get; }
    public abstract int MoveRange { get; }
    public abstract Action<EnemyUnit> DeathAction { get; }
}
