using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CoinText.coinCount += 1;
            SoundManager.PlaySound("Put", true);
            this.gameObject.SetActive(false);
        }
    }
}
