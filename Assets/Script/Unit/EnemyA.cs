using System;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "EnemyA", menuName = "Scriptable/EnemyA")]
public class EnemyA : EnemyDataBase
{
    public override string Name => "EnemyA";
    public override int[] AttackRange => new[] {0};
    public override int MoveRange => 1;
    public override Action<EnemyUnit> DeathAction => Death; //MonoBehaviourを引数に渡すとそこからgameObjectを取得することができる
    void Death(EnemyUnit unit)
    {
        unit.transform.DOScale();
        unit.gameObject.GetComponent<AcquisitionAnimation>().enabled = true;
    }
}