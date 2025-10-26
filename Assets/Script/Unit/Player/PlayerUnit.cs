using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary> プレイヤーの機能を実装するクラス </summary>
public class PlayerUnit : UnitBase
{
    [SerializeField] int _attackRange;
    [SerializeField, Range(0, 2)] float _inputDuration;
    GameObject _playerObj;//キャッシュ
    Vector3Int _beforeTilePos; //Shoot, OnMoveの際に更新
    Vector2 _move;
    float _time;
    
    void Start()
    {
        _playerObj = GameDataManager.Instance.PlayerObj;
        Debug.Log("1" + _playerObj);
        //Test
        BulletType = BulletType.Normal;
        _attackTilePos = new Vector3Int(0, _attackRange, 0);
        _beforeTilePos = _attackTilePos;
        GameDataManager.Instance.Tilemap.SetTile(_attackTilePos, GameDataManager.Instance.AttackTileBase);
    }
    void Update()
    {
        //WASD入力ディレイ
        _time += Time.deltaTime;
        if (_time > _inputDuration)
        {
            _time = 0;
            _move = Vector2.zero;
        }
        //Shootメソッドの起動
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Mouse.current.leftButton.wasPressedThisFrame");
            Shoot();
        }
    }
    
    protected override void Shoot()
    {
        base.Shoot();
        if (GameDataManager.Instance.EnemyObjectArray.Any(obj => obj.transform.position == _attackTilePos))
        {
            GameDataManager.Instance.EnemyObjectArray.FirstOrDefault(obj => obj.transform.position == _attackTilePos).GetComponent<EnemyUnit>().Death();
        }
    }
    /// <summary> NewInputSystemからWASD入力を通して一度だけ呼ばれるメソッド</summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_move != context.ReadValue<Vector2>())
            {
                _move = context.ReadValue<Vector2>();
                if (_move == new Vector2(1, 0))//D入力
                {
                    _playerObj.transform.eulerAngles = new Vector3(0, 0, 270);
                    _attackTilePos = new Vector3Int(_attackRange, 0, 0);
                }
                else if (_move == new Vector2(-1, 0))//A入力
                {
                    _playerObj.transform.eulerAngles = new Vector3(0, 0, 90);
                    _attackTilePos = new Vector3Int(_attackRange * -1, 0, 0);
                }
                else if (_move == new Vector2(0, 1))//W入力
                {
                    _playerObj.transform.eulerAngles = new Vector3(0, 0, 0);
                    _attackTilePos = new Vector3Int(0, _attackRange, 0);
                }
                else if (_move == new Vector2(0, -1))//S入力
                {
                    _playerObj.transform.eulerAngles = new Vector3(0, 0, 180);
                    _attackTilePos = new Vector3Int(0, _attackRange * -1, 0);
                }
                //移動後、元の攻撃予測地点から攻撃用のタイルを取り除く
                GameDataManager.Instance.Tilemap.SetTile(_beforeTilePos, GameDataManager.Instance.NormalTileBase);
                //攻撃予測地点にタイルをセットする
                GameDataManager.Instance.Tilemap.SetTile(_attackTilePos, GameDataManager.Instance.PredictedAttackTileBase);
                _beforeTilePos = _attackTilePos;
                GameDataManager.Instance.InGameManager.TurnCount++;
                _time = 0;
            }
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Shoot();
    }
}