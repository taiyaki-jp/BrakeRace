using UnityEngine;
using UnityEngine.UI;

public class Test1 : MonoBehaviour
{

    [SerializeField] private Button Button;
    
    [SerializeField] private FadeManager _fadeManager;
    // Start is called before the first frame update
    void Start()
    {
        Button.onClick.AddListener(FadeStart);
    }

    private void OnDestroy()
    {
        Button.onClick.RemoveAllListeners();
    }

    private void FadeStart()
    {
        _fadeManager.FadeWhite("A-Test2");
    }
}
