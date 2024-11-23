using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeAndLoad 
{
    private float _fadeSpeed=100;
    public float speed
    {
        set { _fadeSpeed = value; }
    }
    private Image _fadeImage;
    public Image image
    {
        set { _fadeImage = value; }
    }

    private float _fillAmount = 0;
    /// <summary>
    /// フェードイン
    /// </summary>
    /// <param name="method">Image.FillMethod</param>
    /// <param name="origin">methodで指定したものに対応するFillOriginEnumを使う</param>
    public async UniTask FadeIn<T>(Image.FillMethod method,T origin) where T : Enum
    {
        if (TypeCheck(method, origin) == false)
        {
            Debug.LogError("指定されたmethodとoriginが正しく対応していません!");
            return;
        }

        _fillAmount = 0;

        _fadeImage.fillMethod = method;
        _fadeImage.fillOrigin = Convert.ToInt32(origin);//Enumをintに変換
        while (_fillAmount < 1)
        {
            _fillAmount += Time.deltaTime * _fadeSpeed;
            _fadeImage.fillAmount= _fillAmount;
            await UniTask.Yield();
        }
    }
    /// <summary>
    /// フェードアウト
    /// </summary>
    /// <param name="method">Image.FillMethod</param>
    /// <param name="origin">methodで指定したものに対応するFillOriginEnumを使う</param>
    /// <returns></returns>
    public async UniTask FadeOut<T>(Image.FillMethod method, T origin)where T : Enum
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
