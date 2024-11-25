using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeAndLoad2
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
    public async UniTask FadeIn()
    {
        _fillAmount = 0;
        _fadeImage.color = new Color(0, 0, 0, 1);
        _fadeImage.fillMethod = Image.FillMethod.Horizontal;
        _fadeImage.fillOrigin = Convert.ToInt32(FillOriginEnum.HorizontalOrigin.Left);//Enumをintに変換
        while (_fillAmount < 1)
        {
            _fillAmount += Time.deltaTime;
            _fadeImage.fillAmount= _fillAmount;
            await UniTask.Yield();
        }
    }
    /// <summary>
    /// フェードアウト
    /// </summary>
    public async UniTask FadeOut()
    {
        _fillAmount = 1;
        _fadeImage.color = new Color(0, 0, 0, 1);
        _fadeImage.fillMethod = Image.FillMethod.Horizontal;
        _fadeImage.fillOrigin = Convert.ToInt32(FillOriginEnum.HorizontalOrigin.Right);//Enumをintに変換
        while (_fillAmount > 0)
        {
            _fillAmount -= Time.deltaTime;
            _fadeImage.fillAmount = _fillAmount;
            await UniTask.Yield();
        }
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    public async UniTask FadeInWhite()
    {
        _fadeImage.fillAmount = 1;
        float image_a = 0;
        _fadeImage.color = new Color(1, 1, 1, 0);
        _fadeImage.color =new (_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, image_a);
        while (image_a < 1)
        {
            image_a += Time.deltaTime;
            _fadeImage.color = new(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, image_a);
            await UniTask.Yield();
        }
    }
    /// <summary>
    /// フェードイン
    /// </summary>
    public async UniTask FadeOutWhite()
    {
        _fadeImage.fillAmount = 1;
        float image_a = 1;
        _fadeImage.color = new Color(1, 1, 1, 1);
        _fadeImage.color = new(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, image_a);
        while (image_a > 0)
        {
            image_a -= Time.deltaTime; 
            _fadeImage.color = new(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, image_a);
            await UniTask.Yield();
        }
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    public async UniTask FadeOutBrack()
    {
        _fadeImage.fillAmount = 1;
        float image_a = 1;
        _fadeImage.color = new Color(0, 0, 0, 1);
        _fadeImage.color = new(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, image_a);
        while (image_a > 0)
        {
            image_a -= Time.deltaTime;
            _fadeImage.color = new(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, image_a);
            await UniTask.Yield();
        }
    }

}
