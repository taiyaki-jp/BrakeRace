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
    public async UniTask FadeIn<OriginEnum>(OriginEnum origin) where OriginEnum : Enum
    {
        float fillAmount = 0;
        _fadeImage.fillAmount = fillAmount;

        AutoMethodSet(origin);
        _fadeImage.fillOrigin = Convert.ToInt32(origin);//Enumをintに変換
        while (fillAmount < 1)
        {
            fillAmount += Time.deltaTime;
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
    public async UniTask FadeOut<OriginEnum>(OriginEnum origin)where OriginEnum : Enum
    {
        

        float fillAmount = 1;
        _fadeImage.fillAmount = fillAmount;

        AutoMethodSet(origin);
        _fadeImage.fillOrigin = Convert.ToInt32(origin);//Enumをintに変換
        while (fillAmount > 0)
        {
            fillAmount -= Time.deltaTime;
            _fadeImage.fillAmount = fillAmount;
            await UniTask.Yield();
        }
    }

    /// <summary>
    /// 渡されたoriginに基づいてmethodを変える関数
    /// </summary>
    /// <typeparam name="OriginEnum">FillOriginEnum.csの中のEnum</typeparam>
    /// <param name="origin">渡されたOriginEnum</param>
    private void AutoMethodSet<OriginEnum>(OriginEnum origin)where OriginEnum : Enum
    {
        switch (origin)
        {
            case HorizontalOrigin:
                _fadeImage.fillMethod = Image.FillMethod.Horizontal;
                break;
            case VerticalOrigin:
                _fadeImage.fillMethod= Image.FillMethod.Vertical;
                break;
            case Radial_90_Origin:
                _fadeImage.fillMethod=Image.FillMethod.Radial90;
                break;
            case Radial_180_Origin:
                _fadeImage.fillMethod=Image.FillMethod.Radial180;
                break;
            case Radial_360_Origin:
                _fadeImage.fillMethod=Image.FillMethod.Radial360;
                break;
        }
    }
    
    
    /// <summary>
    /// 透明度いじる方式のフェードイン
    /// </summary>
    /// <param name="color">フェードさせるパネルの色</param>
    /// <returns></returns>
    public async UniTask FadeIn()
    {
        _fadeImage.fillAmount = 1;

        float a = 0f;
  
        while (a<1)
        {
            a += Time.deltaTime*_fadeSpeed;
            _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, a);
            await UniTask.Yield();
        }
    }
    /// <summary>
    /// 透明度いじる方式のフェードアウト
    /// </summary>
    /// <param name="color">フェードさせるパネルの色</param>
    /// <returns></returns>
    public async UniTask FadeOut()
    {
        _fadeImage.fillAmount = 1;

        float a = 1f;

        while (a > 0)
        {
            a -= Time.deltaTime * _fadeSpeed;
            _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, a);
            await UniTask.Yield();
        }
    }
    /// <summary>
    /// どの色でフェードするか
    /// </summary>
    /// <param name="color">色</param>
    public void SetColor(Color color)
    {
        _fadeImage.color = color;
    }
}
