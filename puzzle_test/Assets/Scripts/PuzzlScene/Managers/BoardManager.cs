using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("Board Settings")]
    public int rows = 5;
    public int cols = 6;
    public float cellSize = 2f;

    [Header("References")]
    public Transform boardRoot;

    // ★ 色別Prefab配列（DropType順に並べる）
    [SerializeField] GameObject firePrefab;
    [SerializeField] GameObject waterPrefab;
    [SerializeField] GameObject windPrefab;
    [SerializeField] GameObject thunderPrefab;
    [SerializeField] GameObject earthPrefab;

    [SerializeField] GameObject icePrefab;
    [SerializeField] GameObject woodPrefab;
    [SerializeField] GameObject stormPrefab;
    [SerializeField] GameObject lavaPrefab;
    [SerializeField] GameObject steamPrefab;

    [SerializeField] GameObject penetrationPrefab;

    Dictionary<DropType, GameObject> prefabTable;


    public DropData[,] board;
    public DropView[,] views;

    public bool resolving = false;

    [Header("Chain Timing")]
    public float baseRemoveInterval = 0.06f; // 1連鎖目
    public float chainSpeedUp = 0.03f;       // 連鎖ごとの短縮量
    public float minRemoveInterval = 0.02f; // 最速制限

    [Header("Chain UI")]
    public TextMeshProUGUI chainText;
    int chainCount = 0;
    public float baseFontSize = 5f;
    public float sizePerChain = 5f;
    [SerializeField] private Gradient chainGradient;
    [SerializeField] private int maxChain = 10;

    [Header("ドロップ操作時間")]
    public float inputTimeLimit = 4f;
    private float inputTimer;

    [SerializeField] private CombatManager combatManager;
    [SerializeField] private AttackStockManager attackStockManager;

    private bool playerMoved = false;

    private void Awake()
    {
        InitPrefabTable();
    }

    void Start()
    {
        board = new DropData[rows, cols];
        views = new DropView[rows, cols];
        CreateBoard();
        PuzzleGameManager.Instance.CanInput = true;
    }

    private void Update()
    {
        if (!PuzzleGameManager.Instance.CanInput) return;

        inputTimer -= Time.deltaTime;
        if (inputTimer <= 0f)
        {
            EndInputPhase();
        }
    }

    void InitPrefabTable()
    {
        prefabTable = new Dictionary<DropType, GameObject>
    {
        { DropType.Fire, firePrefab },
        { DropType.Water, waterPrefab },
        { DropType.Wind, windPrefab },
        { DropType.Thunder, thunderPrefab },
        { DropType.Earth, earthPrefab },

        { DropType.Ice, icePrefab },
        { DropType.Wood, woodPrefab },
        { DropType.Storm, stormPrefab },
        { DropType.Lava, lavaPrefab },
        { DropType.Steam, steamPrefab },

        { DropType.Penetration, penetrationPrefab },
    };
        /*foreach (var kv in prefabTable)
        {
            Debug.Log($"[PrefabTable] {kv.Key} = {(kv.Value == null ? "NULL" : kv.Value.name)}");
        }*/
    }

    void CreateBoard()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                CreateDropInternal(r, c, GetRandomBaseType(), false);
            }
        }
    }


    public void StartInputPhase()
    {
        if (combatManager == null || !combatManager.enemyManager.GetCurrentEnemy())
        {
            PuzzleGameManager.Instance.CanInput = false;
            return;
        }
        inputTimer = inputTimeLimit;
        PuzzleGameManager.Instance.CanInput = true;
        playerMoved = false;
    }

    void EndInputPhase()
    {
        if (!PuzzleGameManager.Instance.CanInput) return; // ★ 二重防止
        PuzzleGameManager.Instance.CanInput = false;

        if (playerMoved)
        {
            ResolveBoard();
        }
        else
        {
            StartInputPhase(); // 操作なしならやり直し
        }
    }


    DropView CreateDropInternal(
    int r,
    int c,
    DropType type,
    bool playFall,
    int spawnRow = -1 // ★ デフォルトは盤面外上
)
    {
        Debug.Log($"[CreateDropInternal] spawn ({r},{c}) type={type} playFall={playFall}");

        if (prefabTable == null)
        {
            Debug.LogError("prefabTable is NULL");
            return null;
        }

        if (!prefabTable.TryGetValue(type, out var prefab) || prefab == null)
        {
            Debug.LogError($"Prefab missing for {type}");
            return null;
        }

        GameObject obj = Instantiate(prefab, boardRoot);
        obj.SetActive(true);

        DropView view = obj.GetComponent<DropView>();
        if (view == null)
        {
            Debug.LogError("DropView missing");
            return null;
        }

        DropData data = new DropData(r, c, type);
        view.Setup(data);

        Vector3 targetPos = GridToWorld(r, c);

        if (playFall)
        {
            // ★ spawnRow を使用する
            Vector3 spawnPos = GridToWorld(spawnRow, c);
            obj.transform.position = spawnPos;

            Debug.Log($"[CreateDropInternal] fall from row={spawnRow} to row={r}");
            view.PlayFall(targetPos);
        }
        else
        {
            obj.transform.position = targetPos;
        }

        board[r, c] = data;
        views[r, c] = view;

        return view;
    }





    DropType GetRandomBaseType()
    {
        // 基礎5属性のみ
        int baseCount = 5;
        return (DropType)UnityEngine.Random.Range(0, baseCount);
    }



    public Vector3 GridToWorld(int r, int c)
    {
        return new Vector3(

            c * cellSize,                 // Xは固定
           -r * cellSize,      // Yが縦方向（重力）
           0f        // Zが横方向

        );
    }

    public bool IsInside(int r, int c)
    {
        return r >= 0 && r < rows && c >= 0 && c < cols;
    }

    public void SwapViews(DropView a, DropView b)
    {
        if (a == null || b == null) return;

        playerMoved = true; // ★ 操作した

        DropData da = a.data;
        DropData db = b.data;

        // データは即入れ替え
        board[da.row, da.col] = db;
        board[db.row, db.col] = da;

        (da.row, db.row) = (db.row, da.row);
        (da.col, db.col) = (db.col, da.col);

        views[da.row, da.col] = a;
        views[db.row, db.col] = b;

        // ★ Viewはぬるっと移動
        a.MoveTo(GridToWorld(da.row, da.col));
        b.MoveTo(GridToWorld(db.row, db.col));
    }


    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int col = Mathf.RoundToInt(worldPos.x / cellSize);
        int row = Mathf.RoundToInt(-worldPos.y / cellSize);

        return new Vector2Int(row, col);
    }


    public List<DropView> CheckMatches()
    {
        HashSet<DropView> removeSet = new HashSet<DropView>();

        // ===== 横方向 =====
        for (int r = 0; r < rows; r++)
        {
            int count = 1;

            for (int c = 1; c <= cols; c++)
            {
                if (c < cols &&
                    board[r, c] != null &&
                    board[r, c - 1] != null &&
                    board[r, c].type == board[r, c - 1].type)
                {
                    count++;
                }
                else
                {
                    if (count >= 3)
                    {
                        chainCount++;
                        UpdateChainUI();
                        for (int k = 0; k < count; k++)
                            removeSet.Add(views[r, c - 1 - k]);
                    }
                    count = 1;
                }
            }
        }

        // ===== 縦方向 =====
        for (int c = 0; c < cols; c++)
        {
            int count = 1;

            for (int r = 1; r <= rows; r++)
            {
                if (r < rows &&
                    board[r, c] != null &&
                    board[r - 1, c] != null &&
                    board[r, c].type == board[r - 1, c].type)
                {
                    count++;
                }
                else
                {
                    if (count >= 3)
                    {
                        chainCount++;
                        UpdateChainUI();

                        for (int k = 0; k < count; k++)
                            removeSet.Add(views[r - 1 - k, c]);
                    }
                    count = 1;
                }
            }
        }

        return new List<DropView>(removeSet);
    }


    public class MatchGroup
    {
        public List<DropView> drops = new List<DropView>();
    }


    public bool ApplyGravity()
    {
        bool moved = false;

        for (int c = 0; c < cols; c++)
        {
            for (int r = rows - 1; r >= 0; r--)
            {
                if (board[r, c] != null) continue;

                for (int sr = r - 1; sr >= 0; sr--)
                {
                    if (board[sr, c] != null)
                    {
                        DropData data = board[sr, c];
                        DropView view = views[sr, c];

                        board[r, c] = data;
                        views[r, c] = view;
                        board[sr, c] = null;
                        views[sr, c] = null;

                        data.row = r;

                        // ★ 即時移動しない
                        view.PlayFall(GridToWorld(r, c));

                        moved = true;
                        break;
                    }
                }
            }
        }

        return moved;
    }

    void SpawnNewDrops()
    {
        for (int c = 0; c < cols; c++)
        {
            for (int r = 0; r < rows; r++)
            {
                if (board[r, c] != null) continue;

                // ★ Lv1（5属性）のみ生成
                DropType type = (DropType)UnityEngine.Random.Range(0, 5);

                CreateDropInternal(
                    r,
                    c,
                    type,
                    playFall: true,
                    spawnRow: -1
                );
            }
        }
    }


    public void ResolveBoard()
    {
        if (resolving) return;
        StartCoroutine(ResolveRoutine());
    }


    IEnumerator ResolveRoutine()
    {
        resolving = true;

        while (true)
        {
            yield return new WaitForSeconds(0.15f);

            var removeList = CheckMatches();
            if (removeList.Count == 0)
                break;

            var groups = SplitByAdjacency(removeList);

            foreach (var group in groups)
            {
                // ===== ① 属性抽出 =====
                HashSet<DropType> types = new HashSet<DropType>();
                foreach (var view in group)
                    types.Add(view.data.type);

                // ===== ② 融合判定 =====
                AttackAttribute resultAttribute =
                    FusionResolver.Resolve(types); // ★ DropType → AttackAttribute

                int amount = group.Count;

                Debug.Log($"[Resolve] types={string.Join(",", types)} result={resultAttribute} amount={amount}");


                // ===== ③ 中心取得 =====
                DropView center = GetCenterView(group);
                int cr = center.data.row;
                int cc = center.data.col;

                // ===== ④ 幻素蓄積 =====
                combatManager.AddAttackAttribute(resultAttribute, amount);

                // ===== ⑤ 削除 =====
                foreach (var view in group)
                {
                    int r = view.data.row;
                    int c = view.data.col;

                    board[r, c] = null;
                    views[r, c] = null;

                    view.PlayRemoveEffect(null);
                    AudioManager.Instance.PlayDeleteDrop();
                    yield return new WaitForSeconds(0.03f);
                }

                // ===== ⑥ 融合ドロップ生成（複合属性のみ）=====
                /*if (types.Count >= 2)
                {
                    CreateDrop(cr, cc, resultAttribute);
                }*/
            }

            yield return new WaitForSeconds(0.05f);

            if (ApplyGravity())
                yield return new WaitForSeconds(0.1f);

            SpawnNewDrops();
        }

        resolving = false;
        StartInputPhase();
        ResetChainUI();
    }


    void UpdateChainUI()
    {
        chainText.gameObject.SetActive(true);
        chainText.text = $"{chainCount}";

        float size = baseFontSize + (chainCount - 1) * sizePerChain;

        chainText.fontSize = size;
        float t = Mathf.Clamp01((float)chainCount / maxChain);
        Color color = chainGradient.Evaluate(t);

        chainText.color = color; // ベースカラー
    }

    void ResetChainUI()
    {
        chainText.gameObject.SetActive(false);
        chainCount = 0;
    }

    public enum AttackAttribute
    {
        Fire,
        Water,
        Wind,
        Thunder,
        Earth,

        Ice,
        Wood,
        Steam,
        Storm,
        Lava,

        Penetration,
        // etc...
    }


    public struct FusionKey
    {
        public HashSet<DropType> types;

        public FusionKey(params DropType[] input)
        {
            types = new HashSet<DropType>(input);
        }

        public override bool Equals(object obj)
        {
            if (obj is not FusionKey other) return false;
            return types.SetEquals(other.types);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (var t in types)
                hash = hash * 31 + t.GetHashCode();
            return hash;
        }
    }

    List<List<DropView>> SplitByAdjacency(List<DropView> drops)
    {
        var result = new List<List<DropView>>();
        var remaining = new HashSet<DropView>(drops);

        while (remaining.Count > 0)
        {
            var start = remaining.First();
            var group = new List<DropView>();
            var queue = new Queue<DropView>();

            queue.Enqueue(start);
            remaining.Remove(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                group.Add(current);

                foreach (var neighbor in GetAdjacentDrops(current))
                {
                    if (remaining.Contains(neighbor))
                    {
                        remaining.Remove(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            result.Add(group);
        }

        return result;
    }

    IEnumerable<DropView> GetAdjacentDrops(DropView view)
    {
        int r = view.data.row;
        int c = view.data.col;

        int[] dr = { -1, 1, 0, 0 };
        int[] dc = { 0, 0, -1, 1 };

        for (int i = 0; i < 4; i++)
        {
            int nr = r + dr[i];
            int nc = c + dc[i];

            if (!IsInside(nr, nc)) continue;
            if (views[nr, nc] == null) continue;

            yield return views[nr, nc];
        }
    }

    DropView GetCenterView(List<DropView> group)
    {
        float avgR = 0f;
        float avgC = 0f;

        foreach (var v in group)
        {
            avgR += v.data.row;
            avgC += v.data.col;
        }

        avgR /= group.Count;
        avgC /= group.Count;

        DropView nearest = group[0];
        float minDist = float.MaxValue;

        foreach (var v in group)
        {
            float d =
                Mathf.Abs(v.data.row - avgR) +
                Mathf.Abs(v.data.col - avgC);

            if (d < minDist)
            {
                minDist = d;
                nearest = v;
            }
        }

        return nearest;
    }


}
