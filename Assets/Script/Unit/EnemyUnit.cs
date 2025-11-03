using System;
using UnityEngine;
/// <summary> Script内で敵の情報を参照するためのクラス。ScriptableObjectを保持し、EnemyPrefabにアタッチして使う </summary>
public class EnemyUnit : MonoBehaviour
{
    public EnemyDataBase Data;
    [HideInInspector] public Vector3Int MovePos;
    //Data
    [HideInInspector] public string Name;
    [HideInInspector] public int[] AttackRange;
    [HideInInspector] public int MoveRange;
    [HideInInspector] public Action<EnemyUnit> DeathAction;
    void Start()
    {
        //Data
        Name = Data.Name;
        AttackRange = Data.AttackRange;
        MoveRange = Data.MoveRange;
        DeathAction = Data.DeathAction;
        GameDataManager.Instance.InGameManager.AdvanceTurn += Advance;
        DecideMovePos();
    }
    void Advance()
    {
        transform.position += MovePos; //プレイヤーに追従できるようにしたい
        if (transform.position == new Vector3(0, 0, 0))
        {
            GameDataManager.Instance.InGameManager.Restart();
        }
    }
    public void Death()
    {
        GameDataManager.Instance.InGameManager.AdvanceTurn -= Advance;
        DeathAction?.Invoke(this);
    }
    void DecideMovePos()
    {
        MovePos = new Vector3Int(-MathF.Sign(transform.position.x) * MoveRange, -MathF.Sign(transform.position.y) * MoveRange, 0); //敵のいるポジションによって攻撃方向を修正する
    }
}