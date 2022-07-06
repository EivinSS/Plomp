using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] int speed;

    void Update()
    {
        transform.Rotate(0, Time.deltaTime * speed, 0, Space.Self);
    }
}
