using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DomeStageGenerator : MonoBehaviour
{
    [Header("Dome Settings")]
    [SerializeField] float radius = 20f;
    [SerializeField] int longitude = 32;
    [SerializeField] int latitude = 16;

    [ContextMenu("Generate Dome")]
    public void GenerateDome()
    {
        Clear();

        GameObject dome = new GameObject("Dome");
        dome.transform.SetParent(transform);

        MeshFilter mf = dome.AddComponent<MeshFilter>();
        MeshRenderer mr = dome.AddComponent<MeshRenderer>();

        mf.sharedMesh = BuildDomeMesh();
        mr.sharedMaterial = CreateDomeMaterial();
    }

    void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);
    }

    Mesh BuildDomeMesh()
    {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new();
        List<int> triangles = new();

        for (int lat = 0; lat <= latitude; lat++)
        {
            float a1 = Mathf.PI * 0.5f * lat / latitude; // ”¼‹…
            float sin1 = Mathf.Sin(a1);
            float cos1 = Mathf.Cos(a1);

            for (int lon = 0; lon <= longitude; lon++)
            {
                float a2 = 2 * Mathf.PI * lon / longitude;
                float sin2 = Mathf.Sin(a2);
                float cos2 = Mathf.Cos(a2);

                Vector3 v = new Vector3(
                    cos2 * sin1,
                    cos1,
                    sin2 * sin1
                ) * radius;

                vertices.Add(v);
            }
        }

        for (int lat = 0; lat < latitude; lat++)
        {
            for (int lon = 0; lon < longitude; lon++)
            {
                int current = lat * (longitude + 1) + lon;
                int next = current + longitude + 1;

                triangles.Add(current);
                triangles.Add(next);
                triangles.Add(current + 1);

                triangles.Add(current + 1);
                triangles.Add(next);
                triangles.Add(next + 1);
            }
        }

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateNormals();

        return mesh;
    }

    Material CreateDomeMaterial()
    {
        Material mat = new Material(Shader.Find("Standard"));
        mat.color = new Color(0.7f, 0.75f, 0.85f);
        mat.SetFloat("_Glossiness", 0.05f);
        return mat;
    }
}
