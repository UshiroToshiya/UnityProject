using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    [Header("街サイズ")]
    [SerializeField] int width = 20;
    [SerializeField] int depth = 20;
    [SerializeField] float spacing = 1.2f;

    [Header("道路設定")]
    [SerializeField] int roadInterval = 4;
    [SerializeField] float roadHeight = 0.1f;

    [Header("建物設定")]
    [SerializeField] int minFloors = 2;
    [SerializeField] int maxFloors = 8;

    [Header("色設定（開発用）")]
    [SerializeField] Color groundColor = new Color(0.3f, 0.8f, 0.3f);
    [SerializeField] Color roadColor = Color.gray;
    [SerializeField] Color buildingColor = new Color(0.8f, 0.8f, 0.9f);

    [Header("参照")]
    [SerializeField] StageCameraController devCamera;

    Transform cityRoot;

    Material groundMat;
    Material roadMat;
    Material buildingMat;

    // =============================
    // 司令塔
    // =============================
    public void GenerateCity()
    {
        Clear();
        CreateMaterials();

        cityRoot = new GameObject("CityRoot").transform;

        GenerateGround();
        GenerateRoads();
        GenerateCityBlocks();

        if (devCamera != null)
            devCamera.AdjustCamera(width, depth);
    }

    // =============================
    // マテリアル生成
    // =============================
    void CreateMaterials()
    {
        groundMat = CreateMaterial(groundColor);
        roadMat = CreateMaterial(roadColor);
        buildingMat = CreateMaterial(buildingColor);
    }

    Material CreateMaterial(Color color)
    {
        var mat = new Material(Shader.Find("Standard"));
        mat.color = color;
        return mat;
    }

    // =============================
    // 全消去
    // =============================
    void Clear()
    {
        if (cityRoot != null)
            DestroyImmediate(cityRoot.gameObject);
    }

    // =============================
    // 地面
    // =============================
    void GenerateGround()
    {
        Transform root = new GameObject("Ground").transform;
        root.SetParent(cityRoot);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                var tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tile.transform.SetParent(root);
                tile.transform.localScale = new Vector3(1, 0.1f, 1);
                tile.transform.position = new Vector3(
                    x * spacing,
                    -0.05f,
                    z * spacing
                );
                tile.GetComponent<Renderer>().material = groundMat;
            }
        }
    }

    // =============================
    // 道路
    // =============================
    void GenerateRoads()
    {
        Transform root = new GameObject("Roads").transform;
        root.SetParent(cityRoot);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                if (x % roadInterval != 0 && z % roadInterval != 0)
                    continue;

                var road = GameObject.CreatePrimitive(PrimitiveType.Cube);
                road.transform.SetParent(root);
                road.transform.localScale = new Vector3(1, roadHeight, 1);
                road.transform.position = new Vector3(
                    x * spacing,
                    roadHeight / 2f,
                    z * spacing
                );
                road.GetComponent<Renderer>().material = roadMat;
            }
        }
    }

    // =============================
    // 区画
    // =============================
    void GenerateCityBlocks()
    {
        Transform root = new GameObject("Blocks").transform;
        root.SetParent(cityRoot);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                if (x % roadInterval == 0 || z % roadInterval == 0)
                    continue;

                GenerateBuilding(root, x, z);
            }
        }
    }

    // =============================
    // 建物
    // =============================
    void GenerateBuilding(Transform parent, int gridX, int gridZ)
    {
        int floors = Random.Range(minFloors, maxFloors + 1);

        Transform buildingRoot = new GameObject("Building").transform;
        buildingRoot.SetParent(parent);

        for (int y = 0; y < floors; y++)
        {
            var floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.transform.SetParent(buildingRoot);
            floor.transform.position = new Vector3(
                gridX * spacing,
                y + 0.5f,
                gridZ * spacing
            );
            floor.GetComponent<Renderer>().material = buildingMat;
        }
    }
}
