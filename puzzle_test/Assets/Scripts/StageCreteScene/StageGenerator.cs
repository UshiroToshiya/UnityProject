using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [Header("ステージサイズ")]
    [SerializeField] int width = 10;
    [SerializeField] int depth = 10;
    [SerializeField] float spacing = 1.2f;

    [Header("建物設定")]
    [SerializeField] bool generateBuildings = true;
    [SerializeField] int buildingCount = 5;
    [SerializeField] int minFloors = 2;
    [SerializeField] int maxFloors = 6;

    [Header("参照")]
    [SerializeField] StageCameraController cameraController;

    public Transform stageRoot;

    public void Generate()
    {
        Clear();

        stageRoot = new GameObject("StageRoot").transform;

        GenerateGround();
        if (generateBuildings)
            GenerateBuildings();

        cameraController.AdjustCamera(width, depth);
    }

    void Clear()
    {
        if (stageRoot != null)
            DestroyImmediate(stageRoot.gameObject);
    }

    void GenerateGround()
    {
        Transform root = new GameObject("Ground").transform;
        root.SetParent(stageRoot);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                var tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tile.transform.SetParent(root);
                tile.transform.localScale = new Vector3(1, 0.2f, 1);
                tile.transform.position = new Vector3(
                    x * spacing,
                    -0.1f,
                    z * spacing
                );
            }
        }
    }

    void GenerateBuildings()
    {
        Transform root = new GameObject("Buildings").transform;
        root.SetParent(stageRoot);

        for (int i = 0; i < buildingCount; i++)
        {
            int x = Random.Range(0, width);
            int z = Random.Range(0, depth);
            int floors = Random.Range(minFloors, maxFloors + 1);

            CreateBuilding(root, x, z, floors);
        }
    }

    void CreateBuilding(Transform parent, int gridX, int gridZ, int floors)
    {
        Transform buildingRoot = new GameObject("Building").transform;
        buildingRoot.SetParent(parent);

        for (int y = 0; y < floors; y++)
        {
            var floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.transform.SetParent(buildingRoot);
            floor.transform.localScale = Vector3.one;
            floor.transform.position = new Vector3(
                gridX * spacing,
                y + 0.5f,
                gridZ * spacing
            );
        }
    }


}