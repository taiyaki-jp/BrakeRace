using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField,Scene] private string _sceneName;
    [SerializeField]private bool _useWhite=false;

    private FadeManager _fadeManager;
    void Start()
    {
        _button.onClick.AddListener(FadeStart);
        _fadeManager = GameObject.Find("FadeManager").GetComponent<FadeManager>();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void FadeStart()
    {
        if (_useWhite)
            _ = _fadeManager.Fade(_sceneName, Color.white);
        else
            _ = _fadeManager.Fade<Enum>(_sceneName, HorizontalOrigin.Left,HorizontalOrigin.Right);


    }
}
