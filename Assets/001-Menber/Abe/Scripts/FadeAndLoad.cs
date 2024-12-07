using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeAndLoad 
{
    private float _fadeSpeed=1;
    public float speed
    {
        set { _fadeSpeed = value; }
    }
    private Image _fadeImage;
    public Image image
    {
        set { _fadeImage = value; }
    }

    /// <summary>
    /// FillAmount式フェードイン
    /// </summary>
    /// <typeparam name="OriginEnum">FillOriginEnum.csの中のEnum</typeparam>
    /// <param name="method">Image.FillMethod</param>
    /// <param name="origin">methodで指定したものに対応するFillOriginEnumを使う</param>
    public async UniTask FadeIn<OriginEnum>(Image.FillMethod method, OriginEnum origin) where OriginEnum : Enum
    {
        if (TypeCheck(method, origin) == false)
        {
            Debug.LogError("指定されたmethodとoriginが正しく対応していません!");
            return;
        }

        float fillAmount = 0;
        _fadeImage.fillAmount = fillAmount;

        _fadeImage.fillMethod = method;
        _fadeImage.fillOrigin = Convert.ToInt32(origin);//Enumをintに変換
        while (fillAmount < 1)
        {
            fillAmount += Time.deltaTime * _fadeSpeed;
            _fadeImage.fillAmount= fillAmount;
            await UniTask.Yield();
        }
    }
    /// <summary>
    /// FillAmount式フェードアウト
    /// </summary>
    /// <typeparam name="OriginEnum">FillOriginEnum.csの中のEnum</typeparam>
    /// <param name="method">Image.FillMethod</param>
    /// <param name="origin">methodで指定したものに対応するFillOriginEnumを使う</param>
    /// <returns></returns>
    public async UniTask FadeOut<OriginEnum>(Image.FillMethod method, OriginEnum origin)where OriginEnum : Enum
    {
        if (TypeCheck(method, origin) == false)
        {
            Debug.LogError("指定されたmethodとoriginが正しく対応していません!");
            return;
        }

        float fillAmount = 1;
        _fadeImage.fillAmount = fillAmount;

        _fadeImage.fillMethod = method;
        _fadeImage.fillOrigin = Convert.ToInt32(origin);//Enumをintに変換
        while (fillAmount > 0)
        {
            fillAmount -= Time.deltaTime * _fadeSpeed;
            _fadeImage.fillAmount = fillAmount;
            await UniTask.Yield();
        }
    }

    /// <summary>
    /// 渡された型をチェックする関数
    /// </summary>
    /// <typeparam name="OriginEnum">FillOriginEnum.csの中のEnum</typeparam>
    /// <param name="method">渡されたImage.FillMethod</param>
    /// <param name="origin">渡されたOriginEnum</param>
    /// <returns>型が合っていたか</returns>
    private bool TypeCheck<OriginEnum>(Image.FillMethod method,OriginEnum origin)where OriginEnum : Enum
    {
        switch (method)
        {
            case Image.FillMethod.Horizontal:
                if (origin.GetType() == typeof(HorizontalOrigin)) return true;
                return false;
            case Image.FillMethod.Vertical:
                if (origin.GetType()==typeof(VerticalOrigin))return true;
                return false;
            case Image.FillMethod.Radial90:
                if (origin.GetType() == typeof(Radial_90_Origin)) return true;
                return false;
            case Image.FillMethod.Radial180:
                if (origin.GetType() == typeof(Radial_180_Origin)) return true;
                return false;
            case Image.FillMethod.Radial360:
                if (origin.GetType() == typeof(Radial_360_Origin)) return true;
                return false;
        }
        return false;
    }
    
    
    /// <summary>
    /// 透明度いじる方式のフェードイン
    /// </summary>
    /// <param name="color">フェードさせるパネルの色</param>
    /// <returns></returns>
    public async UniTask FadeIn(Color color)
    {
        _fadeImage.fillAmount = 1;

        float a = 0f;

        _fadeImage.color=new Color(color.r,color.g,color.b,a);
        while (a<1)
        {
            a += Time.deltaTime*_fadeSpeed;
            _fadeImage.color = new Color(color.r, color.g, color.b, a);
            await UniTask.Yield();
        }
    }
    /// <summary>
    /// 透明度いじる方式のフェードアウト
    /// </summary>
    /// <param name="color">フェードさせるパネルの色</param>
    /// <returns></returns>
    public async UniTask FadeOut(Color color)
    {
        _fadeImage.fillAmount = 1;

        float a = 1f;

        _fadeImage.color = new Color(color.r, color.g, color.b, a);
        while (a > 0)
        {
            a -= Time.deltaTime * _fadeSpeed;
            _fadeImage.color = new Color(color.r, color.g, color.b, a);
            await UniTask.Yield();
        }
    }
}
