using UnityEngine;

public class DragController : MonoBehaviour
{
    private DropView selected;
    private BoardManager board;

    private Vector2Int currentGrid;
    private bool dragging;

    void Start()
    {
        board = FindObjectOfType<BoardManager>();
    }

    void Update()
    {
        if (!PuzzleGameManager.Instance.CanInput) return;

        if (Input.GetMouseButtonDown(0))
            Select();

        if (Input.GetMouseButton(0))
            Drag();

        if (Input.GetMouseButtonUp(0))
            Release();
    }


    void Select()
    {
        //Debug.Log("クリック判定");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            selected = hit.collider.GetComponent<DropView>();
            if (selected == null) return;

            currentGrid = new Vector2Int(
                selected.data.row,
                selected.data.col
            );

            if (PuzzleGameManager.Instance.CanInput) AudioManager.Instance.PlayDropSelect();
            dragging = true;
        }
    }

    //ドラッグした時の処理
    void Drag()
    {
        //Debug.Log("ドラッグ判定");
        if (!dragging || selected == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.forward, selected.transform.position);



        if (!plane.Raycast(ray, out float dist)) return;

        Vector3 worldPos = ray.GetPoint(dist);

        Vector2Int grid = board.WorldToGrid(worldPos);

        Vector2Int delta = grid - currentGrid;

        // 上下左右1マスのみ
        if (Mathf.Abs(delta.x) + Mathf.Abs(delta.y) != 1)
            return;

        int nr = grid.x;
        int nc = grid.y;

        if (!board.IsInside(nr, nc)) return;

        DropView target = board.views[nr, nc];
        if (target == null) return;

        board.SwapViews(selected, target);
        AudioManager.Instance.PlayMoveDrop();

        // ★ 現在位置を更新（ここが肝）
        currentGrid = new Vector2Int(nr, nc);
    }

    //離した時の処理
    void Release()
    {
        if (selected != null && dragging)
        {
            //Debug.Log("離した判定");
            board.ResolveBoard(); // ★ これだけでOK
        }

        dragging = false;
        selected = null;
    }

}
