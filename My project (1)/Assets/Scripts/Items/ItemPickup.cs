using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItemData item;
    bool flag = true;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && flag == true)
        {

            Inventory.instance.Add(item);
            Destroy(this.gameObject);

            flag = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        flag = true;
    }
}
