using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyA", menuName = "Scriptable/EnemyA")]
public class EnemyA : EnemyDataBase
{
    public override string Name => "EnemyA";
    public override int[] AttackRange => new[] {0};
    public override int MoveRange => 1;
    public override Action<EnemyUnit> DeathAction => DeathBase; //MonoBehaviourを引数に渡すとそこからgameObjectを取得することができる
    void DeathBase(EnemyUnit unit)
    {
        unit.gameObject.SetActive(false);
    }
}