using UnityEngine;
using UnityEngine.UIElements;

public class HeaderController : MonoBehaviour
{
    private UIDocument document;
    private VisualElement root;
    public GameObject settingDialog;

    private void Update()
    {
        if (PuzzleGameManager.Instance.isClear)
        {
            gameObject.SetActive(false);
        }
        ;
    }
    private void OnEnable()
    {
        document = GetComponent<UIDocument>();
        if (document == null)
        {
            Debug.LogError("UIDocument ‚ª‚ ‚è‚Ü‚¹‚ñ");
            return;
        }

        root = document.rootVisualElement;

        ClickEvents();
    }

    private void OnDisable()
    {
        UnClickEvents();
    }

    private void ClickEvents()
    {
        root.Q<Button>("settingButton").clicked += OnTitleClicked;

    }

    private void UnClickEvents()
    {
        root.Q<Button>("settingButton").clicked -= OnTitleClicked;

    }

    private void OnTitleClicked()
    {
        settingDialog.SetActive(true);
    }

}
