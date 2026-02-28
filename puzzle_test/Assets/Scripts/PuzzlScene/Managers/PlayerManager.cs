using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public GameObject currentCharacter;
    [SerializeField] public CharacterDatabase characterDatabase;
    public GameObject playerspawnpoint;

    public static PlayerManager Instance { get; private set; }

    [NonSerialized]
    public CharacterAttributeController playerCharacterAttributeController;

    void Awake()
    {
        Instance = this;
        int id = GameManager.Instance.SelectedCharacterId;
        SelectCharacter(characterDatabase.GetById(id).player);
        playerCharacterAttributeController = currentCharacter.GetComponent<CharacterAttributeController>();
    }
    public void SelectCharacter(GameObject character)
    {
        currentCharacter = character;
    }

    [ContextMenu("set CurrentChar")]
    public void setCurrentCharacter()
    {
        Instantiate(currentCharacter, playerspawnpoint.transform);
    }

}
