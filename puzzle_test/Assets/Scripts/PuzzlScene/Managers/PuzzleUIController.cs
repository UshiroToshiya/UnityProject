using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class PuzzleUIController : MonoBehaviour
{
    public static PuzzleUIController Instance { get; private set; }
    public GameObject skillUI;

    private VisualElement root;
    private Label clearLabel;

    private void Awake()
    {
        Instance = this;

        root = GetComponent<UIDocument>().rootVisualElement;
        clearLabel = root.Q<Label>("clearLabel");
    }

    public void ShowClear()
    {
        clearLabel.experimental.animation
            .Start(new StyleValues
            {
                opacity = 1f
            }, 600)
            .Ease(Easing.OutBack);
        skillUI.gameObject.SetActive(false);
    }
}
