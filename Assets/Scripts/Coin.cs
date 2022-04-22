using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameEvent AddCoin;
    public void CollectCoin()
    {
        AddCoin.Raise();
        Destroy(this.gameObject);
    }
}
