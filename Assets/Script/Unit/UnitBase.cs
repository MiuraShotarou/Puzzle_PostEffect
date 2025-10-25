using UnityEngine;
/// <summary> Unitの規定クラス。汎用的なフィールドとメソッドを持つ</summary>
public abstract class UnitBase : MonoBehaviour
{
    public BulletType BulletType;
    protected Vector3Int _attackTilePos;
    /// <summary> BulletTypeに応じて攻撃UIの表示を切り替えるメソッド </summary>
    protected virtual void Shoot()
    {
        if (BulletType == BulletType.Normal)
        {
            Debug.Log($"基底{_attackTilePos}");
            GameDataManager.Instance.Tilemap.SetTile(_attackTilePos, GameDataManager.Instance.AttackTileBase);
        }
    }
}
