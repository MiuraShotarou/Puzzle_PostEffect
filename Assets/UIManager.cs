using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject[] _slotObjectArray;
    public int CurrentSlotIndex;

    public GameObject[] SlotObjectArray
    {
        get => _slotObjectArray; 
        set => _slotObjectArray = value;
    }
}
