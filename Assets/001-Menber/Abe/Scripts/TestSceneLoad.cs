using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestSceneLoad : MonoBehaviour
{
    [SerializeField]private Button button;
    [SerializeField, Label("フェード速度")] private float _fadeSpeed=100;
    [SerializeField] private string str=null;

    private Image _fadeImage;

    private float _fillAmount = 0;

    private void Start()
    {
        var a=FillOriginEnum.Radial_360_Origin.Right;

        Debug.Log(a.GetType());
        Debug.Log(a);
        button.onClick.AddListener(test);
    }

    private void test()
    {
        FadeIn(Image.FillMethod.Radial180,FillOriginEnum.Radial_180_Origin.Buttom);
    }
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
    /// <summary>
    /// フェードインを呼び出す　
    /// </summary>
    /// <param name="method">Image.FillMethod</param>
    /// <param name="origin">methodで指定したものに対応するFillOriginEnumを使う</param>
    public async void FadeIn<T>(Image.FillMethod method,T origin)where T : Enum
    {
        if (TypeCheck(method, origin) == false)
        {
            Debug.LogError("指定されたmethodとoriginが正しく対応していません!");
            return;
        }

        _fillAmount = 0;
        _fadeImage = Fade_Singleton.FadeImage;

        _fadeImage.fillMethod = method;
        _fadeImage.fillOrigin = Convert.ToInt32(origin);//Enumをintに変換
        while (_fillAmount < 1)
        {
            _fillAmount += Time.deltaTime * _fadeSpeed;
            _fadeImage.fillAmount= _fillAmount;
            await UniTask.Yield();
        }
        SceneManager.LoadScene(str);
    }
    /// <summary>
    /// フェードアウトを呼び出す
    /// </summary>
    /// <param name="method">Image.FillMethod</param>
    /// <param name="origin">methodで指定したものに対応するFillOriginEnumを使う</param>
    /// <returns></returns>
    public async Task FadeOut<T>(Image.FillMethod method, T origin)where T : Enum
    {
        if (TypeCheck(method, origin) == false)
        {
            Debug.LogError("指定されたmethodとoriginが正しく対応していません!");
            return;
        }
        _fadeImage.fillMethod = method;
        _fadeImage.fillOrigin = Convert.ToInt32(origin);//Enumをintに変換
        _fadeImage.fillOrigin = 1;
        while (_fillAmount > 0)
        {
            _fillAmount -= Time.deltaTime * _fadeSpeed;
            _fadeImage.fillAmount = _fillAmount;
            await UniTask.Yield();
        }
    }

    private bool TypeCheck<T>(Image.FillMethod method,T origin)where T : Enum
    {
        switch (method)
        {
            case Image.FillMethod.Horizontal:
                if (origin.GetType() == typeof(FillOriginEnum.HorizontalOrigin)) return true;
                return false;
            case Image.FillMethod.Vertical:
                if (origin.GetType()==typeof(FillOriginEnum.VerticalOrigin))return true;
                return false;
            case Image.FillMethod.Radial90:
                if (origin.GetType() == typeof(FillOriginEnum.Radial_90_Origin)) return true;
                return false;
            case Image.FillMethod.Radial180:
                if (origin.GetType() == typeof(FillOriginEnum.Radial_180_Origin)) return true;
                return false;
            case Image.FillMethod.Radial360:
                if (origin.GetType() == typeof(FillOriginEnum.Radial_360_Origin)) return true;
                return false;
        }
        return false;
    }
}
