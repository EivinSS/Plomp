using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningBlock : MonoBehaviour
{
    [SerializeField] Material winningMat;
    [SerializeField] GameObject winningSphere;

    private void Start()
    {
        winningSphere.SetActive(false);
    }
    public void ActivateWinningSphere()
    {
        SetWinningMat();
        winningSphere.SetActive(true);
    }

    void SetWinningMat()
    {
        GetComponent<Renderer>().material = winningMat;
    }
}
