using UnityEngine;
using UnityEngine.UI;

public class CoinCollection : MonoBehaviour
{PlayerManager playerManager;
    bool flag = true;
    Text text;

    private void Start()
    {
        playerManager = PlayerManager.instance;

        text = playerManager.coinText;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && flag == true)
        {
            playerManager.coins++;
            Destroy(this.gameObject);
            string dialog = playerManager.coins.ToString();
            text.text = (": " + dialog);
            flag = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        flag = true;
    }
}
