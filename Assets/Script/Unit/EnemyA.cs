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
        Sequence sequence = DOTween.Sequence();
        sequence.Append(unit.transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 5f));
        sequence.Append(unit.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 10f));
        sequence.InsertCallback(14.8f, () => unit.gameObject.GetComponent<AcquisitionAnimation>().enabled = true);
    }
}