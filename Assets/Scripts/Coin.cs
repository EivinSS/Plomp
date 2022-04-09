using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public void CollectCoin()
    {
        Destroy(this.gameObject);
        GameManager.Instance.AddCoin();
    }
}
