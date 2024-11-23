using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLine : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ゲーム終了
        }
    }
}
