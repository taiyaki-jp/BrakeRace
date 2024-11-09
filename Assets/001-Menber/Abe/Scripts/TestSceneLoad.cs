using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestSceneLoad : MonoBehaviour
{
    [SerializeField]private Button button;
    [SerializeField, Label("�t�F�[�h���x")] private float _fadeSpeed=100;
    [SerializeField] private string str=null;

    private Image _fadeImage;

    private float _fillAmount = 0;

    private void Start()
    {
        //button.onClick.AddListener(Fade(FillMethodOriginEnum.Method.Horizontal, FillMethodOriginEnum.HorizontalOrigin.Right));
    }
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
    /// <summary>
    /// Method��Horizontal�̃t�F�[�h���Ăяo���@
    /// </summary>
    /// <param name="method">FillMethodOriginEnum.Method</param>
    /// <param name="origin">���Ŏw�肵�����̂ɑΉ�����FillMethodOriginEnum���g��</param>
    private async void Fade(FillMethodOriginEnum.Method method,FillMethodOriginEnum.HorizontalOrigin origin)
    {
        _fillAmount = 0;
        _fadeImage = Fade_Singleton.FadeImage;
        _fadeImage.fillOrigin = 0;
        while (_fillAmount < 1)
        {
            _fillAmount += Time.deltaTime * _fadeSpeed;
            _fadeImage.fillAmount= _fillAmount;
            await UniTask.Yield();
        }
        SceneManager.LoadScene(str);

        _fadeImage.fillOrigin = 1;
        await UniTask.DelayFrame(100);
        while (_fillAmount > 0)
        {
            _fillAmount -= Time.deltaTime * _fadeSpeed;
            _fadeImage.fillAmount = _fillAmount;
            await UniTask.Yield();
        }
    }
}
