using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _second;     //  時間

    [SerializeField] private TextMeshProUGUI _timeText; //  Textをいれる

    void Start()
    {
        asTimer(_second);
    }
    private async void asTimer(float _second)
    {
        while(_second >= 0)
        {
            if(0 < _second)
            {
                _second -= Time.deltaTime;
                _timeText.text = _second.ToString("F0");
            }
            await UniTask.Yield();
        }
    }
}
