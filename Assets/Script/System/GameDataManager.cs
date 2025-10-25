using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

/// <summary>
/// ゲーム全体で保持する必要のあるデータを格納する。
/// </summary>
public class GameDataManager : MonoBehaviour
{
    [HideInInspector] public GameObject[] EnemyObjectArray;
    [HideInInspector] public InGameManager InGameManager;
    [HideInInspector] public GameObject PlayerObj;
    [HideInInspector] public Tilemap Tilemap;
    public TileBase NormalTileBase;
    public TileBase PredictedAttackTileBase;
    public TileBase AttackTileBase;
    public (string name, GameObject obj)[] EnemyTupleArray;
    private MyProject InputSystem;
    public static GameDataManager Instance { get; private set; }
    private void Awake()
    {
#if UNITY_EDITOR
        Time.timeScale = 10;
#endif
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Initialize(); //シーンのロード時にも
        SceneManager.activeSceneChanged += ChangeSceneLoaded;
        InGameManager = GetComponent<InGameManager>();
        InputSystem = new MyProject();
    }
    
    void Initialize()
    {
        PlayerObj = GameObject.FindWithTag("Player");
        Tilemap = GameObject.FindWithTag("Tilemap").GetComponent<Tilemap>();
        EnemyObjectArray = FindObjectsByType<EnemyUnit>(FindObjectsSortMode.None).Select(unit => unit.gameObject).ToArray();
        EnemyTupleArray = EnemyObjectArray.Select(enemy => (enemy.name, enemy)).ToArray();
    }

    void ChangeSceneLoaded(Scene current, Scene next)
    {
        Initialize();
    }

    private void OnEnable()
    {
        if (InputSystem != null)
        {
            InputSystem.Enable();
        }
    }
    //<パラメーター関連>
}