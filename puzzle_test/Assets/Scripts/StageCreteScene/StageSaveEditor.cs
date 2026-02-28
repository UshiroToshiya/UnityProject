#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageGenerator))]
public class StageSaveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        StageGenerator generator = (StageGenerator)target;

        GUILayout.Space(10);

        if (GUILayout.Button("ステージをPrefabとして保存"))
        {
            SavePrefab(generator);
        }
    }

    void SavePrefab(StageGenerator generator)
    {
        if (generator.stageRoot == null)
        {
            Debug.LogError("StageRootが存在しません");
            return;
        }

        string path = EditorUtility.SaveFilePanelInProject(
            "Save Stage Prefab",
            "Stage",
            "prefab",
            "保存先を選択してください"
        );

        if (string.IsNullOrEmpty(path)) return;

        PrefabUtility.SaveAsPrefabAsset(
            generator.stageRoot.gameObject,
            path
        );

        Debug.Log("Prefab保存完了: " + path);
    }
}
#endif
