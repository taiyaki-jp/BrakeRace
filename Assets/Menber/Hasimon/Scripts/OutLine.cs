using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLine : MonoBehaviour
{
    private bool _isMissed = false;
    public bool IsMissed => _isMissed;

    private void Start()
    {
        _isMissed=false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ゲーム終了
            _isMissed=true;
        }
    }
}
