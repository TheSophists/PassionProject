using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;

    public GameObject UI;

    public Transform itemsParent;

    InventorySlot[] slots;

    bool flag;


    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        UI.transform.GetChild(0).gameObject.SetActive(false);
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        if (Input.GetKeyDown(KeyCode.I))
        {
            flag = !flag;
            UI.transform.GetChild(0).gameObject.SetActive(flag);
        }
    }

    void UpdateUI()
    {for(int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
