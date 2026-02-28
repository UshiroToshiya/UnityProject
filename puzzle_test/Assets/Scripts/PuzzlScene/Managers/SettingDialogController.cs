using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SettingDialogController : MonoBehaviour
{
    public event Action OnMoveCompleted;
    private UIDocument document;
    private VisualElement root;
    public GameObject settingDialog;

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
        root.Q<Button>("backTitle").clicked += OnTitleClicked;
        root.Q<Button>("backMenu").clicked += OnMenuClicked;
        root.Q<Button>("closeButton").clicked += OnCloseClicked;
    }

    private void UnClickEvents()
    {
        root.Q<Button>("backTitle").clicked -= OnTitleClicked;
        root.Q<Button>("backMenu").clicked -= OnMenuClicked;
        root.Q<Button>("closeButton").clicked -= OnCloseClicked;
    }

    private void OnTitleClicked()
    {
        SceneManager.LoadScene("TitleScene");
    }

    private void OnMenuClicked()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void OnCloseClicked()
    {
        settingDialog.SetActive(false);
    }
}
