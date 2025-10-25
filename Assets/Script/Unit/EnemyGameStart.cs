using System;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyGameStart", menuName = "Scriptable/EnemyGameStart")]
public class EnemyGameStart : EnemyDataBase
{
    public override string Name => "GameStartEnemy";
    public override int[] AttackRange => new[] {0};
    public override int MoveRange => 0;
    public override Action<EnemyUnit> DeathAction => DeathBase; //MonoBehaviourを引数に渡すとそこからgameObjectを取得することができる
    void DeathBase(EnemyUnit unit)
    {
        unit.gameObject.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        DOTween.Sequence().AppendInterval(10f).AppendCallback(() =>
            {
                DOTween.KillAll();
                GameDataManager.Instance.InGameManager.NextStage();
            }).Play();
    }
}