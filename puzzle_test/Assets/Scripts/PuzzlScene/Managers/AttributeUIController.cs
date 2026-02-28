using UnityEngine;

public class AttributeUIController : MonoBehaviour
{
    [SerializeField] AttributeSlotUI slotPrefab;
    [SerializeField] Transform slotParent;
    [SerializeField] CharacterAttributeController character;

    void Start()
    {
        character = PlayerManager.Instance.currentCharacter.GetComponent<CharacterAttributeController>();
        Debug.Log($"character:{character} ");
        SetSlot();
    }

    void SetSlot()
    {
        foreach (var slot in character.slots)
        {
            var ui = Instantiate(slotPrefab, slotParent);
            slot.currentGauge = 0;
            slot.currentLevel = 0;
            ui.Bind(slot);
        }
    }
}
