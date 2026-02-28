using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public void OnLoadScene()
    {
        SceneManager.LoadScene("PuzzleScene");
    }
}
