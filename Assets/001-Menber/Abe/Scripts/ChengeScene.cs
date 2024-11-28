using NaughtyAttributes;
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
        _fadeManager.FadeWhite(_SceneName);
        else
        _fadeManager.Fade(_SceneName);
    }
}
