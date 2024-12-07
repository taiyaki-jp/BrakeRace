using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ChengeScene : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField,Scene] private string _SceneName;
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
            _fadeManager.Fade(_SceneName, Color.white, Color.white);
        else
            _fadeManager.Fade<Enum>(_SceneName, Image.FillMethod.Horizontal, HorizontalOrigin.Left, Image.FillMethod.Horizontal, HorizontalOrigin.Right);


    }
}
